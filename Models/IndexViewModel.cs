using System.Collections.Generic;

namespace Currency.Models
{
    public class IndexViewModel
    {
        public IEnumerable<EntityCurrency> EntityCurrencies{ get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
