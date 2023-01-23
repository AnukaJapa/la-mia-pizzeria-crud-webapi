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
        public IActionResult Get()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> articoli = db.Pizzas.ToList<Pizza>();
                return Ok(articoli);
            }

        }

    }
}

