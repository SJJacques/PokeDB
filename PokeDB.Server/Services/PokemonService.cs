using Microsoft.EntityFrameworkCore;
using PokeDB.Server.Data;
using PokeDB.Server.Models;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;

namespace PokeDB.Server.Services
{
    public class PokemonService : ICrudService<PokemonDto>
    {
        private readonly PokeDbContext _context;

        public PokemonService(PokeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PokemonDto>> GetAllAsync()
        {
            return await _context.Pokemon
                .Include(p => p.Type)
                .Include(p => p.Ability)
                .Include(p => p.Moves)
                .Include(p => p.PreviousEvolution)
                .Select(p => new PokemonDto
                {
                    Id = p.Id,
                    PokemonName = p.PokemonName,
                    BaseHp = p.BaseHp,
                    BaseAttack = p.BaseAttack,
                    BaseDefense = p.BaseDefense,
                    TypeName = p.Type.TypeName,
                    AbilityName = p.Ability.AbilityName,
                    MoveNames = p.Moves.Select(m => m.MoveName).ToList(),
                    PreviousEvolutionId = p.PreviousEvolutionId,
                    PreviousEvolutionName = p.PreviousEvolution != null ? p.PreviousEvolution.PokemonName : null
                })
                .ToListAsync();
        }

        public async Task<PokemonDto?> GetByIdAsync(int id)
        {
            var p = await _context.Pokemon
                .Include(p => p.Type)
                .Include(p => p.Ability)
                .Include(p => p.Moves)
                .Include(p => p.PreviousEvolution)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (p == null) return null;

            return new PokemonDto
            {
                Id = p.Id,
                PokemonName = p.PokemonName,
                BaseHp = p.BaseHp,
                BaseAttack = p.BaseAttack,
                BaseDefense = p.BaseDefense,
                TypeName = p.Type.TypeName,
                AbilityName = p.Ability.AbilityName,
                MoveNames = p.Moves.Select(m => m.MoveName).ToList(),
                PreviousEvolutionId = p.PreviousEvolutionId,
                PreviousEvolutionName = p.PreviousEvolution?.PokemonName
            };
        }

        public async Task<PokemonDto?> CreateAsync(PokemonDto dto)
        {
            var pokemon = new Pokemon
            {
                PokemonName = dto.PokemonName,
                BaseHp = dto.BaseHp,
                BaseAttack = dto.BaseAttack,
                BaseDefense = dto.BaseDefense,
                TypeId = await GetTypeIdByName(dto.TypeName),
                AbilityId = await GetAbilityIdByName(dto.AbilityName),
                PreviousEvolutionId = dto.PreviousEvolutionId
            };

            pokemon.Moves = await _context.Moves
                .Where(m => dto.MoveNames.Contains(m.MoveName))
                .ToListAsync();

            _context.Pokemon.Add(pokemon);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(pokemon.Id);
        }

        public async Task<bool> UpdateAsync(int id, PokemonDto dto)
        {
            var pokemon = await _context.Pokemon
                .Include(p => p.Moves)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pokemon == null) return false;

            pokemon.PokemonName = dto.PokemonName;
            pokemon.BaseHp = dto.BaseHp;
            pokemon.BaseAttack = dto.BaseAttack;
            pokemon.BaseDefense = dto.BaseDefense;
            pokemon.TypeId = await GetTypeIdByName(dto.TypeName);
            pokemon.AbilityId = await GetAbilityIdByName(dto.AbilityName);
            pokemon.PreviousEvolutionId = dto.PreviousEvolutionId;

            pokemon.Moves = await _context.Moves
                .Where(m => dto.MoveNames.Contains(m.MoveName))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null) return false;

            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<int> GetTypeIdByName(string typeName)
        {
            var type = await _context.Types.FirstOrDefaultAsync(t => t.TypeName == typeName);
            return type?.Id ?? throw new ArgumentException($"Type '{typeName}' not found.");
        }

        private async Task<int> GetAbilityIdByName(string abilityName)
        {
            var ability = await _context.Abilities.FirstOrDefaultAsync(a => a.AbilityName == abilityName);
            return ability?.Id ?? throw new ArgumentException($"Ability '{abilityName}' not found.");
        }
    }
}
