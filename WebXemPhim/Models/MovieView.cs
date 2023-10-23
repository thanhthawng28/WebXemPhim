using System;
using System.Collections.Generic;

namespace WebXemPhim.Models
{
    public partial class MovieView
    {
        public int MovieViewsId { get; set; }
        public string? UserName { get; set; }
        public int? MovieId { get; set; }
        public DateTime TimeView { get; set; }

        public virtual Movie? Movie { get; set; }
        public virtual User? UserNameNavigation { get; set; }
    }
}
