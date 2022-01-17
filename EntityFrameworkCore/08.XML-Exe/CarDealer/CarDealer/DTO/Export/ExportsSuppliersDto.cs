﻿namespace CarDealer.DTO.Export
{
    using System.Xml.Serialization;

    [XmlType("suplier")]
    public class ExportsSuppliersDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
//<suplier id="2" name="VF Corporation" parts-count="3" />