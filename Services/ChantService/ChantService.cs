using System.Security.Claims;
using AutoMapper;
using DOTNET_RPG.Data;
using DOTNET_RPG.Dtos.Chant;
using DOTNET_RPG.Dtos.Fighter;
using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Services.ChantService
{
    public class ChantService : IChantService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ChantService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ServiceResponse<GetFighterDto>> AddChant(AddChantDto newChant)
        {
            var response = new ServiceResponse<GetFighterDto>();
            try
            {
                var fighter = await _context.Fighters
                            .FirstOrDefaultAsync(c => c.Id == newChant.FighterId 
                            && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

                if(fighter == null)
                {
                    response.Success = false;
                    response.Message = "Fighter not found.";
                    return response;
                }

                var chant = new Chant
                {
                    Name = newChant.Name,
                    Damage = newChant.Damage,
                    Fighter = fighter
                };
                _context.Chants.Add(chant);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetFighterDto>(fighter);

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