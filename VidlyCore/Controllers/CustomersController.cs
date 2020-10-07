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
    public class CustomersController : Controller
    {
        private readonly AppDbContext _ctx;
        public CustomersController(AppDbContext ctx)
        {
            _ctx = ctx;   
        }

        // GET: Customers
        public IActionResult Index()
        {
            var customers = _ctx.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }
        //[Route("Customers/Details/Id")]
        // action to fetch details of a single customer
        public IActionResult Details(int id)
        {
            var customer = _ctx.Customers.Include(e => e.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // action to create new customer form
        public IActionResult New()
        {
            var membershipTypes = _ctx.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }

        // action to process form
        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _ctx.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            if(customer.Id == 0)
            {
                _ctx.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _ctx.Customers.SingleOrDefault(e => e.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _ctx.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Edit(int Id)
        {
            var customer = _ctx.Customers.SingleOrDefault(e => e.Id == Id);
            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _ctx.MembershipType.ToList()
            };
            return View("CustomerForm", viewModel);
        }
    }
}
