using Currency.Data;
using Currency.Models;
using Currency.Models.Currency;
using Currency.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;
        private readonly IMemoryCache cache;

        public HomeController(AppDbContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            var dateDb = context.ValCurses.Select(x => x.Date).ToList().LastOrDefault();
            if (cache.TryGetValue("key", out string DateXml))
            {
                if (DateXml != dateDb)
                {
                    var serializer = new XmlSerializer(typeof(ValCurs));
                    using (var reader = new StreamReader(@"D:\repositivs\Currency\Info\NewXmlFile.xml",Encoding.GetEncoding(1251)))
                    {
                        var obj = serializer.Deserialize(reader) as ValCurs;
                        context.ValCurses.UpdateRange(obj);
                        await context.SaveChangesAsync();
                    }

                }
            }
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Currencies", "Home");
            }
            else
            {
                return View();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Currencies(int page = 1)
        {
            int pageSize = 10;

            IQueryable<Valute> source = context.Valutes.Include(x => x.ValCurs);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageInfo pageInfo = new PageInfo(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel { PageInfo = pageInfo, Valutes = items };
            ViewBag.Date = context.ValCurses.Select(x => x.Date).ToList().LastOrDefault();
            return View(viewModel);
        }



        [HttpPost]
        public IActionResult Currencies(string name)
        {
            return RedirectToAction("Currency", new { name = name });
        }

        [HttpGet]
        public async Task<IActionResult> Currency(string name)
        {
            Valute valute;

            valute = await context.Valutes.FirstOrDefaultAsync(x => x.Name == name);
            if (valute != null)
            {
                return View(valute);
            }
            else
            {
                return Content("Данная валюта не была найдена. Повторите попытку.");
            }
        }
    }
} 
