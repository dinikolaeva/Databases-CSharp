namespace ProductShop.Dto.Import
{
    using System.Xml.Serialization;

    [XmlType("CategoryProduct")]
    public class CatProdImportDto
    {
        [XmlElement("CategoryId")]
        public int CategoryId { get; set; }

        [XmlElement("ProductId")]
        public int ProductId { get; set; }
    }
}
//< CategoryProduct >
//        < CategoryId > 4 </ CategoryId >
//        < ProductId > 1 </ ProductId >
//    </ CategoryProduct >