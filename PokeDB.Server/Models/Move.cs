namespace PokeDB.Server.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string MoveName { get; set; } = null!;
        public int AttackPower { get; set; }
        public int Accuracy { get; set; }
        
        public int TypeId { get; set; }
        public Type Type { get; set; } = null!;

        public ICollection<Pokemon> Pokemon { get; set; } = new List<Pokemon>();
    }
}
