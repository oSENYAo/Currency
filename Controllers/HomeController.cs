using CurrencyDbModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context;
        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<IActionResult>> Currencies()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Currencie(int? id)
        {
            if (id != null)
            {
                EntityCurrency entityCurrency = await context.EntityCurrencies.FirstOrDefaultAsync(x => x.Id == id);
                if (entityCurrency != null)
                {
                    return View(entityCurrency);
                }
            }
            return NotFound();
        }
    }
}
