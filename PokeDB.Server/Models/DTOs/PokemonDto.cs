namespace PokeDB.Server.Models.DTOs
{
    public class PokemonDto
    {
        public int Id { get; set; }
        public string PokemonName { get; set; } = null!;
        public int BaseHp { get; set; }
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }

        public string TypeName { get; set; } = null!;
        public string AbilityName { get; set; } = null!;
        public List<string> MoveNames { get; set; } = new();
        public int? PreviousEvolutionId { get; set; }
        public string? PreviousEvolutionName { get; set; }
    }
}
