using Microsoft.EntityFrameworkCore;
using PokeDB.Server.Models;
using Type = PokeDB.Server.Models.Type;

namespace PokeDB.Server.Data
{
    public class PokeDbContext : DbContext
    {
        public DbSet<Ability> Abilities { get; set; } = null!;
        public DbSet<Move> Moves { get; set; } = null!;
        public DbSet<Pokemon> Pokemon { get; set; } = null!;
        public DbSet<Type> Types { get; set; } = null!;

        public PokeDbContext(DbContextOptions<PokeDbContext> options) : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>()
                .HasMany(p => p.Moves)
                .WithMany(m => m.Pokemon)
                .UsingEntity("PokemonMoves",
                    l => l.HasOne(typeof(Move)).WithMany().HasForeignKey("MoveId"),
                    r => r.HasOne(typeof(Pokemon)).WithMany().HasForeignKey("PokemonId"));
        }
    }
}
