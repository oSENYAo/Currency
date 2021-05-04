using System.Collections.Generic;
using System.Xml.Serialization;

namespace Currency.Models.Currency
{

    [XmlRoot(ElementName = "ValCurs")]
	public class ValCurs
	{
		public int ValCursId { get; set; }
		
		[XmlAttribute(AttributeName = "Date")]
		public string Date { get; set; }
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlElement(ElementName = "Valute")]
		public List<Valute> Valute { get; set; }
	}
}
