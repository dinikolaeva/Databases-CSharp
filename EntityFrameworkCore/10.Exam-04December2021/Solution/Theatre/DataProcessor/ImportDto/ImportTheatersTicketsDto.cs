namespace Theatre.DataProcessor.ImportDto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ImportTheatersTicketsDto
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public sbyte NumberOfHalls { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Director { get; set; }

        public TicketsDto[] Tickets { get; set; }
    }
}

//"Name": "",
//    "NumberOfHalls": 7,
//    "Director": "Ulwin Mabosty",
//    "Tickets": [
//      {
//        "Price": 7.63,
//        "RowNumber": 5,
//        "PlayId": 4
//      }

//•	Name – text with length [4, 30] (required)
//•	NumberOfHalls – sbyte between[1…10] (required)
//•	Director – text with length [4, 30] (required)
//•	Tickets - a collection of type Ticket
