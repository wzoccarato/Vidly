using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        List<Movie> movies = new List<Movie>()
                     {
                         new Movie() {Name = "Shrek"},
                         new Movie() {Name = "Wall-e"}

                     };
        List<Customer> customers = new List<Customer>()
                        {
                            new Customer {Name = "John Smith"},
                            new Customer {Name = "Mary Williams"}
                        };


        // GET: Movies/Random
        public ActionResult Random()
        {
            
            var viewModel = new RandomMovieViewModel
                            {
                                Movies = movies,
                                Customers = customers
                            };
            return View(viewModel);
        }

        public ViewResult Index()
        {
            var movies = GetMovies();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = GetMovies().SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();
            return View(movie);
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