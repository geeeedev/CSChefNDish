using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSChefNDish.Models;
using Microsoft.AspNetCore.Mvc;
// using CSChefNDish.Models;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Http;
// HttpContext.Session.SetString("key","")/.GetString("key")/.GetInt32("key",)/.GetInt32("key")
// HttpContext.Session.Clear();
// TempData["var"] = "";  //persist across one redirect only


namespace CSChefNDish.Controllers
{
    public class DishesController : Controller
    {
        private dbChefNDishContext db;
        public DishesController(dbChefNDishContext dbContext)
        {
            db = dbContext;
        }

        [HttpGet("/Dishes")]                         //overrides the ones in Home Controller ?? 
        public IActionResult All()
        {
            // List<Dish> dishes = db.Dishes.Reverse().ToList();
            List<Dish> dishes = db.Dishes
                .Include(d => d.dishChef)
                .OrderByDescending(d => d.CreatedAt).ToList();
            return View(dishes);
        }

        [HttpGet("/Dish/New")]  
        public IActionResult New()
        {
            ChefNDish chefnDish = new ChefNDish()
            {
                NewDish = new Dish(),
                ListOfChefs = db.Chefs.ToList()
            };

            return View(chefnDish);
        }

        [HttpPost("/Dish/Create")]  
        public IActionResult Create(ChefNDish newChefNDish)                                     //Dish newDish    //param retrieval MUST match @model passback!
        
        {
            if (ModelState.IsValid == false)
            {
                // return View("New");                                                          //not enough, the Dish/New view also needs ListOfChefs
                newChefNDish.ListOfChefs = db.Chefs.ToList();
                return View("New",newChefNDish);
            }
            // ModalState.IsValid...
            //db.Dishes.Add(newChefNDish.NewDish);                                          //DB Insert
            db.Dishes.Add(newChefNDish.NewDish);
            db.SaveChanges();
            return RedirectToAction("All");                                         //"Details", new {id = newDish.DishId}
        }























    //     //defaultMVCRoute: /Dish/Details/4
    //     //exploring "id" as route param
    //     public IActionResult Details(int id)
    //     {
    //         Dish selectedDish = db.Dishes.FirstOrDefault(d => d.DishId == id);
    //         if (selectedDish == null)
    //         {
    //             return RedirectToAction("Index");
    //         }
    //         return View(selectedDish);
    //     }

    //     //defaultMVCRoute: /Dish/Edit/1
    //     //HttpRoute takes precedence: /Home/1/Edit
    //     //exploring "id" as route param
    //     //exploring HttpRoute - apply this route when avail, without this use default MVCRoute
    //     [HttpGet("/Dish/{id}/Edit")]
    //     public IActionResult Edit(int id)
    //     {
    //         Dish selectedDish = db.Dishes.FirstOrDefault(d => d.DishId == id);
    //         if (selectedDish == null)
    //         {
    //             return RedirectToAction("Index");
    //         }
    //         return View(selectedDish);
    //     }


    //     // RESTful route: /Dish/1/Update
    //     // exploring "id" as route param
    //     // [HttpPost("/Dish/{dishId}/Update")]     //dishId  //id = "This page isn't working"
    //     public IActionResult Update(Dish updatedDish, int dishId)       
    //     {
    //         if (ModelState.IsValid == false)
    //         {
    //             return View("Edit", updatedDish);  //correct way - get the new updated model with errors displayed
    //          // return RedirectToAction("Edit", dishId);        //??? why wouldnt this work - breakpoint shows updatedDish.DishId has a valid id
    //          //??? want to learn this error troubleshoot - I know logically the View version makes more sense 
    //         }

    //         // updatedDish.DishId only works if asp-route- on <form> use asp-route-dishId, 
    //         // frmwk knows to match it using asp-route-dishId withOUT the (... , int dishId) input param
    //         //Dish dbDish = db.Dishes.FirstOrDefault(d => d.DishId == updatedDish.DishId);    
    //         Dish dbDish = db.Dishes.FirstOrDefault(d => d.DishId == dishId);
    //         if (dbDish == null)
    //         {
    //             return RedirectToAction("Index");
    //         }
    //         dbDish.Name = updatedDish.Name;
    //         dbDish.Chef = updatedDish.Chef;
    //         dbDish.Description = updatedDish.Description;
    //         dbDish.Calories = updatedDish.Calories;
    //         dbDish.Tastiness = updatedDish.Tastiness;
    //         dbDish.UpdatedAt = DateTime.Now;
            
    //         db.Dishes.Update(dbDish);           // NOT Update(updatedDish) - it would override CreatedAt timestamp as well.
    //         db.SaveChanges();
    //         return RedirectToAction("Details",new{id = dbDish.DishId});     //RedirectToAction MUST HAVE object route param format in dict format if included, hence new{ x = xxx}   
    //         // return RedirectToAction("/Home/Details/{dishId}", dishId);   //- Err: No route matches the supplied values
    //         // return View("Details",dbDish);               //works
    //         // return RedirectToAction("Index");            //works, not good enough
    //         // return View("test", updatedDish.DishId);     //works
    //     }

    // //defaultMVCRoute: /Dish/Delete/4  
    //     //exploring "id" as route param
    //     public IActionResult Delete(int id)
    //     {
    //         // not checking null here - not needed - Default would be null if not found, will send to Index anyway
    //         db.Dishes.Remove(db.Dishes.FirstOrDefault(d => d.DishId == id));
    //         db.SaveChanges();
    //         return RedirectToAction("Index");
    //     }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
