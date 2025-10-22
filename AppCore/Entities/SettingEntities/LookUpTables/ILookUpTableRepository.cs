using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.LookUpTables
{
    public interface ILookUpTableRepository
    {
        public Task<bool> Add(LookUpTable lookUpTable);
        public Task<bool> Update(LookUpTable lookUpTable);
        public Task<bool> Delete(Guid lookUpTable);
        public Task<List<LookUpTable>> GetAll();
        public Task<LookUpTable> Get(Guid lookUpTable);
        public Task<bool> AddInside(LookUpTableInside lookUpTableInside);
        public Task<bool> UpdateInside(LookUpTableInside lookUpTableInside);
        public Task<bool> DeleteInside(Guid lookUpTableInside);
        public Task<List<LookUpTableInside>> GetAllInside(Guid lookUpId);
        public Task<LookUpTableInside> GetInside(Guid lookUpTableInside);
    }
}
