using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CSChefNDish.Models;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Http;
// HttpContext.Session.SetString("key","")/.GetString("key")/.GetInt32("key",)/.GetInt32("key")
// HttpContext.Session.Clear();
// TempData["var"] = "";  //persist across one redirect only


namespace CSChefNDish.Controllers
{
    public class ChefsController : Controller
    {
        private dbChefNDishContext db;
        public ChefsController(dbChefNDishContext dbContext)
        {
            db = dbContext;
        }

        [HttpGet("")]               //overrides the ones in Home Controller ?? 
        public IActionResult Index()
        {
            // // Retreiving just List<Chef> would work as long as 
            // // its View is also just expecting List<Chef>
            // List<Chef> chefs = db.Chefs
            //     .Include(m => m.createdDishes)
            //     .OrderByDescending(c => c.ChefId).ToList();
            // return View(chefs);

            //First + Last
            //Age = DateTime.Now - BirthDate
            //# of Dishes = db.Chefs.include(m = > m.createdDishes).createdDishes.Count;

            // Also Works!
            ChefNDish chefnDish = new ChefNDish()
            {
                NewDish = new Dish(),
                ListOfChefs = db.Chefs
                    .Include(c => c.createdDishes)
                    .OrderByDescending(c => c.ChefId).ToList()
            };
            return View(chefnDish);
        }

        [HttpGet("/Chef/New")]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost("/Chef/Create")]
        public IActionResult Create(Chef newChef)
        {
            if (ModelState.IsValid == false)
            {
                return View("New");                     //Just "New" - already in Chef
            }
            // ModalState.IsValid...
            // newDish.CreatedAt = DateTime.Now;        //works, but moved to Model's instantiation
            db.Chefs.Add(newChef);                      //DB Insert
            db.SaveChanges();
            return RedirectToAction("Index");
            // return RedirectToAction("Details", newDish.DishId);  //"Details", new {id = newDish.DishId}
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}