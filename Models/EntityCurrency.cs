using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Currency.Models
{
    public class EntityCurrency
    {
        public string Id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public List<string> Value1 { get; set; } = new List<string>();
        public decimal Value2 { get; set; }
       
    }
}
