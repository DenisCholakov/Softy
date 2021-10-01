using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicHub.Data.Models;

namespace MusicHub
{
    using System;

    using Data;
    using Initializer;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context = 
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var albums = context.Producers.FirstOrDefault(p => p.Id == producerId)
                .Albums.Select(a => new
                {
                    a.Name,
                    a.ReleaseDate,
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs.Select(s => new
                    {
                        s.Name,
                        s.Price,
                        WriterName = s.Writer.Name
                    }).OrderByDescending(s => s.Name)
                        .ThenBy(s => s.WriterName).ToList(),
                    AlbumPrice = a.Price
                }).OrderByDescending(a => a.AlbumPrice).ToList();

            var sb = new StringBuilder();

            foreach (var album in albums)
            {
                sb.AppendLine($"-AlbumName: {album.Name}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate:MM/dd/yyyy}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine("-Songs:");

                for (int i = 0; i < album.Songs.Count(); i++)
                {
                    var currentSong = album.Songs[i];
                    sb.AppendLine($"---#{i + 1}")
                        .AppendLine($"---SongName: {currentSong.Name}")
                        .AppendLine($"---Price: {currentSong.Price:f2}")
                        .AppendLine($"---Writer: {currentSong.WriterName}");
                }

                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var songs = context.Songs
                .ToList()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    s.Name,
                    PerformerName = s.SongPerformers
                        .Select(sp => sp.Performer.FirstName + " " + sp.Performer.LastName)
                        .FirstOrDefault(),
                    WriterName = s.Writer.Name,
                    AlbumProducerName = s.Album.Producer.Name,
                    s.Duration
                }).OrderBy(s => s.Name)
                .ThenBy(s => s.WriterName)
                .ThenBy(s => s.PerformerName).ToList();

            var sb = new StringBuilder();

            for (int i = 0; i < songs.Count; i++)
            {
                var currentsong = songs[i];

                sb.AppendLine($"-Song #{i + 1}")
                    .AppendLine($"---SongName: {currentsong.Name}")
                    .AppendLine($"---Writer: {currentsong.WriterName}")
                    .AppendLine($"---Performer: {currentsong.PerformerName}")
                    .AppendLine($"---AlbumProducer: {currentsong.AlbumProducerName}")
                    .AppendLine($"---Duration: {currentsong.Duration:c}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
