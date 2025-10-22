using AppCore.Entities.InvoiceInformations.NettingProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorInformation.FactorNettingProcessItems
{
    public interface IFactorNettingProcessItemRepository
    {
        public Task<bool> Add(List<FactorNettingProcessItem> nettingProcessItem);
        public Task<bool> Add(FactorNettingProcessItem nettingProcessItem);
        public Task<FactorNettingProcessItem> Get(Guid nettingProcessItemId);
        public Task<List<FactorNettingProcessItem>> GetAll(Guid factorId);
        public Task<bool> Delete(Guid nettingProcessItemId, Guid userId);
        public Task<bool> Update(FactorNettingProcessItem nettingProcessItem);
    }
}
