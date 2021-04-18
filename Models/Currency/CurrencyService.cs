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

                    EntityCurrency entityCurrency = new EntityCurrency();

                    entityCurrency.Id = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Attributes("ID").FirstOrDefault().Value);
                    entityCurrency.NumCode = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("NumCode").FirstOrDefault().Value);
                    entityCurrency.CharCode = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("CharCode").FirstOrDefault().Value);
                    entityCurrency.Nominal = Convert.ToInt32(xdocument.Elements("ValCurs").Elements("Valute").Elements("Nominal").FirstOrDefault().Value);
                    entityCurrency.Name = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Elements("Name").FirstOrDefault().Value);
                    entityCurrency.Value = Convert.ToDecimal(xdocument.Elements("ValCurs").Elements("Valute").Elements("Value").FirstOrDefault().Value);


                    Cache.Set("key_currency", entityCurrency, TimeSpan.FromMinutes(1440));
                }
                catch (Exception)
                {

                }
                await Task.Delay(3600000,stoppingToken);
            }
        }
    }
}
