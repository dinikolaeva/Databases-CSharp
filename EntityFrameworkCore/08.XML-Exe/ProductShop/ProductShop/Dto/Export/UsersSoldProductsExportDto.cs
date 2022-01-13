namespace ProductShop.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("Users")]
    public class UsersSoldProductsExportDto
    {
        [XmlElement("count")]
        public int CountOfProducts { get; set; }

        [XmlArray("users")]
        public UsersWithSoldProducts[] UsersWithSoldProducts { get; set; }
    }
}
//<Users>
// < count > 54 </ count >
//  < users >

