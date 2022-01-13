namespace ProductShop.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class ProductsExportDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("buyer")]
        public string Buyer { get; set; }
    }
}
//<Product>
//    <name>Parsley</name>
//    <price>519.06</price>
//    <buyer>Brendin Predohl</buyer>
//  </Product>


