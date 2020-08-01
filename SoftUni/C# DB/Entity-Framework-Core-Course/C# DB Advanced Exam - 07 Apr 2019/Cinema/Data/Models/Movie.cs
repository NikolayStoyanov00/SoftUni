using Cinema.Data.Models.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Data.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Projections = new HashSet<Projection>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(1, 10)]
        public Genre Genre { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Range(1.00, 10.00)]
        [Required]
        public double Rating { get; set; }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Director { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
    }
}
