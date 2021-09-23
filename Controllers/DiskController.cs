using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;
using Mvc.Models;

namespace Mvc.Controllers
{
    public class DiskController : Controller
    {
        private readonly MvcDiskContext _context;

        public DiskController(MvcDiskContext context)
        {
            _context = context;
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
            Response.Cookies.Append("City", id.ToString());
            return RedirectToAction(nameof(Index));
        }
    }
}
