namespace CarDealer.DTO.Input
{
    using System.Xml.Serialization;

    [XmlType("Supplier")]
    public class SupplierDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}

//< Supplier >
//  < name > 3M Company </ name >
//   < isImporter > true </ isImporter >
//</ Supplier >