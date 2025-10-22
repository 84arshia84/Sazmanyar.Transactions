using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.PriceLists
{
    public  interface IPriceListExplanationService
    {
        public Task<(string message, bool isSuccess)> AddListExplanations(List<PriceListExplanationsDto> listExplanations);
        public Task<(string message, bool isSuccess)> AddExplanation(PriceListExplanationsDto Explanation);
        public Task<(string message, bool isSuccess)> DeleteExplanation(Guid explanaionId);
        public Task<(string message, bool isSuccess)> UpdateExplanation(PriceListExplanationsDto Explanation);
        public Task<List<PriceListExplanationsDto>> GetAllListExplanation();
        public Task<PriceListExplanationsDto> GetistExplanatio(Guid explanaionId);
        public Task<List<PriceListExplanationsDto>> GetAllListExplanationByClauseID(Guid explanaionId);
    }
}
