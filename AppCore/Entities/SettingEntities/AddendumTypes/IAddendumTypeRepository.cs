using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.AddendumTypes
{
    public interface IAddendumTypeRepository
    {
        public Task<bool> Add(AddendumType addendumType);
        public Task<bool> Update(AddendumType addendumType);
        public Task<List<AddendumType>> GetAll();
        public Task<AddendumType> Get(Guid id);
        public Task<bool> Delete(Guid id);
    }
}
