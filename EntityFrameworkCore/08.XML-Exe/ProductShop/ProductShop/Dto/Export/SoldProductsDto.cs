namespace ProductShop.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class SoldProductsDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
//< Product >
//        < name > olio activ mouthwash</name>
//           <price>206.06</price>
//      </Product>

//08
//< Product >
//            < name > Fair Foundation SPF 15</name>
//            <price>1394.24</price>
//          </Product>