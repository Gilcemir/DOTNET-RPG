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
        
        public async Task<List<Fighter>> AddFighter(Fighter newFighter)
        {
            fighters.Add(newFighter);
            return fighters;
        }

        public async Task<List<Fighter>> GetAllFighters() => fighters;

        public async Task<Fighter> GetFighterById(int id) => fighters.FirstOrDefault(c => c.Id == id);
    }
}