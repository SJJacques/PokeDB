namespace PokeDB.Server.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string PokemonName { get; set; } = null!;
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        
        public int TypeId { get; set; }
        public Type Type { get; set; } = null!;
        
        public int AbilityId { get; set; }
        public Ability Ability { get; set; } = null!;

        public ICollection<Move> Moves { get; set; } = new List<Move>();

        public int? PreviousEvolutionId { get; set; }
        public Pokemon? PreviousEvolution { get; set; }
    }
}
