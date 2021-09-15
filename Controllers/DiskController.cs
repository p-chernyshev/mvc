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
            return View(new DiskListViewModel
            {
                Disks = await _context.Disk.ToListAsync(),
                SortBy = sortBy,
                SortDirection = sortDirection,
            });
        }
    }
}
