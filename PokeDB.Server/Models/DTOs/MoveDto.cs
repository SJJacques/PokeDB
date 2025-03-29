namespace PokeDB.Server.Models.DTOs
{
    public class MoveDto
    {
        public int Id { get; set; }
        public string MoveName { get; set; } = null!;
        public int Power { get; set; }
        public int Accuracy { get; set; }

        public string TypeName { get; set; } = null!;
    }
}
