using DOTNET_RPG.Dtos.Chant;
using DOTNET_RPG.Models;

namespace DOTNET_RPG.Dtos.Fighter
{
    public class GetFighterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Random";
        public int HitPoints { get; set; } = 100;
        public int Takedown { get; set; } = 0;
        public int Submission { get; set; } = 0;
        public int Defense { get; set; } = 0;
        public int Strike { get; set; } = 0;
        public Country Origin { get; set; } = Country.Brazil;
        public GetChantDto Chant { set; get; }
    }
}