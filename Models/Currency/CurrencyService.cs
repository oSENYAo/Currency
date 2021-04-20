using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using System.Net;

namespace Currency.Models
{
    public class CurrencyService : BackgroundService
    {
        private IMemoryCache Cache;
        
        public CurrencyService(IMemoryCache memory)
        {
            this.Cache = memory;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                    XDocument xdocument = XDocument.Load("http://www.cbr.ru/scripts/XML_daily.asp");

                    ListCurrency listCurrency = new ListCurrency();

                    EntityCurrency entityCurrency = new EntityCurrency();
                    
                    var allBranch = xdocument.Elements("ValCurs").Nodes().ToList();
                    var allBranch2 = xdocument.Elements("ValCurs").Elements("Valute").ToList();

                    //.Select(x => x.CreateReader()
                    int result = Convert.ToInt32(allBranch2.Select(x => x.CreateReader().AttributeCount));



                    //entityCurrency.Value1.AddRange(result);


                    //var s = xdocument.Elements("ValCurs").Elements("Valute").Elements("NumCode").Select(x => x.Value);
                    //listCurrency.entityCurrencies.AddRange(allBranch);

                    #region
                    entityCurrency.Id = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Select(x => x.CreateReader().Value));
                    //entityCurrency.NumCode = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("NumCode").Select(x => x.Value));
                    //entityCurrency.CharCode = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("CharCode").Select(x => x.Value));
                    //entityCurrency.Nominal = Convert.ToInt32(xdocument.Elements("ValCurs").Elements("Valute").Elements("Nominal").Select(x => x.Value));
                    //entityCurrency.Name = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("Name").Select(x => x.Value));
                    //entityCurrency.Value = Convert.ToDecimal(xdocument.Elements("ValCurs").Elements("Valute").Elements("Value").Select(x => x.Value));
                    #endregion

                    Cache.Set("key_currency", listCurrency, TimeSpan.FromMinutes(1440));
                }
                catch (Exception)
                {

                }
                await Task.Delay(3600000,stoppingToken);
            }
        }
    }
}
