namespace WebXemPhim.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime? ReleaseDay { get; set; }
        public string? DirectorName { get; set; }
        public string? CountryName { get; set; }
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public string? ImageLink { get; set; }
        public string? Directory { get; set; }
        public bool? MovieStatus { get; set; }
        public DateTime? TimeView { get; set; }
        public int ViewCount { get; set; }
        public double AvgRating { get; set; }
        public int LikeCount { get; set; }
    }
}
