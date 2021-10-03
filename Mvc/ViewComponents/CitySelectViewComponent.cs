using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mvc.Data;

namespace Mvc.ViewComponents
{
    public class CitySelectViewComponent : ViewComponent
    {
        private readonly MvcDiskContext _context;

        public CitySelectViewComponent(MvcDiskContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new SelectList(await _context.City.ToListAsync(), "Id", "Name", Request.Cookies["City"]));
        }
    }
}
