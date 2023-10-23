using System;
using System.Collections.Generic;

namespace WebXemPhim.Models
{
    public partial class User
    {
        public User()
        {
            FavoriteMovies = new HashSet<FavoriteMovie>();
            MovieViews = new HashSet<MovieView>();
            UserRatings = new HashSet<UserRating>();
        }

        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public bool? UserRole { get; set; }

        public virtual ICollection<FavoriteMovie> FavoriteMovies { get; set; }
        public virtual ICollection<MovieView> MovieViews { get; set; }
        public virtual ICollection<UserRating> UserRatings { get; set; }
    }
}
