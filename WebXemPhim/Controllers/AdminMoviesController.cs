using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebXemPhim.Models;

namespace WebXemPhim.Controllers
{
    public class AdminMoviesController : Controller
    {
        private readonly WebXemPhimContext _context;
        public AdminMoviesController(WebXemPhimContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m => m.MovieActors).ToListAsync();

            foreach (var movie in movies)
            {
                var genre = await _context.Genres.FindAsync(movie.GenreId);
                movie.Genre = genre;
            }

            return View(movies);
        }

        public async Task<IActionResult> Create()
        {
            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            });
            ViewData["GenreList"] = genreList;
            var genreListFromViewBag = (IEnumerable<SelectListItem>)ViewData["GenreList"];
            foreach (var item in genreListFromViewBag)
            {
                Console.WriteLine($"Value: {item.Value}, Text: {item.Text}");
            }

            return View();
        }
        static List<string> ConvertToArray(string input)
        {
            if (input == null) return null;
            List<string> result = new List<string>();
            input = input.Trim('[', ']');
            string pattern = @"""(.*?)""";
            MatchCollection matches = Regex.Matches(input, pattern);
            foreach (Match match in matches)
            {
                result.Add(match.Groups[1].Value);
            }
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie, string[] ActorList)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                Console.WriteLine(ActorList.Length);
                if (ActorList != null)
                {
                    if (ActorList.Length > 0)
                    {
                        List<string> output = ConvertToArray(ActorList[0]);
                        foreach (var actorName in output)
                        {
                            if (!string.IsNullOrWhiteSpace(actorName))
                            {
                                Console.WriteLine(actorName);
                                movie.MovieActors.Add(new MovieActor
                                {
                                    ActorName = actorName
                                });
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }



        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie, string[] ActorList)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMovieActors = _context.MovieActors.Where(ma => ma.MovieId == id).ToList();
                    _context.MovieActors.RemoveRange(existingMovieActors);
                    if (ActorList.Length > 0)
                    {
                        List<string> actorNames = ConvertToArray(ActorList[0]);
                        foreach (var actorName in actorNames)
                        {
                            if (!string.IsNullOrWhiteSpace(actorName))
                            {
                                movie.MovieActors.Add(new MovieActor
                                {
                                    ActorName = actorName
                                });
                            }
                        }
                    }
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var genres = await _context.Genres.ToListAsync();
            var genreList = genres.Select(g => new SelectListItem
            {
                Value = g.GenreId.ToString(),
                Text = g.GenreName
            });
            ViewData["GenreList"] = genreList;
            var movieActors = await _context.MovieActors
                .Where(ma => ma.MovieId == id)
                .ToListAsync();
            ViewData["MovieActors"] = movieActors;

            return View(movie);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            try
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting movie: {ex.Message}");
                return RedirectToAction(nameof(Index)); 
            }
        }

    }
}
