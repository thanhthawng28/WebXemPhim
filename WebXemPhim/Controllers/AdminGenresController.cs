using Microsoft.AspNetCore.Mvc;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class AdminGenresController : Controller
    {
        private readonly WebXemPhimContext _context;
        public AdminGenresController(WebXemPhimContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var genres = _context.Genres.OrderBy(g => g.GenreId).ToList();
            return View(genres);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if(ModelState.IsValid)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }
        public IActionResult Edit(int id)
        {
            var genre = _context.Genres.Find(id);
            if(genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }
        [HttpPost]
        public IActionResult Edit(int id, Genre genre)
        {
            if(id != genre.GenreId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(genre);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(genre);
        }
        public IActionResult Delete(int id)
        {
            var genre = _context.Genres.Find(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult MovieStatistics()
        {
            var genreMovies = _context.Genres
                .Select(genre => new MovieStatisticsModel
                {
                    GenreName = genre.GenreName,
                    MovieCount = genre.Movies.Count,
                    MovieViews = genre.Movies.Select(movie => new MovieViewModel
                    {
                        Title = movie.Title,
                        ViewCount = movie.MovieViews.Count
                    }).ToList()
                })
                .ToList();

            return View(genreMovies);
        }
    }
}
