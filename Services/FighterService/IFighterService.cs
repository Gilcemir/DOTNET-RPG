using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FighterService
{
    public interface IFighterService
    {
         Task<List<Fighter>> GetAllFighters();
         Task<Fighter> GetFighterById(int id);

         Task<List<Fighter>> AddFighter(Fighter newFighter);
    }
}