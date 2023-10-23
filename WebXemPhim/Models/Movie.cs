using System;
using System.Collections.Generic;

namespace WebXemPhim.Models
{
    public partial class Movie
    {
        public Movie()
        {
            FavoriteMovies = new HashSet<FavoriteMovie>();
            MovieActors = new HashSet<MovieActor>();
            MovieViews = new HashSet<MovieView>();
            UserRatings = new HashSet<UserRating>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime? ReleaseDay { get; set; }
        public string DirectorName { get; set; } = null!;
        public string CountryName { get; set; } = null!;
        public int? GenreId { get; set; }
        public string? ImageLink { get; set; }
        public string? Directory { get; set; }
        public bool? MovieStatus { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual ICollection<FavoriteMovie> FavoriteMovies { get; set; }
        public virtual ICollection<MovieActor> MovieActors { get; set; }
        public virtual ICollection<MovieView> MovieViews { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
