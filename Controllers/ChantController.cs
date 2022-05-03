using DOTNET_RPG.Dtos.Chant;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using DOTNET_RPG.Services.ChantService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_RPG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ChantController : ControllerBase
    {
        private readonly IChantService _chantService;
        public ChantController(IChantService chantService)
        {
            _chantService = chantService;

        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetFighterDto>>> AddChant(AddChantDto newChant)
        {
            return Ok(await _chantService.AddChant(newChant));
        }
    }
}