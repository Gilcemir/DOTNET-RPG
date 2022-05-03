using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace DOTNET_RPG.Services.FighterService
{
    public class FighterService : IFighterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public readonly IHttpContextAccessor _httpContextAccessor;

        public FighterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetFighterDto>>> AddFighter(AddFighterDto newFighter)
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            Fighter fighter = _mapper.Map<Fighter>(newFighter);
            fighter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Fighters.Add(fighter);
            await _context.SaveChangesAsync();//write the changes in the db and also generates new ID
            serviceResponse.Data = await _context.Fighters
                                                .Where(c => c.User.Id == GetUserId())
                                                .Select(c => _mapper.Map<GetFighterDto>(c))
                                                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFighterDto>>> GetAllFighters()
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            var dbFighters = await _context.Fighters
                                    .Include(c => c.Chant)
                                    .Include(c => c.Skills)
                                    .Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbFighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> GetFighterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();

            try
            {
                var dbFighter = await _context.Fighters
                                    .Include(c => c.Chant)
                                    .Include(c => c.Skills)
                                    .FirstAsync(f => f.Id == id && f.User.Id == GetUserId());
                serviceResponse.Data = _mapper.Map<GetFighterDto>(dbFighter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> UpdateFighter(UpdateFighterDto updatedFighter)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();
            try
            {
                Fighter fighter = await _context.Fighters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedFighter.Id);
                if (fighter.User.Id == GetUserId())
                {
                    fighter.Name = updatedFighter.Name;
                    fighter.HitPoints = updatedFighter.HitPoints;
                    fighter.Takedown = updatedFighter.Takedown;
                    fighter.Submission = updatedFighter.Submission;
                    fighter.Defense = updatedFighter.Defense;
                    fighter.Strike = updatedFighter.Strike;
                    fighter.Origin = updatedFighter.Origin;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetFighterDto>(fighter);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Fighter not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetFighterDto>> UpdateFighterById(AddFighterDto updatedFighter, int id)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();
            try
            {
                Fighter fighter = await _context.Fighters
                                .Include(c => c.User)
                                .FirstOrDefaultAsync(c => c.Id == id);
                if (fighter.User.Id == GetUserId())
                {
                    fighter.Name = updatedFighter.Name;
                    fighter.HitPoints = updatedFighter.HitPoints;
                    fighter.Takedown = updatedFighter.Takedown;
                    fighter.Submission = updatedFighter.Submission;
                    fighter.Defense = updatedFighter.Defense;
                    fighter.Strike = updatedFighter.Strike;
                    fighter.Origin = updatedFighter.Origin;

                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _mapper.Map<GetFighterDto>(fighter);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Fighter not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFighterDto>>> DeleteFighter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            try
            {
                Fighter fighter = await _context.Fighters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (fighter != null)
                {
                    _context.Fighters.Remove(fighter);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = _context.Fighters
                                            .Where(c => c.User.Id == GetUserId())
                                            .Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Fighter not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> AddFighterSkill(AddFighterSkillDto newFighterSkill)
        {
            var response = new ServiceResponse<GetFighterDto>();
            try
            {
                var fighter = await _context.Fighters
                                    .Include(c => c.Chant)
                                    .Include(c => c.Skills)
                                    .FirstOrDefaultAsync(c => c.Id == newFighterSkill.FighterId && c.User.Id == GetUserId());

                if (fighter == null)
                {
                    response.Success = false;
                    response.Message = "Fighter not found.";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newFighterSkill.SkillId);

                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }

                fighter.Skills.Add(skill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetFighterDto>(fighter);
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