using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Enums;
using ApplicationService.DtoModels.SettingDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface IContractTypeService
    {
        public Task<(string message , bool isSuccess)> Add(ContractTypeDto contractType);
        public Task<(string message, bool isSuccess)> Update(ContractTypeDto contractType);
        public Task<(string message, bool isSuccess)> Delete(Guid contractType);
        public Task<(string message, bool isSuccess)> UpdateOfficeOnline(Guid contractTypeId, Guid? filenameId);
        public Task<List<ContractTypeDto>> GetAll();
        public Task<List<ContractTypeDto>> GetAllWithAccessGroupEffect(Guid userIid,int part,int property,int mode);
        public Task<ContractTypeDto> Get(Guid contractType);
        //public Task<(string message, bool isSuccess)> UpsertTemplate(Guid contractTypeId, TemplateType type, Guid fileId, bool isDefault);
        //public  Task<List<ContractTypeTemplateDto>> GetTemplates(Guid contractTypeId);
        //public Task<(string message, bool isSuccess)> RemoveTemplate(Guid templateId);



    }
}
