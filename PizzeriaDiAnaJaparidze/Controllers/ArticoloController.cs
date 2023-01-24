using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PizzeriaDiAnaJaparidze.Database;
using PizzeriaDiAnaJaparidze.Models;

namespace PizzeriaDiAnaJaparidze.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticoloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string? search)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> articoli = new List<Pizza>();
                if (search is null || search == "")
                {
                    articoli = db.Pizzas.ToList<Pizza>();
                }
                else
                {
                    articoli = db.Pizzas.Where(p => p.Title.ToLower().Contains(search.ToLower())).ToList<Pizza>();
                }

                return Ok(articoli);
            }

        }
 //---------------------------------------------------------------------------------------

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza articolo = db.Pizzas.Where(p => p.Id == id).FirstOrDefault();

                if (articolo is null)
                {
                    return NotFound("L'articolo non è stato trovato con questo id");
                }

                return Ok(articolo);
            }

//---------------------------------------------------------------------------------------
        }


    }

}
