using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Currency.Models
{
    public class ListCurrency
    {
        public List<XObject> entityCurrencies { get; set; } = new List<XObject>();
        public IEnumerable<XObject> xElements { get; set; }

    }
}
