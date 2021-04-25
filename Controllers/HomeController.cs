using Currency.Data;
using Currency.Models;
using Currency.Models.Currency;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache cache;

        public HomeController(IMemoryCache cache)
        {
            this.cache = cache;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ListEntity listEntity = new ListEntity();
            return View(listEntity.listEntities);
        }
        #region
        //private readonly AppDbContext context;
        //private readonly IMemoryCache cache;

        //public HomeController(IMemoryCache cache)
        //{
        //    this.cache = cache;
        //}
        //public IActionResult Index()
        //{
        //    return RedirectToAction("Currencies");
        //}


        //[HttpGet]
        //public  IActionResult Currencies()
        //{
        //    if (!cache.TryGetValue("key_currency", out ListCurrency model))
        //    {
        //        throw new Exception("Problem in CurrencyService");
        //    }
        //    return View(model.entityCurrencies);
        //}



        //[HttpGet]
        //public async Task<IActionResult> Currencie(int? NumCode)
        //{
        //    if (NumCode != null)
        //    {
        //        EntityCurrency entityCurrency = await context.EntityCurrencies.FirstOrDefaultAsync(x => x.NumCode == NumCode);
        //        if (entityCurrency != null)
        //        {
        //            return View(entityCurrency);
        //        }
        //    }
        //    return NotFound();
        //}
        #endregion

        #region Реазизация пагинации
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
        #endregion
    }
}
