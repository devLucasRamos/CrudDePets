using ViewModel;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Models;
using System;

namespace Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class PetsController : ControllerBase
    {
        [HttpGet]
        [Route(template: "Pets")]
        public IActionResult Get([FromServices] AppDbContext context)
        {
            var pets = context.Pets.ToList();
            return Ok(pets);
        }

        [HttpGet]
        [Route(template: "Pets/{Id}")]
        public IActionResult GetbyId([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var pet = context.Pets.FirstOrDefault(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            return Ok(pet);
        }

        [HttpPost]
        [Route(template: "Pets")]
        public IActionResult Post([FromServices] AppDbContext context, [FromBody] CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var pet = new Pets
                {
                    DonoNome = model.DonoNome,
                    Nome = model.Nome,
                    TipoDePet = model.TipoDePet,
                    Idade = model.Idade,
                    Cor = model.Cor
                };
                try
                {
                    context.Add(pet);
                    context.SaveChanges();
                    return Created(uri: $"v1/Pets/{pet.Id}", pet);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
        }

        [HttpDelete]
        [Route(template: "Pets/{Id}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var pet = context.Pets.FirstOrDefault(x => x.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            context.Pets.Remove(pet);
            context.SaveChanges();
            return Ok();
        }
    }
}
