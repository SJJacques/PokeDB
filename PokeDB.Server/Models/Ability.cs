namespace PokeDB.Server.Models
{
    public class Ability
    {
        public int Id { get; set; }
        public string AbilityName { get; set; } = null!;

        public ICollection<Pokemon> Pokemon { get; set; } = new List<Pokemon>();
    }
}
