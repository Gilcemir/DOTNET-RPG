namespace DOTNET_RPG.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }

        public List<Fighter> Fighters { get; set; }
    }
}