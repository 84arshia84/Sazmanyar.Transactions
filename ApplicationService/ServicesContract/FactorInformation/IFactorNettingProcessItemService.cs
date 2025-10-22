using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.FactorInformation
{
    public interface IFactorNettingProcessItemService
    {
        public Task<bool> AddDefaultFactorData(FactorFinancialDetaileAddDto factorFinancialDetaile, List<FactorServiceExplanationAddDto> serviceExplanation,Guid factorId, Guid userid);
        public Task<(string message, bool isSuccess)> Add(FactorNettingProcessItemAddDto nettingProcessItem,string userName);
        public Task<FactorNettingProcessItemGetDto> Get(Guid nettingProcessItemId);
        public Task<(List<FactorNettingProcessItemGetDto> factorNettingProcessesItems ,List<NettedAmountGet> netted)> GetAll(Guid factorId);
        public Task<(string message, bool isSuccess)> Delete(Guid nettingProcessItemId, string userName);
        public Task<(string message, bool isSuccess)> Update(FactorNettingProcessItemUpdateDto nettingProcessItem);
        public Task UpdateDefaultFactorData(FactorFinancialDetaileUpdateDto factorFinancialDetaile,Guid factorId, List<FactorServiceExplanationUpdateDto> serviceExplanation);
    }
}
