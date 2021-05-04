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
            if (!context.ValCurses.Any())
            {
                using (var reader = new StreamReader(@"D:\repositivs\Currency\Info\XmlFile.xml"))
                {
                    var obj = serializer.Deserialize(reader) as ValCurs;
                    context.ValCurses.UpdateRange(obj);
                }
            }
        }
        
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;

            IQueryable<Valute> source = context.Valutes.Include(x => x.ValCurs);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageInfo pageInfo = new PageInfo(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel { PageInfo = pageInfo, Valutes = items };
            //PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = context.Valutes.Count() };
            //IndexViewModel ivm = new IndexViewModel { PageInfo = pageInfo, Valutes = ValutesPerPages };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Test()
        {
            return View();
        }

    }
}
