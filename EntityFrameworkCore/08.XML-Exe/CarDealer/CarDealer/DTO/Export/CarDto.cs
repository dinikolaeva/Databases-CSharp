namespace CarDealer.DTO.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
//From ExportSaleDto
//<car make="BMW" model="M5 F10" travelled-distance="435603343" />
