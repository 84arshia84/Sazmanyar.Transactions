using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PriceLists
{
    public interface IPriceListFieldService
    {
        public Task<(string message,bool isSecces)> AddList(List<PriceListFieldDto> field);
        public Task<(Guid ID, string message, bool isSecces)> Add(PriceListFieldDto field);
        public Task<(string message, bool isSecces)> Delete(Guid fieldId);
        public Task<(string message, bool isSecces)> Update(PriceListFieldDto field);
        public Task<List<PriceListField>> GetAll();
        public Task<List<PriceListFieldDto>> GetAllByYearId(Guid id);
        public Task<PriceListField> Get(Guid fieldId);

    }
}
