using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Currency.Models
{
    public struct EntityCurrency
    {
        public string Id { get; set; }
        public int? NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public ListEntity ListEntity { get; set; }
    }
}
