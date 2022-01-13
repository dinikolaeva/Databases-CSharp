namespace ProductShop.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("SoldProducts")]
    public class CountOfUserProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public SoldProductsDto[] SoldProducts { get; set; }
    }
}
//< SoldProducts >
//        < count > 9 </ count >
//        < products >

