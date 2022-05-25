using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private List<SuperHero> Heroes = new List<SuperHero>
        {
            new SuperHero {
                Id = 2,
                Name = "Iron Man",
                FirstName = "Tony",
                LastName = "Stark"}

        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {            
            return Ok(await _context.SuperHeroes.ToListAsync()); 
        }

        [HttpGet("id")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero== null)
            {
                return BadRequest("Hero not found");
                
            }
            return Ok(hero);
              
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero Hero)
        {
            _context.SuperHeroes.Add(Hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero Hero)
        {
            var hero = await _context.SuperHeroes.FindAsync(Hero.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found");

            }
            hero.FirstName = Hero.FirstName;
            hero.LastName = Hero.LastName;
            hero.Name = Hero.Name;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");

            }
            _context.SuperHeroes.Remove(hero);
            _context.SaveChanges();
            return Ok(await _context.SuperHeroes.ToListAsync());

        }
    }
}
