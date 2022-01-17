namespace Theatre.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Play")]
    public class ImportPlaysDto
    {
        [XmlElement("Title")]
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Title { get; set; }

        [XmlElement("Duration")]
        [Required]
        public string Duration { get; set; }

        [XmlElement("Rating")]
        [Required]
        [Range(0.00, 10.00)]
        public float Rating { get; set; }

        [XmlElement("Genre")]
        [Required]
        public string Genre { get; set; }

        [XmlElement("Description")]
        [Required]
        [MaxLength(700)]
        public string Description { get; set; }

        [XmlElement("Screenwriter")]
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Screenwriter { get; set; }
    }
}
//<Play>
//    <Title>The Hsdfoming</Title>
//    <Duration>03:40:00</Duration>
//    <Rating>8.2</Rating>
//    <Genre>Action</Genre>
//    <Description>A guyat Pinter turns into a debatable conundrum as oth ordinary and menacing. Much of this has to do with the fabled "Pinter Pause," which simply mirrors the way we often respond to each other in conversation, tossing in remainders of thoughts on one subject well after having moved on to another.</Description>
//    <Screenwriter>Roger Nciotti</Screenwriter>
//  </Play>


//•	Title – text with length [4, 50] (required)-
//•	Duration – TimeSpan in format { hours: minutes: seconds}, with a minimum length of 1 hour. (required)-
//•	Rating – float in the range[0.00….10.00] (required)-
//•	Genre – enumeration of type Genre, with possible values (Drama, Comedy, Romance, Musical) (required)-
//•	Description – text with length up to 700 characters (required)-
//•	Screenwriter – text with length [4, 30] (required)
