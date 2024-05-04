using firstapp.Data;
using firstapp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult <List<SuperHero>>> GetAllHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();            
            
            //new List<SuperHero> {
               // new SuperHero
               // {
                  //  Id = 1,
                  //  Name = "Spiderman",
                 //   FirstName = "Peter", 
                   // LastName = "Parker",
                   // Place = "NewYork",
             //}
            
            return Ok(heroes);
        }

     // rechercher un hero a trvavers son ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero is null)
                return NotFound("Hero is not found!");
    

            return Ok(hero);
        }

        // ajouter un hero 
        // httpPost permet d'ajouter une nouvellle entité dans la BD
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
             _context.SuperHeroes.Add(hero);
            //savechange permet  de socker dans la base de données ce que l'on fait.
                await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
     
       }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updateHero)
        { 
            //FindAsync permet de rechercher de manière asynchrone les id 
             var dbhero = await _context.SuperHeroes.FindAsync(updateHero.Id);
            if (dbhero is null)
                return NotFound("Hero is not found!");
            
            dbhero.Name = updateHero.Name;
            dbhero.FirstName = updateHero.FirstName;
            dbhero.LastName = updateHero.LastName;
            dbhero.Place = updateHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id) 
        {
            //FindAsync permet de rechercher de manière asynchrone les id 
            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero is null)
                return NotFound("Hero is not found!");
           
            _context .SuperHeroes.Remove(dbhero);

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
