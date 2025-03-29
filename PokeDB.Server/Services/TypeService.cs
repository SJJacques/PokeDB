using Microsoft.EntityFrameworkCore;
using PokeDB.Server.Data;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;
using Type = PokeDB.Server.Models.Type;

namespace PokeDB.Server.Services
{
    public class TypeService : ICrudService<TypeDto>
    {
        private readonly PokeDbContext _context;

        public TypeService(PokeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TypeDto>> GetAllAsync()
        {
            return await _context.Types
                .Select(t => new TypeDto
                {
                    Id = t.Id,
                    TypeName = t.TypeName
                })
                .ToListAsync();
        }

        public async Task<TypeDto?> GetByIdAsync(int id)
        {
            var type = await _context.Types
                .FirstOrDefaultAsync(t => t.Id == id);

            if (type == null) return null;

            return new TypeDto
            {
                Id = type.Id,
                TypeName = type.TypeName
            };
        }

        public async Task<TypeDto?> CreateAsync(TypeDto dto)
        {
            var type = new Type
            {
                TypeName = dto.TypeName
            };

            _context.Types.Add(type);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(type.Id);
        }

        public async Task<bool> UpdateAsync(int id, TypeDto dto)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null) return false;

            type.TypeName = dto.TypeName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var type = await _context.Types.FindAsync(id);
            if (type == null) return false;

            _context.Types.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
