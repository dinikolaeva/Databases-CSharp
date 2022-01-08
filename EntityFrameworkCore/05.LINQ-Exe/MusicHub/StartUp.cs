namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            //Console.WriteLine(ExportAlbumsInfo(context, 7));

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Albums
                .Where(a => a.ProducerId == producerId)
                .Select(a => new
                {
                    Name = a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    AlbumSongs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        Price = s.Price,
                        SongWriterName = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(w => w.SongWriterName)
                    .ToList(),
                    AlbumPrice = a.Price
                })
                .OrderByDescending(a => a.AlbumPrice)
                .ToList();

            var result = new StringBuilder();

            foreach (var a in albums)
            {
                result.AppendLine($"-AlbumName: {a.Name}");
                result.AppendLine($"-ReleaseDate: {a.ReleaseDate}");
                result.AppendLine($"-ProducerName: {a.ProducerName}");
                result.AppendLine("-Songs:");
                var count = 1;

                foreach (var s in a.AlbumSongs)
                {
                    result.AppendLine($"---#{count}");
                    result.AppendLine($"---SongName: {s.SongName}");
                    result.AppendLine($"---Price: {s.Price:F2}");
                    result.AppendLine($"---Writer: {s.SongWriterName}");
                    count++;
                }

                result.AppendLine($"-AlbumPrice: {a.AlbumPrice:F2}");
            }

            return result.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            TimeSpan seconds = TimeSpan.FromSeconds(duration);

            var songs = context.Songs
                .Where(s => s.Duration > seconds)
                .Select(s => new
                {
                    Name = s.Name,
                    PerformerFullName = s.SongPerformers
                                         .Select(p => p.Performer.FirstName + " " + p.Performer.LastName)
                                         .FirstOrDefault(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer.Name,
                    Duration = s.Duration.ToString("c", CultureInfo.InvariantCulture)
                })
                .OrderBy(s => s.Name)
                .ThenBy(s => s.WriterName)
                .ThenBy(s => s.PerformerFullName)
                .ToList();

            var result = new StringBuilder();
            var count = 1;

            foreach (var s in songs)
            {
                result.AppendLine($"-Song #{count}")
                      .AppendLine($"---SongName: {s.Name}")
                      .AppendLine($"---Writer: {s.WriterName}")
                      .AppendLine($"---Performer: {s.PerformerFullName}")
                      .AppendLine($"---AlbumProducer: {s.AlbumProducer}")
                      .AppendLine($"---Duration: {s.Duration}");

                count++;
            }

            return result.ToString();
        }
    }
}
