using Currency.Data;
using Currency.Models.Currency;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Currency.Models
{
    public class CurrencyService
    {
        AppDbContext context;
        public CurrencyService(AppDbContext context)
        {
            this.context = context;
        }
       
    }
}
