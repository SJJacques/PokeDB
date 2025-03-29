using Microsoft.EntityFrameworkCore;
using PokeDB.Server.Data;
using PokeDB.Server.Models;
using PokeDB.Server.Models.DTOs;
using PokeDB.Server.Services.Interfaces;

namespace PokeDB.Server.Services
{
    public class MoveService : ICrudService<MoveDto>
    {
        private readonly PokeDbContext _context;

        public MoveService(PokeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MoveDto>> GetAllAsync()
        {
            return await _context.Moves
                .Include(m => m.Type)
                .Select(m => new MoveDto
                {
                    Id = m.Id,
                    MoveName = m.MoveName,
                    Power = m.AttackPower,
                    Accuracy = m.Accuracy,
                    TypeName = m.Type.TypeName
                })
                .ToListAsync();
        }

        public async Task<MoveDto?> GetByIdAsync(int id)
        {
            var move = await _context.Moves
                .Include(m => m.Type)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (move == null) return null;

            return new MoveDto
            {
                Id = move.Id,
                MoveName = move.MoveName,
                Power = move.AttackPower,
                Accuracy = move.Accuracy,
                TypeName = move.Type.TypeName
            };
        }

        public async Task<MoveDto?> CreateAsync(MoveDto dto)
        {
            var move = new Move
            {
                MoveName = dto.MoveName,
                AttackPower = dto.Power,
                Accuracy = dto.Accuracy,
                TypeId = await GetTypeIdByName(dto.TypeName)
            };

            _context.Moves.Add(move);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(move.Id);
        }

        public async Task<bool> UpdateAsync(int id, MoveDto dto)
        {
            var move = await _context.Moves.FindAsync(id);
            if (move == null) return false;

            move.MoveName = dto.MoveName;
            move.AttackPower = dto.Power;
            move.Accuracy = dto.Accuracy;
            move.TypeId = await GetTypeIdByName(dto.TypeName);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var move = await _context.Moves.FindAsync(id);
            if (move == null) return false;

            _context.Moves.Remove(move);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<int> GetTypeIdByName(string typeName)
        {
            var type = await _context.Types.FirstOrDefaultAsync(t => t.TypeName == typeName);
            return type?.Id ?? throw new ArgumentException($"Type '{typeName}' not found.");
        }
    }
}
