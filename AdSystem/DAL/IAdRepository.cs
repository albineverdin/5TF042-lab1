using AdSystem.Models;

namespace AdSystem.DAL;

public interface IAdRepository
{
    Task<IEnumerable<Ad>> GetAllAsync();
    Task<Ad> CreateAsync(Ad ad);
    Task<bool> DeleteAsync(int id);
}
