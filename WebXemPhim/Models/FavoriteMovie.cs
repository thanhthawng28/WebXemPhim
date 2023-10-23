using System;
using System.Collections.Generic;

namespace WebXemPhim.Models
{
    public partial class FavoriteMovie
    {
        public int FavoriteMoviesId { get; set; }
        public string? UserName { get; set; }
        public int? MovieId { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? UserNameNavigation { get; set; }
    }
}
