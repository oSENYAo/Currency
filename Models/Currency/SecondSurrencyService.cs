using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;
using System.Text;
using System.Globalization;

namespace Currency.Models.Currency
{
    public class SecondSurrencyService : BackgroundService
    {
        private IMemoryCache Cache;
        public SecondSurrencyService(IMemoryCache Cache)
        {
            this.Cache = Cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                string UrlAdress = "http://www.cbr.ru/scripts/XML_daily.asp";
                XmlReader reader = XmlReader.Create(UrlAdress);
                EntityCurrency currency = new EntityCurrency();
                ListEntity listEntity = new ListEntity();
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Valute"))
                    {
                        if (reader.HasAttributes)
                            currency.Id = reader.GetAttribute("ID");
                    }
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "NumCode"))
                        currency.NumCode = Convert.ToInt32(reader.ReadInnerXml());
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "CharCode"))
                        currency.CharCode = Convert.ToString(reader.ReadInnerXml());
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Nominal"))
                        currency.Nominal = Convert.ToInt32(reader.ReadInnerXml());
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Name"))
                        currency.Name = Convert.ToString(reader.ReadInnerXml());
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Value"))
                    {
                        currency.Value = Convert.ToDecimal(reader.ReadInnerXml());
                        listEntity.listEntities.Add(currency);
                    }
                }

                Cache.Set("key_currency", listEntity, TimeSpan.FromMinutes(1440));
            }
            await Task.Delay(3600000, stoppingToken);
        }
    }
}
