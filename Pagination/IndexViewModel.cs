using Currency.Models;
using Currency.Models.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Pagination
{
    public class IndexViewModel
    {
        public IEnumerable<Valute> Valutes{ get; set; }
        //public IEnumerable<ValCurs> ValCurses{ get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
