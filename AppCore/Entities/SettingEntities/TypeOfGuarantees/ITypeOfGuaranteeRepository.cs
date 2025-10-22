using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.TypeOfGuarantees
{
    public interface ITypeOfGuaranteeRepository
    {
        public Task<bool> Add(TypeOfGuarantee guarantee);
        public Task<bool> Update(TypeOfGuarantee guarantee);
        public Task<bool> Delete(Guid guarantee);
        public Task<List<TypeOfGuarantee>> GetAll();
        public Task<TypeOfGuarantee> Get(Guid guaranteeId);
    }
}
