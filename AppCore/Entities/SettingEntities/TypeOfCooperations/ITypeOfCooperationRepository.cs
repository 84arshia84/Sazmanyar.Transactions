using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.TypeOfCooperations
{
    public interface ITypeOfCooperationRepository
    {
        #region crud
        public Task<Tuple<string, bool>> Add(TypeOfCooperation typeOfCooperation);
        public Task<TypeOfCooperation> Get(Guid id);
        public Task<List<TypeOfCooperation>> GetAll();
        public Task<Tuple<string, bool>> Update(TypeOfCooperation typeOfCooperation);
        public Task<Tuple<string, bool>> Delete(Guid id);
        #endregion
    }
}
