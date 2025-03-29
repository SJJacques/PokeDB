namespace PokeDB.Server.Models
{
    public class Type
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = null!;

        public ICollection<Move> Moves { get; set; } = new List<Move>();
        public ICollection<Pokemon> Pokemon { get; set; } = new List<Pokemon>();
    }
}
