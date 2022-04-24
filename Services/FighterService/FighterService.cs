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

        public async Task<ServiceResponse<List<Fighter>>> AddFighter(Fighter newFighter)
        {
            var serviceResponse = new ServiceResponse<List<Fighter>>();
            fighters.Add(newFighter);
            serviceResponse.Data = fighters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Fighter>>> GetAllFighters()
        {
            var serviceResponse = new ServiceResponse<List<Fighter>>();
            serviceResponse.Data = fighters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Fighter>> GetFighterById(int id)
        {
            var serviceResponse = new ServiceResponse<Fighter>();
            serviceResponse.Data = fighters.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }
    }
}