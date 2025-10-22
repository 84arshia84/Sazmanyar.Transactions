

namespace AppCore.Entities.Histories
{
    public interface IHistoryRepository
    {
        Task<bool> AddAsync(History history);
        Task<List<History>> GetAll(Guid EntityId);
    }
}
