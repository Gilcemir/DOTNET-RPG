using DOTNET_RPG.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace DOTNET_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FighterController : ControllerBase
    {
        private static List<Fighter> fighters = new List<Fighter>{
            new Fighter{id = 0, Name = "Jose "},
            new Fighter {id = 1, Name = "Gil"},
            new Fighter {id = 200, Name = "Arthur"}
            };
        
        [HttpGet]
        public ActionResult<Fighter> Get()
        {

            return Ok(fighters);
        }

        [HttpGet("{id}")]
        public ActionResult<Fighter> GetByID(int id)
        {
            var fighter = fighters.FirstOrDefault(c => c.id == id);
            if(fighter  == null)
            {
                return NotFound();
            }
            return Ok(fighter);
        }
        [HttpPost]
        public ActionResult<List<Fighter>> AddFighter(Fighter newFighter)
        {
            fighters.Add(newFighter);
            return Ok(fighters);
        }


    }
}