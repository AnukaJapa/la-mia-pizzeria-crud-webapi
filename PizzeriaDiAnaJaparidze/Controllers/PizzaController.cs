using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.SqlServer.Server;
using PizzeriaDiAnaJaparidze.Database;
using PizzeriaDiAnaJaparidze.Models;

namespace PizzeriaDiAnaJaparidze.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> pizzaList = db.Pizzas.OrderBy(x=>x.Id).ToList<Pizza>();
             
                return View("Index", pizzaList);
            }
        }

        public IActionResult Details(int id)
        {
           using (PizzeriaContext db = new PizzeriaContext()){
               
                Pizza pizzaTrovata = db.Pizzas
                   .Where(p => p.Id == id).Include(pizza => pizza.Category)
                   .FirstOrDefault();

                if (pizzaTrovata != null)
                {
                    return View(pizzaTrovata);
                }

                return NotFound("la pizza con l'id cercato non esiste!");
            }

        }
      

        [HttpGet]
        public IActionResult Create()
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                List<Category> categoriesFromDB = db.Categories.ToList<Category>();
                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = new Pizza();
                modelForView.Categories = categoriesFromDB;
                return View("Create", modelForView);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategoriesView formData)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                if (!ModelState.IsValid)
                {
                    List<Category> categories = db.Categories.ToList<Category>();
                    formData.Categories = categories;
                    return View("Create", formData);

                }
                else
                {
                    db.Pizzas.Add(formData.Pizza);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");

            }
        }



        [HttpGet]

        public IActionResult Update(int id)
        {
            using(PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaToUpdate = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (pizzaToUpdate == null)
                {
                    return NotFound("la pizza non è stato trovato");
                }

                List<Category> categories = db.Categories.ToList<Category>();
                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = pizzaToUpdate;
                modelForView.Categories = categories;
 
                return View("Update", modelForView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Update(int id, PizzaCategoriesView formPizza)
        {
            if (!ModelState.IsValid)
            {
                using(PizzeriaContext db= new PizzeriaContext())
                {
                    List<Category> categories = db.Categories.ToList<Category>();
                    formPizza.Categories = categories;
                }
                return View("Update", formPizza);
            }
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaToUpdate = db.Pizzas.Where(p=>p.Id == id).FirstOrDefault();

                if (pizzaToUpdate != null)
                {
                    pizzaToUpdate.Title = formPizza.Pizza.Title;
                    pizzaToUpdate.Description = formPizza.Pizza.Description;
                    pizzaToUpdate.Image = formPizza.Pizza.Image;
                    pizzaToUpdate.CategoryId = formPizza.Pizza.CategoryId;


                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post che volevi modificare non è stato trovato!");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza pizzaToDelete = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Il post da eliminare non è stato trovato!");
                }
            }
        }

    }
}
