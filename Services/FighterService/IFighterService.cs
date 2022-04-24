using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FighterService
{
    public interface IFighterService
    {
         Task<ServiceResponse<List<GetFighterDto>>> GetAllFighters();
         Task<ServiceResponse<GetFighterDto>> GetFighterById(int id);

         Task<ServiceResponse<List<GetFighterDto>>> AddFighter(AddFighterDto newFighter);
    }
}