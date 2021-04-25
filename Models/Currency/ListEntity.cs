using System.Collections.Generic;

namespace Currency.Models
{
    public class ListEntity
    {
        public int Id { get; set; }
        public List<EntityCurrency> listEntities { get; set; } = new List<EntityCurrency>();
    }
}