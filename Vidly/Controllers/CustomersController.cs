using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;       // da inludere per utilizzate  l'estensione "Include" nel metodo Index
using System.Web;
using System.Web.Mvc;
using Vidly.Models;


namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // e' necessario farlo perche' _context e' un oggetto "Disposable"
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // con ToList la query non vine piu' eseguita in maniera differita.
            // alcontrario, questa vine eseguita immediatamente, e ritorna subito
            // la lista degli elementi
            // Eager Loading
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            // anche questa query, naturalmente, viene eseguita immdiatamente
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}