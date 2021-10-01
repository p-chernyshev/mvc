using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
            var filteredQuery = string.IsNullOrEmpty(search)
                ? _context.Disk
                : _context.Disk.Where(disk => disk.Name.Contains(search));

            var sortedQuery = sortBy switch
            {
                "Name" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.Name) : filteredQuery.OrderByDescending(disk => disk.Name),
                "SizeGb" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.SizeGb) : filteredQuery.OrderByDescending(disk => disk.SizeGb),
                "WriteSpeedMbps" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.WriteSpeedMbps) : filteredQuery.OrderByDescending(disk => disk.WriteSpeedMbps),
                "ReadSpeedMbps" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.ReadSpeedMbps) : filteredQuery.OrderByDescending(disk => disk.ReadSpeedMbps),
                "Price" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.Price) : filteredQuery.OrderByDescending(disk => disk.Price),
                "Type" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.Type) : filteredQuery.OrderByDescending(disk => disk.Type),
                "Form" => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.Form) : filteredQuery.OrderByDescending(disk => disk.Form),
                _ => sortDirection == "asc" ? filteredQuery.OrderBy(disk => disk.Id) : filteredQuery.OrderByDescending(disk => disk.Id),
            };

            return View(new DiskListViewModel
            {
                Disks = await sortedQuery.ToListAsync(),
                SortBy = sortBy,
                SortDirection = sortDirection,
                Search = search,
            });
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
                sessionCart.Add(new CartEntry(diskId));
            }

            var cart = await GetCart(sessionCart);

            SaveSessionCart(sessionCart);
            return Ok(new CartActionResponseModel(cart, diskId));
        }

        private List<CartEntry> GetSessionCart()
        {
            var cartJson = HttpContext.Session.GetString(SessionKeyCart);
            return cartJson is null
                ? new List<CartEntry>()
                : JsonSerializer.Deserialize<List<CartEntry>>(cartJson);
        }

        private int GetCartLength(IEnumerable<CartEntry> cart) => cart.Select(entry => entry.Count).Sum();

        private void SaveSessionCart(IEnumerable<CartEntry> cart)
        {
            HttpContext.Session.SetString(SessionKeyCart, JsonSerializer.Serialize(cart));
        }

        private async Task<CartViewModel> GetCart(List<CartEntry> sessionCart)
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
