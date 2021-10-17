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
        public IActionResult Post([FromServices] AppDbContext context, [FromBody] PetsViewModel model)
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
        [HttpPut]
        [Route(template: "Pets/{id}")]
        public IActionResult Put([FromServices] AppDbContext context,
            [FromBody] PetsViewModel model, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                var pet = context.Pets.FirstOrDefault(x => x.Id == id);
                if (pet == null)
                {
                    return NotFound();
                }
                try
                {
                    pet.DonoNome = model.DonoNome == null? pet.DonoNome : model.DonoNome;
                    pet.Nome = model.Nome == null? pet.Nome : model.Nome;
                    pet.TipoDePet = model.TipoDePet == null? pet.TipoDePet : model.TipoDePet;
                    pet.Idade = model.Idade == 0? pet.Idade : model.Idade;
                    pet.Cor = model.Cor == null? pet.Cor : model.Cor;
                    context.Update(pet);
                    context.SaveChanges();
                    return Ok(pet);
                }
                catch (Exception)
                {
                    return NotFound();
                }
            }
        }
    }
}
