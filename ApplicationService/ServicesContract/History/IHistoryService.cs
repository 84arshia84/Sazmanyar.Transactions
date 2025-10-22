using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.HistoryDto;

namespace ApplicationService.ServicesContract.History
{
    public interface IHistoryService
    {
        public Task<bool> AddHistory(Guid userId, string action, Guid entityId, string userName);
        public Task<List<HistoryDto>> GetAll(Guid entityId);
    }
}
