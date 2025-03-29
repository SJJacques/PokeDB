namespace PokeDB.Server.Services.Interfaces
{
    public interface ICrudService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> CreateAsync(T dto);
        Task<bool> UpdateAsync(int id, T dto);
        Task<bool> DeleteAsync(int id);
    }
}
