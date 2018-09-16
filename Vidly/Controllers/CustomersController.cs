using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
// da inludere per utilizzate  l'estensione "Include" nel metodo Index
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;


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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel
                            {
                                MembershipTypes = membershipTypes
                            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            return View();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // con ToList la query non vine piu' eseguita in maniera differita.
            // al contrario, questa vine eseguita immediatamente, e ritorna subito
            // la lista degli elementi
            // Eager Loading
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            // anche questa query, naturalmente, viene eseguita immdiatamente
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
    }
}