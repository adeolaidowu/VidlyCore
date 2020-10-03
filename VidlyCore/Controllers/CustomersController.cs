using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VidlyCore.Data;
using VidlyCore.Models;

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
        public IActionResult Details(int id)
        {
            var customer = _ctx.Customers.Include(e => e.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult New()
        {
            return View();
        }
        

    }
}
