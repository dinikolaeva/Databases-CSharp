namespace CarDealer.DTO.Input
{
    using System.Xml.Serialization;

    [XmlType("Sale")]
    public class SalesDto
    {
        [XmlElement("carId")]
        public int CarId { get; set; }

        [XmlElement("customerId")]
        public int CustomerId { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }
    }
}
//< Sale >
//        < carId > 105 </ carId >
//        < customerId > 30 </ customerId >
//        < discount > 30 </ discount >
//    </ Sale >