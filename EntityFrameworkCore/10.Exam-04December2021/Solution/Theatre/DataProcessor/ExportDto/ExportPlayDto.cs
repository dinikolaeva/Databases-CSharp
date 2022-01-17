namespace Theatre.DataProcessor.ExportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;

    [XmlType("Play")]
    public class ExportPlayDto
    {
        [XmlAttribute("Title")]
        public string Title { get; set; }

        [XmlAttribute("Duration")]

        public string Duration { get; set; }

        [XmlAttribute("Rating")]
        public string Rating { get; set; }

        [XmlAttribute("Genre")]
        public string Genre { get; set; }

        [XmlArray("Actors")]
        public CastDto[] Casts { get; set; }

    }
}
//<Play Title="A Raisin in the Sun" Duration="01:40:00" Rating="5.4" Genre="Drama">