using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PriceLists
{
    public interface IPriceListClauseService
    {
        public Task<(string message, bool isSuccess)> AddList(List<PriceListClauseDto> listClauses);
        public Task<(Guid ID,string message, bool isSuccess)> Add(PriceListClauseDto Clause);
        public Task<(string message, bool isSuccess)> Delete(Guid cluadId);
        public Task<(string message, bool isSuccess)> Update(PriceListClauseDto Clause);
        public Task<List<PriceListClause>> GetAll();
        public Task<List<PriceListClauseDto>> GetAllByFieldId(Guid id);

    }
}
