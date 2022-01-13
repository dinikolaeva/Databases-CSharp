namespace ProductShop.Dto.Import
{
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class CategoriesImportDto
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
//< Category >
//        < name > Drugs </ name >
//    </ Category >