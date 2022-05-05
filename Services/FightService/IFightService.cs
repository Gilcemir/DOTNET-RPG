using DOTNET_RPG.Dtos.Fight;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Services.FightService
{
    public interface IFightService 
    {
         Task<ServiceResponse<AttackResultDto>> ChantAttack(ChantAttackDto request);
         Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
         Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
         Task<ServiceResponse<List<HighScoreDto>>> GetHighScore();
    }
}