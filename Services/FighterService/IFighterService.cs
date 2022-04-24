using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FighterService
{
    public interface IFighterService
    {
         Task<ServiceResponse<List<Fighter>>> GetAllFighters();
         Task<ServiceResponse<Fighter>> GetFighterById(int id);

         Task<ServiceResponse<List<Fighter>>> AddFighter(Fighter newFighter);
    }
}