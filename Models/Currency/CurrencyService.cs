using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Currency.Models
{
    public class CurrencyService : BackgroundService
    {
        private IMemoryCache Cache;
        
        public CurrencyService(IMemoryCache memory)
        {
            this.Cache = memory;

        }
        // Временное хранения (Кэш) для xml-файля
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

                    
                    var allBranchs = xdocument.Elements("ValCurs").Nodes().ToList();

                    //EntityCurrency entityCurrency = new EntityCurrency();
                    //var allBranch2 = xdocument.Elements("ValCurs").Elements("Valute").ToList();
                    //var result = allBranch2.Select(x => x.CreateReader().AttributeCount);
                    //var s = xdocument.Elements("ValCurs").Elements("Valute").Elements("NumCode").Select(x => x.Value);
                    
                    
                    listCurrency.entityCurrencies.AddRange(allBranchs);

                    #region Объект (свойства) класса EntityCurrency(Первая реализация для установки значений)
                    //entityCurrency.Id = Convert.ToString(xdocument.Elements("ValCurs").Elements("Valute").Select(x => x.Value));
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
                    // Тут должно быть исключение (Конечно можно добавить ещё и Finally)
                }
                await Task.Delay(3600000,stoppingToken);
            }
        }
    }
}
