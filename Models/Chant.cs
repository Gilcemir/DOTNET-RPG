namespace DOTNET_RPG.Models
{
    public class Chant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public Fighter Fighter { get; set; }
        public int FighterId { get; set; }
    }
}