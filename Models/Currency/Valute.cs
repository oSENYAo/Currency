using Currency.Models.Currency;
using System.Xml.Serialization;

namespace Currency.Models
{
    [XmlRoot(ElementName = "Valute")]
	public class Valute
	{
        public int Id { get; set; }
        [XmlElement(ElementName = "NumCode")]
		public string NumCode { get; set; }
		[XmlElement(ElementName = "CharCode")]
		public string CharCode { get; set; }
		[XmlElement(ElementName = "Nominal")]
		public string Nominal { get; set; }
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Value")]
		public string Value { get; set; }
		[XmlAttribute(AttributeName = "ID")]
		public string MainCode { get; set; }
		[XmlText]
		public string Text { get; set; }
        public int ValCursID { get; set; }
        public ValCurs ValCurs{ get; set; }
    }
}
