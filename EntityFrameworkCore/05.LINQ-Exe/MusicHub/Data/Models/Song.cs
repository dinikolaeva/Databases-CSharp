﻿namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Song
    {

        public Song()
        {
            this.SongPerformers = new HashSet<SongPerformer>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public Genre Genre{ get; set; }

        [ForeignKey("Album")]
        public int? AlbumId { get; set; }

        public Album Album { get; set; }

        [Required]
        [ForeignKey("Writer")]
        public int WriterId { get; set; }

        public Writer Writer { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<SongPerformer> SongPerformers { get; set; }
    }
}

//•	Id – Integer, Primary Key
//•	Name – Text with max length 20 (required)
//•	Duration – TimeSpan(required)
//•	CreatedOn – Date(required)
//•	Genre ¬– Genre enumeration with possible values: "Blues, Rap, PopMusic, Rock, Jazz"(required)
//•	AlbumId – Integer, Foreign key
//•	Album – The song’s album
//•	WriterId – Integer, Foreign key (required)
//•	Writer – The song’s writer
//•	Price – Decimal (required)
//•	SongPerformers – Collection of type SongPerformer
