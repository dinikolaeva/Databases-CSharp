namespace CarDealer.DTO.Export
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class ExportCustomersDto
    {
        [XmlAttribute("full-name")]
        public string Name { get; set; }

        [XmlAttribute("bought-cars")]
        public int BoughtCars { get; set; }

        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}

//< customer full - name = "Hai Everton" bought - cars = "1" spent - money = "2544.67" />
