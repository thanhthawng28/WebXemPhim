using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class MovieController : Controller
    {
        private readonly WebXemPhimContext _context;
        public MovieController(WebXemPhimContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.MovieStatus == true)
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count()
                }).OrderBy(movie => movie.ReleaseDay)
                .ToListAsync();

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();
            ViewBag.Genres = genreList;

            return View(movies);
        }
        public async Task<IActionResult> WatchedMovies()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var movies = await _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.MovieStatus == true && movie.MovieViews.Any(view => view.UserName == userName))
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count()
                }).OrderBy(movie => movie.ReleaseDay)
                .ToListAsync();

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();
            ViewBag.Genres = genreList;

            return View(movies);
        }
        public async Task<IActionResult> Search(string searchQuery)
        {
            var moviesQuery = _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.MovieStatus == true);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                moviesQuery = moviesQuery.Where(movie =>
                movie.Title.Contains(searchQuery) ||
                movie.DirectorName.Contains(searchQuery) ||
                movie.MovieActors.Any(actor => actor.ActorName.Contains(searchQuery))
                );
            }

            var movies = await moviesQuery
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count()
                }).OrderBy(movie => movie.ReleaseDay)
                .ToListAsync();

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
                {
                    Value = g.GenreId.ToString(),
                    Text = g.GenreName
                }).ToList();
            ViewBag.Genres = genreList;
            ViewBag.q = searchQuery;
            return View(movies);
        }
        public async Task<IActionResult> HistoryMovies()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var movies = await _context.Movies
                 .Include(movie => movie.FavoriteMovies)
                 .Include(movie => movie.MovieViews)
                 .Include(movie => movie.UserRatings)
                 .Where(movie => movie.MovieStatus == true && movie.MovieViews.Any(fm => fm.UserName == userName))
                 .Select(movie => new MovieViewModel
                 {
                     MovieId = movie.MovieId,
                     Title = movie.Title,
                     ReleaseDay = movie.ReleaseDay,
                     DirectorName = movie.DirectorName,
                     CountryName = movie.CountryName,
                     GenreId = movie.GenreId,
                     ImageLink = movie.ImageLink,
                     Directory = movie.Directory,
                     TimeView = movie.MovieViews.OrderByDescending(view => view.TimeView).FirstOrDefault().TimeView,
                     MovieStatus = movie.MovieStatus,
                     ViewCount = movie.MovieViews.Count(),
                     AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                     LikeCount = movie.FavoriteMovies.Count(fm => fm.UserName == userName)
                 }).OrderByDescending(movie => movie.TimeView)
                 .ToListAsync();

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();

            ViewBag.Genres = genreList;
            return View(movies);
        }
        public async Task<IActionResult> FavoriteMovies()
        {
            string userName = HttpContext.Session.GetString("UserName");
            var movies = await _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.MovieStatus == true && movie.FavoriteMovies.Any(fm => fm.UserName == userName))
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count(fm => fm.UserName == userName)
                }).OrderBy(movie => movie.ReleaseDay)
                .ToListAsync();
            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();

            ViewBag.Genres = genreList;
            return View(movies);
        }
        public async Task<IActionResult> MoviesByGenre(int genreId)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreId == genreId);
            if (genre == null)
            {
                return NotFound();
            }
            var movies = await _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.GenreId == genreId && movie.MovieStatus == true)
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count()
                }).OrderBy(movie => movie.ReleaseDay)
                .ToListAsync();

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();
            ViewBag.Genres = genreList;
            ViewBag.GenreName = genre.GenreName;
            return View(movies);
        }
        public async Task<IActionResult> WatchMovie(int id)
        {
            var movie = await _context.Movies
                .Include(movie => movie.FavoriteMovies)
                .Include(movie => movie.MovieViews)
                .Include(movie => movie.UserRatings)
                .Where(movie => movie.MovieId == id)
                .Select(movie => new MovieViewModel
                {
                    MovieId = movie.MovieId,
                    Title = movie.Title,
                    Content = movie.Content,
                    ReleaseDay = movie.ReleaseDay,
                    DirectorName = movie.DirectorName,
                    CountryName = movie.CountryName,
                    GenreId = movie.GenreId,
                    GenreName = _context.Genres.FirstOrDefault(g => g.GenreId == movie.GenreId).GenreName,
                    ImageLink = movie.ImageLink,
                    Directory = movie.Directory,
                    MovieStatus = movie.MovieStatus,
                    ViewCount = movie.MovieViews.Count(),
                    AvgRating = movie.UserRatings.Any() ? movie.UserRatings.Average(r => r.Rating) : 0,
                    LikeCount = movie.FavoriteMovies.Count()
                })
                .FirstOrDefaultAsync();
            if (movie == null)
            {
                return NotFound();
            }
            var userName = HttpContext.Session.GetString("UserName");
            var movieView = new MovieView
            {
                UserName = userName,
                MovieId = id,
                TimeView = DateTime.Now
            };

            _context.MovieViews.Add(movieView);
            await _context.SaveChangesAsync();
            movie.ViewCount += 1;
            await _context.SaveChangesAsync();
            var actors = await _context.MovieActors
                .Where(ma => ma.MovieId == id)
                .Select(ma => ma.ActorName)
                .ToListAsync();
            var isFavorite = await _context.FavoriteMovies
                .AnyAsync(f => f.UserName == userName && f.MovieId == id);
            ViewBag.IsFavorite = isFavorite;
            ViewBag.Movie = movie;
            ViewBag.Actors = actors;

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            }).ToList();
            var userRating = await _context.UserRatings
                .FirstOrDefaultAsync(ur => ur.UserName == userName && ur.MovieId == id);

            ViewBag.UserRating = userRating?.Rating ?? 0;
            ViewBag.MovieId = id;
            ViewBag.Genres = genreList;
            return View(movie);
        }
        [HttpPost]
        public IActionResult RateMovie(int movieId, int rating)
        {
            string username = HttpContext.Session.GetString("UserName");
            var existingRating = _context.UserRatings.FirstOrDefault(r => r.MovieId == movieId && r.UserName == username);
            if (existingRating != null)
            {
                existingRating.Rating = rating;
            }
            else
            {
                var userRating = new UserRating
                {
                    Rating = rating,
                    UserName = username,
                    MovieId = movieId
                };
                _context.UserRatings.Add(userRating);
            }
            _context.SaveChanges();

            return Ok();
        }
        [HttpPost]
        public IActionResult ToggleFavorite(int movieId)
        {
            string username = HttpContext.Session.GetString("UserName");
            bool isFavorite = _context.FavoriteMovies
                .Any(fm => fm.UserName == username && fm.MovieId == movieId);
            if (isFavorite)
            {
                FavoriteMovie favoriteMovie = _context.FavoriteMovies
                    .FirstOrDefault(fm => fm.UserName == username && fm.MovieId == movieId);
                if (favoriteMovie != null)
                {
                    _context.FavoriteMovies.Remove(favoriteMovie);
                    _context.SaveChanges();
                }
            }
            else
            {
                FavoriteMovie favoriteMovie = new FavoriteMovie
                {
                    UserName = username,
                    MovieId = movieId
                };
                _context.FavoriteMovies.Add(favoriteMovie);
                _context.SaveChanges();
            }
            return Json(new { isFavorite = !isFavorite });
        }
    }
}
