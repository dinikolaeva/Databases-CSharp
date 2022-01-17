namespace Theatre.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    public class TicketsDto
    {
        [Required]
        [Range(1.00, 100.00)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public sbyte RowNumber { get; set; }

        [Required]
        public int PlayId { get; set; }
    }
}

//"Tickets": [
//      {
//        "Price": 7.63,
//        "RowNumber": 5,
//        "PlayId": 4
//      }

//Price – decimal in the range[1.00….100.00] (required)-
//•	RowNumber – sbyte in range[1….10](required)-
//•	PlayId – integer, foreign key(required)-