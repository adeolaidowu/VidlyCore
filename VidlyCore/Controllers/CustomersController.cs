﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VidlyCore.Models;

namespace VidlyCore.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public IActionResult Index()
        {
            var customers = GetCustomers();
            return View(customers);
        }
        //[Route("Customers/Details/Id")]
        public IActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Serena Williams" },
                new Customer { Id = 2, Name = "Roger Federer" },
            };
        }
    }
}