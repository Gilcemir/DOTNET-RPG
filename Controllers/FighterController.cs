using DOTNET_RPG.Models;
using DOTNET_RPG.Services.FighterService;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
namespace DOTNET_RPG.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FighterController : ControllerBase
    {

        private readonly IFighterService _fighterService;

        public FighterController(IFighterService fighterService)
        {
            _fighterService = fighterService;
            
        }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Fighter>>>> Get()
    {
        return Ok(await _fighterService.GetAllFighters());

    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ServiceResponse<Fighter>>> GetByID(int Id)
    {
        var fighter = _fighterService.GetFighterById(Id);
        if (fighter == null)
        {
            return NotFound();
        }
        return Ok(await fighter);
    }
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<Fighter>>>> AddFighter(Fighter newFighter)
    {
        return Ok(await _fighterService.AddFighter(newFighter));
    }


}
}