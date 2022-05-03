using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FighterService
{
    public interface IFighterService
    {
         Task<ServiceResponse<List<GetFighterDto>>> GetAllFighters();
         Task<ServiceResponse<GetFighterDto>> GetFighterById(int id);
         Task<ServiceResponse<List<GetFighterDto>>> AddFighter(AddFighterDto newFighter);
         Task<ServiceResponse<GetFighterDto>> UpdateFighter(UpdateFighterDto updatedFighter);
         Task<ServiceResponse<List<GetFighterDto>>> DeleteFighter(int id);
         Task<ServiceResponse<GetFighterDto>> UpdateFighterById(AddFighterDto updatedFighter, int id); //AddFighterDto because its the same atributtes 
         Task<ServiceResponse<GetFighterDto>> AddFighterSkill(AddFighterSkillDto newFighterSkill);
    }
}