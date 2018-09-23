using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
            // Eager Loading
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }


        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres,
                Movie = new Movie()
            };

            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
                            {
                                Movie = movie,
                                Genres = _context.Genres.ToList()
                            };

            return View("MovieForm", viewModel);
        }

        //public ActionResult Details(int id)
        //{
        //    var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

        //    if (movie == null)
        //        return HttpNotFound();

        //    return View(movie);
        //}


        // GET: Movies/Random
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };
        //    var customers = new List<Customer>
        //                    {
        //                        new Customer { Name = "Customer 1" },
        //                        new Customer { Name = "Customer 2" }
        //                    };

        //    var viewModel = new RandomMovieViewModel
        //                    {
        //                        Movies = movie,
        //                        Customers = customers
        //                    };
        //    return View(viewModel);
        //}

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                // questa NON utilizzarla. introduce un problema di sicuressa
                //TryUpdateModel(customerInDb);

                // quella che segue e' molto meglio di aggiornare tutte le
                // proprieta'a mano. Utilizza Automapper

                // Mapper.Map(customer, CustomerInDb); 


                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
            }

            _context.SaveChanges();
            
            return RedirectToAction("Index", "Movies");
        }
    }
}