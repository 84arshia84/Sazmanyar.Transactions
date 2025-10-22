using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ILookUpTableService
    {
        public Task<(string message, bool isSuccess)> Add(LookUpTableDtos lookUpTable);
        public Task<(string message, bool isSuccess)> Update(LookUpTableDtos lookUpTable);
        public Task<(string message, bool isSuccess)> Delete(Guid lookUpTable);
        public Task<List<LookUpTableDtos>> GetAll();
        public Task<LookUpTableDtos> Get(Guid lookUpTable);
        public Task<(string message, bool isSuccess)> AddInside(LookUpTableInsideDto lookUpTable);
        public Task<(string message, bool isSuccess)> UpdateInside(LookUpTableInsideDto lookUpTable);
        public Task<(string message, bool isSuccess)> DeleteInside(Guid lookUpTable);
        public Task<List<LookUpTableInsideDto>> GetAllInside(Guid lookUpId);
    }
}
