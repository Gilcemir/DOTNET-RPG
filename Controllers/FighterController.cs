using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.FighterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<ServiceResponse<List<GetFighterDto>>>> Get()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _fighterService.GetAllFighters(id));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetFighterDto>>> GetByID(int Id)
        {
            var fighter = await _fighterService.GetFighterById(Id);

            if (fighter.Data == null)
            {
                return NotFound(fighter);
            }
            return Ok(fighter);
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetFighterDto>>>> AddFighter(AddFighterDto newFighter)
        {
            return Ok(await _fighterService.AddFighter(newFighter));
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ServiceResponse<GetFighterDto>>> UpdateFighterById(AddFighterDto updatedFighter, int Id)
        {
            var response = await _fighterService.UpdateFighterById(updatedFighter, Id); 
            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetFighterDto>>> UpdateFighterDto(UpdateFighterDto updatedFighter)
        {
            var response = await _fighterService.UpdateFighter(updatedFighter);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ServiceResponse<List<GetFighterDto>>>> Delete(int Id)
        {
            var response = await _fighterService.DeleteFighter(Id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}