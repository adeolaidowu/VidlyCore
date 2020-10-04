using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidlyCore.Data;
using VidlyCore.Models;
using VidlyCore.ViewModels;

namespace VidlyCore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _ctx;

        public MoviesController(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        // GET: Movies
        public IActionResult Random()
        {
            var movie = new Movie { Name = "Wonderwoman" };
            var customers = new List<Customer>
            {
                new Customer { Name = "Serena"},
                new Customer { Name = "Tarzan"}
            };
            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };
            return View(viewModel);
        }
        
        public IActionResult Index()
        {
            var movies = _ctx.Movies.Include(x => x.Genre).ToList();
            return View(movies);
            /*if (!pageIndex.HasValue) pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "Name";*/
            //return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
        }


       // [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public IActionResult ByReleaseYear(int year, int month)
        {
            return Content($"{year}/{month}");
        }


        public IActionResult Details(int Id)
        {
            var movie = _ctx.Movies.Include(z => z.Genre).SingleOrDefault(e => e.Id == Id);
            if (movie == null) return NotFound();
            return View(movie);
        }

        // action for new form
        public IActionResult New()
        {
            var genres = _ctx.Genre.ToList();
            var model = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", model);
        }

        [HttpPost]
        public IActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _ctx.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _ctx.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Genre = movie.Genre;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        //Method to edit movie form
        public IActionResult Edit(int Id)
        {
            var movie = _ctx.Movies.SingleOrDefault(e => e.Id == Id);
            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _ctx.Genre.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}
