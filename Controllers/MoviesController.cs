using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadControl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using usermovieApp.Data;
using usermovieApp.Models;

namespace usermovieApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies

        public async Task<IActionResult> Index(Movie movie, string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);

        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        // GET: Movies/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieCreateViewModel movieCreateViewModel)
        {
            if (ModelState.IsValid)
            {
  
                string filename = Path.GetFileName(movieCreateViewModel.ImageFile.FileName.Trim());
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", filename);

                using (var stream = System.IO.File.Create(path))
                {
                    await movieCreateViewModel.ImageFile.CopyToAsync(stream);
                }


                movieCreateViewModel.Movie.Image = "~/image/" + filename;

                _context.Movie.Add(movieCreateViewModel.Movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movieCreateViewModel);
            
        }

        // GET: Movies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id, MovieCreateViewModel movieCreateViewMode)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movieCreateViewMode);
        }

        // POST: Movies/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieCreateViewModel movieCreateViewModel)
        {
            if (id != movieCreateViewModel.Movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                string filename = Path.GetFileName(movieCreateViewModel.ImageFile.FileName.Trim());
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", filename);

                using (var stream = System.IO.File.Create(path))
                {
                    await movieCreateViewModel.ImageFile.CopyToAsync(stream);
                }


                movieCreateViewModel.Movie.Image = "~/image/" + filename;

                try
                {
                    _context.Update(movieCreateViewModel.Movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movieCreateViewModel.Movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                _context.Movie.Add(movieCreateViewModel.Movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            
            return View(movieCreateViewModel);
        }

        // GET: Movies/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }


    }
}

//kingpash@gmail.com pablo@gmail.com
//Password1@ Pablo0!
