using System;
using System.Collections.Generic;

namespace WebXemPhim.Models
{
    public partial class MovieActor
    {
        public int MovieActorsId { get; set; }
        public int? MovieId { get; set; }
        public string? ActorName { get; set; }

        public virtual Movie? Movie { get; set; }
    }
}
