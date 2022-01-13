namespace ProductShop.Dto.Export
{
   using System.Xml.Serialization;

    [XmlType("User")]
    public class UsersWithSoldProducts
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public CountOfUserProductsDto CountOfUserProductsDto { get; set; }
    }
}
//<User>
//< firstName > Cathee </ firstName >
//      < lastName > Rallings </ lastName >
//      < age > 33 </ age >
//      < SoldProducts >
//<count>9</count>

