using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class DiskController : Controller
    {
        private const string CookieKeyCity = "City";
        private const string SessionKeyCart = "Cart";

        private readonly MvcDiskContext _context;

        public DiskController(MvcDiskContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["SelectedCity"] = Request.Cookies[CookieKeyCity];
            ViewData["InCart"] = GetCartLength(GetSessionCart());
        }

        public async Task<IActionResult> Index(string sortBy = "Id", string sortDirection = "asc", string search = "")
        {
            return View(new DiskListViewModel
            {
                Disks = await DisksList(sortBy, sortDirection, search),
                SortBy = sortBy,
                SortDirection = sortDirection,
                Search = search,
            });
        }

        [EnableCors("Localhost")]
        [HttpGet]
        public async Task<IActionResult> IndexJson(string sortBy = "Id", string sortDirection = "asc", string search = "")
        {
            return Json(await DisksList(sortBy, sortDirection, search));
        }

        private Task<List<Disk>> DisksList(string sortBy, string sortDirection, string search)
        {
            var filteredQuery = string.IsNullOrEmpty(search)
                ? _context.Disk
                : _context.Disk.Where(disk => disk.Name.Contains(search));

            var sortedQuery = sortBy switch
            {
                "Name" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.Name)
                    : filteredQuery.OrderByDescending(disk => disk.Name),
                "SizeGb" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.SizeGb)
                    : filteredQuery.OrderByDescending(disk => disk.SizeGb),
                "WriteSpeedMbps" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.WriteSpeedMbps)
                    : filteredQuery.OrderByDescending(disk => disk.WriteSpeedMbps),
                "ReadSpeedMbps" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.ReadSpeedMbps)
                    : filteredQuery.OrderByDescending(disk => disk.ReadSpeedMbps),
                "Price" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.Price)
                    : filteredQuery.OrderByDescending(disk => disk.Price),
                "Type" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.Type)
                    : filteredQuery.OrderByDescending(disk => disk.Type),
                "Form" => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.Form)
                    : filteredQuery.OrderByDescending(disk => disk.Form),
                _ => sortDirection == "asc"
                    ? filteredQuery.OrderBy(disk => disk.Id)
                    : filteredQuery.OrderByDescending(disk => disk.Id),
            };

            return sortedQuery.ToListAsync();
        }

        [HttpPost]
        public IActionResult SelectCity(int id)
        {
            Response.Cookies.Append(CookieKeyCity, id.ToString());
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cart()
        {
            var cart = await GetCart(GetSessionCart());
            return View(cart);
        }

        [EnableCors("Localhost")]
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] int diskId)
        {
            if (!DiskExists(diskId)) return NotFound();

            var sessionCart = GetSessionCart();

            var selectedDiskEntry = sessionCart.FirstOrDefault(entry => entry.DiskId == diskId);
            if (selectedDiskEntry is { } foundSelectedDiskEntry)
            {
                foundSelectedDiskEntry.Count++;
            }
            else
            {
                sessionCart.Add(new CartEntrySessionJsonModel(diskId));
            }

            var cart = await GetCart(sessionCart);

            SaveSessionCart(sessionCart);
            return Ok(new CartActionResponseModel(cart, diskId));
        }

        [HttpPost]
        public async Task<IActionResult> CartDecreaseEntry([FromBody] int diskId)
        {
            if (!DiskExists(diskId)) return NotFound();

            var sessionCart = GetSessionCart();

            var selectedDiskEntry = sessionCart.FirstOrDefault(entry => entry.DiskId == diskId);
            if (selectedDiskEntry is { Count: > 1 } foundSelectedDiskEntry)
            {
                foundSelectedDiskEntry.Count--;
            }

            var cart = await GetCart(sessionCart);

            SaveSessionCart(sessionCart);
            return Ok(new CartActionResponseModel(cart, diskId));
        }

        [HttpPost]
        public async Task<IActionResult> CartRemoveEntry([FromBody] int diskId)
        {
            if (!DiskExists(diskId)) return NotFound();

            var sessionCart = GetSessionCart();

            var selectedDiskEntry = sessionCart.FirstOrDefault(entry => entry.DiskId == diskId);
            if (selectedDiskEntry is { } foundSelectedDiskEntry)
            {
                sessionCart.Remove(foundSelectedDiskEntry);
            }

            var cart = await GetCart(sessionCart);

            SaveSessionCart(sessionCart);
            return Ok(new CartActionResponseModel(cart, diskId));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var sessionCart = GetSessionCart();
            var cityIdCookie = Request.Cookies[CookieKeyCity];
            if (sessionCart.Count == 0 || cityIdCookie is null) return RedirectToAction(nameof(Cart));

            var cityId = int.Parse(cityIdCookie);

            var order = new Order
            {
                CityId = cityId,
                Entries = sessionCart.Select(cartEntry => new OrderEntry
                {
                    DiskId = cartEntry.DiskId,
                    Count = cartEntry.Count,
                }).ToList(),
            };
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            SaveSessionCart(null);
            return RedirectToAction(nameof(Index));
        }

        private List<CartEntrySessionJsonModel> GetSessionCart()
        {
            var cartJson = HttpContext.Session.GetString(SessionKeyCart);
            return cartJson is null
                ? new List<CartEntrySessionJsonModel>()
                : JsonSerializer.Deserialize<List<CartEntrySessionJsonModel>>(cartJson);
        }

        private int GetCartLength(IEnumerable<CartEntrySessionJsonModel> cart) => cart.Select(entry => entry.Count).Sum();

        private void SaveSessionCart(IEnumerable<CartEntrySessionJsonModel> cart)
        {
            var cartJson = cart is null
                ? "[]"
                : JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(SessionKeyCart, cartJson);
        }

        private async Task<CartViewModel> GetCart(List<CartEntrySessionJsonModel> sessionCart)
        {
            var cartViewList = (await _context.Disk.ToListAsync())
                .Join(
                    sessionCart,
                    disk => disk.Id,
                    cartEntry => cartEntry.DiskId,
                    (disk, cartEntry) => new CartEntryViewModel(disk, cartEntry)
                )
                .ToArray();

            var cart = new CartViewModel(cartViewList);
            return cart;
        }

        private bool DiskExists(int id)
        {
            return _context.Disk.Any(disk => disk.Id == id);
        }
    }
}
