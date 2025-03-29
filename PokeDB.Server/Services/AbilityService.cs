using PokeDB.Server.Data;
using PokeDB.Server.Models;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PokeDB.Server.Services
{
    public class AbilityService : ICrudService<AbilityDto>
    {
        private readonly PokeDbContext _context;

        public AbilityService(PokeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AbilityDto>> GetAllAsync()
        {
            return await _context.Abilities
                .Select(a => new AbilityDto
                {
                    Id = a.Id,
                    AbilityName = a.AbilityName
                })
                .ToListAsync();
        }

        public async Task<AbilityDto?> GetByIdAsync(int id)
        {
            var ability = await _context.Abilities
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ability == null) return null;

            return new AbilityDto
            {
                Id = ability.Id,
                AbilityName = ability.AbilityName
            };
        }

        public async Task<AbilityDto?> CreateAsync(AbilityDto dto)
        {
            var ability = new Ability
            {
                AbilityName = dto.AbilityName
            };

            _context.Abilities.Add(ability);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(ability.Id);
        }

        public async Task<bool> UpdateAsync(int id, AbilityDto dto)
        {
            var ability = await _context.Abilities.FindAsync(id);
            if (ability == null) return false;

            ability.AbilityName = dto.AbilityName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ability = await _context.Abilities.FindAsync(id);
            if (ability == null) return false;

            _context.Abilities.Remove(ability);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
