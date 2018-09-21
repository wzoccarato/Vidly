using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI.WebControls;
// da inludere per utilizzate  l'estensione "Include" nel metodo Index
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //List<Movie> movies = new List<Movie>()
        //             {
        //                 new Movie() {Name = "Shrek"},
        //                 new Movie() {Name = "Wall-e"}

        //             };
        //List<Customer> customers = new List<Customer>()
        //                {   
        //                    new Customer {Name = "John Smith"},
        //                    new Customer {Name = "Mary Williams"}
        //                };


        //// GET: Movies/Random
        //public ActionResult Random()
        //{
            
        //    var viewModel = new RandomMovieViewModel
        //                    {
        //                        Movies = movies,
        //                        Customers = customers
        //                    };
        //    return View(viewModel);
        //}


        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }

        [HttpPost]
        public ActionResult Create()
        {
            if (Request.Form["BtnCreate"] != null)
            {
                return RedirectToAction("NewMovie");
            }

            throw new NotImplementedException();
        }


        public ViewResult NewMovie()
        {
            var viewModel = new NewMovieViewModel
                            {
                                Movie = new Movie(),
                                Genres = _context.Genres.ToList()
                            };
            viewModel.Movie.ReleaseDate = DateTime.Today;
            return View(viewModel);
        }


        [HttpPost]
        public ViewResult Save()
        {
            throw new InvalidOperationException();
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new NewMovieViewModel
                            {
                                Movie = movie,
                                Genres = _context.Genres.ToList()
                            };

            return View(viewModel);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
               new Movie { Id = 1, Name = "Shrek" },
               new Movie { Id = 2, Name = "Wall-e" }
            };
        }



            //[Route("movies/released/{year:regex(\\{d4})}/{month:regex(\\{d2}):range(1,12)}")]
            //public ActionResult ByReleaseDate(int year,int month)
            //{
            //    return Content($"{year}/{month}");
            //}

            //public ActionResult ShowMovie(int id)
            //{
            //    var viewModel = new RandomMovieViewModel
            //                    {
            //                        Movies = movies,
            //                        Customers = customers
            //                    };
            //    return View(viewModel);
            //}    
    }
}