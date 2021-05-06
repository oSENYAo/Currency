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
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Currency.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context;
        public HomeController(AppDbContext context)
        {
            this.context = context;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var serializer = new XmlSerializer(typeof(ValCurs));
            if (!context.Valutes.Any())
            {
                using (var reader = new StreamReader(@"D:\repositivs\Currency\Info\XmlFile.xml"))
                {
                    var obj = serializer.Deserialize(reader) as ValCurs;
                    context.ValCurses.UpdateRange(obj);
                    context.SaveChanges();
                }
            }
        }
        
        public IActionResult Index()
        {
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
