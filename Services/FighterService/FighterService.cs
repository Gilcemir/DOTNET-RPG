using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DOTNET_RPG.Services.FighterService
{
    public class FighterService : IFighterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public FighterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetFighterDto>>> AddFighter(AddFighterDto newFighter)
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            Fighter fighter = _mapper.Map<Fighter>(newFighter);      
            _context.Fighters.Add(fighter);
            await _context.SaveChangesAsync();//write the changes in the db and also generates new ID
            serviceResponse.Data = await _context.Fighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFighterDto>>> GetAllFighters(int userId)
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            var dbFighters = await _context.Fighters.Where(c => c.User.Id == userId).ToListAsync();
            serviceResponse.Data = dbFighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> GetFighterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();
            
            try
            {
                var dbFighter = await _context.Fighters.FirstAsync(c => c.Id == id);
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
                Fighter fighter = await _context.Fighters.FirstOrDefaultAsync(c => c.Id == updatedFighter.Id);

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
                Fighter fighter = await _context.Fighters.FirstOrDefaultAsync(c => c.Id == id);

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
                Fighter fighter = await _context.Fighters.FirstAsync(c => c.Id == id);
                _context.Fighters.Remove(fighter);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _context.Fighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}