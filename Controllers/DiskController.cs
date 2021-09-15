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

        public async Task<IActionResult> Index(string sortBy = "Id", string sortDirection = "asc")
        {
            var sortedQuery = sortBy switch
            {
                "Name" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.Name) : _context.Disk.OrderByDescending(disk => disk.Name),
                "SizeGb" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.SizeGb) : _context.Disk.OrderByDescending(disk => disk.SizeGb),
                "WriteSpeedMbps" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.WriteSpeedMbps) : _context.Disk.OrderByDescending(disk => disk.WriteSpeedMbps),
                "ReadSpeedMbps" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.ReadSpeedMbps) : _context.Disk.OrderByDescending(disk => disk.ReadSpeedMbps),
                "Price" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.Price) : _context.Disk.OrderByDescending(disk => disk.Price),
                "Type" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.Type) : _context.Disk.OrderByDescending(disk => disk.Type),
                "Form" => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.Form) : _context.Disk.OrderByDescending(disk => disk.Form),
                _ => sortDirection == "asc" ? _context.Disk.OrderBy(disk => disk.Id) : _context.Disk.OrderByDescending(disk => disk.Id),
            };

            return View(new DiskListViewModel
            {
                Disks = await sortedQuery.ToListAsync(),
                SortBy = sortBy,
                SortDirection = sortDirection,
            });
        }
    }
}
