using Currency.Data;
using Currency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        IMemoryCache cache;
        
        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }
        public IActionResult Index()
        {
            if (!cache.TryGetValue("key_currency", out EntityCurrency model))
            {
                throw new Exception("ПРОБЛЕМА В контроллере индекс");
            }
            return View(model);
        }


        //[HttpGet]
        //public async Task<IEnumerable<IActionResult>> Currencies()
        //{
        //    return View();
        //}
        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    int pageSize = 3;   // количество элементов на странице

        //    IQueryable<EntityCurrency> source = context.EntityCurrencies.Include(x => x.Name);
        //    var count = await source.CountAsync();
        //    var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        //    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        //    EntityCurrency viewModel = new EntityCurrency();
        //    return View(viewModel);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Currencie(int? id)
        //{
        //    if (id != null)
        //    {
        //        EntityCurrency entityCurrency = await context.EntityCurrencies.FirstOrDefaultAsync(x => x.Id == id);
        //        if (entityCurrency != null)
        //        {
        //            return View(entityCurrency);
        //        }
        //    }
        //    return NotFound();
        //}
    }
}
