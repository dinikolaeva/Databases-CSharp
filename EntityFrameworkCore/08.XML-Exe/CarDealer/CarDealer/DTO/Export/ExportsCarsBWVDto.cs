namespace CarDealer.DTO.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportsCarsBWVDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}

//  <car id="7" model="1M Coupe" travelled-distance="39826890" />
