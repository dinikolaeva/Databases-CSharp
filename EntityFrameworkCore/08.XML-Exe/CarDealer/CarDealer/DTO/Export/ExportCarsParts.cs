namespace CarDealer.DTO.Export
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarsParts
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public ExportParts[] CarPartsArray { get; set; }
    }
}
//<car make="Opel" model="Astra" travelled-distance="516628215">