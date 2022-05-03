using DOTNET_RPG.Dtos.Chant;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.ChantService
{
    public interface IChantService
    {
         Task<ServiceResponse<GetFighterDto>> AddChant(AddChantDto newChant);
    }
}