using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Statuses
{
    public interface IStatusRepository
    {
        public Task<bool> Add(Status status);
        public Task<bool> Update(Status status);
        public Task<bool> Delete(Guid statusId);
        public Task<List<Status>> GetAll();
        public Task<Status> GetByContractId(Guid contractId);
        public Task<Status> Get(Guid statusId);
    }
}
