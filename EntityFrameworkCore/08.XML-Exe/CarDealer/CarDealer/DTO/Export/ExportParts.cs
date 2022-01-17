namespace CarDealer.DTO.Export
{
    using System.Xml.Serialization;

    [XmlType("part")]
    public class ExportParts
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
//<part name="Master cylinder" price="130.99" />