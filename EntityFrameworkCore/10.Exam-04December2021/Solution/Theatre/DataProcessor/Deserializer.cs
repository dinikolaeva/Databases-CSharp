namespace Theatre.DataProcessor
{
    using CarDealer.XMLHelper;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Theatre.Data;
    using Theatre.Data.Models;
    using Theatre.Data.Models.Enums;
    using Theatre.DataProcessor.ImportDto;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportPlay
            = "Successfully imported {0} with genre {1} and a rating of {2}!";

        private const string SuccessfulImportActor
            = "Successfully imported actor {0} as a {1} character!";

        private const string SuccessfulImportTheatre
            = "Successfully imported theatre {0} with #{1} tickets!";

        public static string ImportPlays(TheatreContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var playsDto = XmlConverter.Deserializer<ImportPlaysDto>(xmlString, "Plays");

            var plays = new HashSet<Play>();

            foreach (var pDto in playsDto)
            {
                TimeSpan duration = TimeSpan.ParseExact(pDto.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None);

                Genre genre;
                bool isGenreValid = Enum.TryParse(pDto.Genre, out genre);

                if (!IsValid(pDto)
                    || duration == null
                    || duration.TotalHours < 1
                    || !isGenreValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var play = new Play
                {
                    Title = pDto.Title,
                    Duration = duration,
                    Rating = pDto.Rating,
                    Genre = genre,
                    Description = pDto.Description,
                    Screenwriter = pDto.Screenwriter
                };

                plays.Add(play);
                sb.AppendLine(String.Format(SuccessfulImportPlay, play.Title, play.Genre, play.Rating));
            }

            context.Plays.AddRange(plays);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportCasts(TheatreContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var castsDto = XmlConverter.Deserializer<ImportCastsDto>(xmlString, "Casts");

            var casts = new HashSet<Cast>();

            foreach (var cDto in castsDto)
            {
                if (!IsValid(cDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var cast = new Cast
                {
                    FullName = cDto.FullName,
                    IsMainCharacter = cDto.IsMainCharacter,
                    PhoneNumber = cDto.PhoneNumber,
                    PlayId = cDto.PlayId
                };

                casts.Add(cast);
                sb.AppendLine(String.Format(SuccessfulImportActor, cast.FullName, cast.IsMainCharacter ? "main" : "lesser"));
            }

            context.Casts.AddRange(casts);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportTtheatersTickets(TheatreContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var ticketsDto = JsonConvert.DeserializeObject<ImportTheatersTicketsDto[]>(jsonString);

            var theatres = new HashSet<Theatre>();

            foreach (var tDto in ticketsDto)
            {
                if (!IsValid(tDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var theatre = new Theatre
                {
                    Name = tDto.Name,
                    NumberOfHalls = tDto.NumberOfHalls,
                    Director = tDto.Director
                };

                var tickets = new HashSet<Ticket>();

                foreach (var t in tDto.Tickets)
                {
                    if (!IsValid(t))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var ticket = new Ticket
                    {
                        Price = t.Price,
                        RowNumber = t.RowNumber,
                        PlayId = t.PlayId
                    };

                    tickets.Add(ticket);
                }

                theatre.Tickets = tickets;
                theatres.Add(theatre);

                sb.AppendLine(String.Format(SuccessfulImportTheatre, theatre.Name, theatre.Tickets.Count));
            }

            context.Theatres.AddRange(theatres);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}