using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CND.Models;

namespace CND.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context { get; set; }

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Chef> chefs = _context.Chefs
                .Include(chef => chef.CreatedDishes)
                .ToList();
            return View("Index", chefs);
        }

        [HttpGet("new")]
        public IActionResult NewChef()
        {
            return View("NewChef");
        }

        [HttpPost("create/chef")]
        public IActionResult CreateChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                if (newChef.DOB > DateTime.Today)
                {
                    ModelState.AddModelError("DOB", "Not A Future Date.");
                    return View("NewChef");
                }

                _context.Add(newChef);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewChef");
        }

        [HttpGet("dishes")]
        public IActionResult Dishes()
        {
            List<Dish> dishes = _context.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            return View("Dishes", dishes);
        }

        [HttpGet("dishes/new")]
        public IActionResult NewDish()
        {
            List<Chef> chefs = _context.Chefs.ToList();
            ViewBag.Chefs = chefs;
            return View("NewDish");
        }

        [HttpPost("create/dish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Dishes");
            }

            List<Chef> chefs = _context.Chefs.ToList();
            ViewBag.Chefs = chefs;
            return View("NewDish");
        }
    }
}