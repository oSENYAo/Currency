using Currency.Data;
using Currency.Models.Currency;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Currency.Info
{
    public class InfoService : BackgroundService
    {
        private readonly string UriAdress = "http://www.cbr.ru/scripts/XML_daily.asp";
        IMemoryCache Cache;
        public InfoService(IMemoryCache cache)
        {
            this.Cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var uri = new Uri(UriAdress);
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    // скачиваем через веб
                    WebClient client = new WebClient();
                    client.DownloadFileAsync(uri, Directory.GetCurrentDirectory()+"/NewXmlFile.xml");
                    // скачиваем через xml
                    XDocument xDocument = XDocument.Load(UriAdress);
                    // ищем дату в xml и в БД
                    var DateXml = Convert.ToString(xDocument.Element("ValCurs").Attribute("Date").Value);
                    // кэшируем
                    Cache.Set("key", DateXml, TimeSpan.FromMinutes(1440));
                }
                catch (Exception)
                {
                    throw new Exception("возникает класс info");
                }
                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}
