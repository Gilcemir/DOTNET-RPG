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
                                .Include(c => c.Chant)
                                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Fighters
                                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                int damage = DoChantAttack(attacker, opponent);

                if (opponent.HitPoints <= 0)
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
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int DoChantAttack(Fighter attacker, Fighter opponent)
        {
            int damage = attacker.Chant.Damage + (new Random().Next(attacker.Strike));
            damage -= new Random().Next(opponent.Defense);
            damage = damage < 0 ? 0 : damage;
            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Fighters
                                .Include(c => c.Skills)
                                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Fighters
                                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);


                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know this skill.";
                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, skill);

                if (opponent.HitPoints <= 0)
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
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int DoSkillAttack(Fighter attacker, Fighter opponent, Skill skill)
        {
            int damage = skill.Damage + (new Random().Next(attacker.Takedown));
            damage -= new Random().Next(opponent.Defense);
            damage = damage < 0 ? 0 : damage;
            if (damage > 0)
                opponent.HitPoints -= damage;
            return damage;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var fighters = await _context.Fighters
                                .Include(f => f.Chant)
                                .Include(f => f.Skills)
                                .Where(f => request.FighterIds.Contains(f.Id)).ToListAsync();

                bool defeated = false;

                while(!defeated)
                {
                    foreach(var attacker in fighters)
                    {
                        var opponents = fighters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useChant = new Random().Next(2) == 0;
                        if(useChant)
                        {
                            attackUsed = attacker.Chant.Name;
                            damage = DoChantAttack(attacker, opponent);
                        }
                        else
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed =skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }

                        response.Data.Log  
                                    .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {damage} damage");
                        if(opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defeated!");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }
                fighters.ForEach(c =>
                       { c.Fights++;
                        c.HitPoints = 100;
                       });
                       await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

    }
}