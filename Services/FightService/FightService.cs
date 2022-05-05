using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Fight;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        public FightService(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<AttackResultDto>> ChantAttack(ChantAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Fighters
                                .Include(c=> c.Chant)
                                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                
                var opponent = await _context.Fighters
                                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);


                int damage = attacker.Chant.Damage + (new Random().Next(attacker.Strike));
                damage -= new Random().Next(opponent.Defense);
                damage = damage < 0 ? 0 : damage;
                if(damage > 0)
                    opponent.HitPoints -= damage;
                
                if(opponent.HitPoints <=0)
                    response.Message = $"{opponent.Name} has been defeated!";
                
                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}