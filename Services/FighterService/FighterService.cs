using AutoMapper;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using System.Linq;

namespace DOTNET_RPG.Services.FighterService
{
    public class FighterService : IFighterService
    {
        private static List<Fighter> fighters = new List<Fighter>{
            new Fighter{Id = 0, Name = "Jose "},
            new Fighter {Id = 1, Name = "Gil"},
            new Fighter {Id = 200, Name = "Arthur"}
            };
        private readonly IMapper _mapper;

        public FighterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetFighterDto>>> AddFighter(AddFighterDto newFighter)
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            Fighter fighter = _mapper.Map<Fighter>(newFighter);
            fighter.Id = fighters.Max(c => c.Id) + 1; 
            fighters.Add(fighter);
            serviceResponse.Data = fighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetFighterDto>>> GetAllFighters()
        {
            var serviceResponse = new ServiceResponse<List<GetFighterDto>>();
            serviceResponse.Data = fighters.Select(c => _mapper.Map<GetFighterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> GetFighterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();
            serviceResponse.Data = _mapper.Map<GetFighterDto>(fighters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetFighterDto>> UpdateFighter(UpdateFighterDto updatedFighter)
        {
            var serviceResponse = new ServiceResponse<GetFighterDto>();
            try
            {
                Fighter fighter = fighters.FirstOrDefault(c => c.Id == updatedFighter.Id);

                fighter.Name = updatedFighter.Name;
                fighter.HitPoints = updatedFighter.HitPoints;
                fighter.Takedown = updatedFighter.Takedown;
                fighter.Submission = updatedFighter.Submission;
                fighter.Defense = updatedFighter.Defense;
                fighter.Strike = updatedFighter.Strike;
                fighter.Origin = updatedFighter.Origin;

                serviceResponse.Data = _mapper.Map<GetFighterDto>(fighter);
            }catch(Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}