using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ContractInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractFinancialDetailsService : IContractFinancialDetailsService
    {
        private IUnitOfWork _unitOfWork;
        public ContractFinancialDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن مشخصات مالی قرارداد
        /// </summary>
        /// <param name="contractFinancial"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> AddFinancialDetaile(ContractFinancialDetails contractFinancial)
        {
            try
            {
                return await _unitOfWork.ContractFinancialDetailsRepository.Add(contractFinancial);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// گرفتن مشخصات مالی یک قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns>ContractFinancialDetails</returns>
        /// <exception cref="ContractFinancialDetails"></exception>
        public async Task<ContractFinancialDetails> GetFinancialDetails(Guid contractId)
        {
            try
            {
                return await _unitOfWork.ContractFinancialDetailsRepository.Get(contractId);
            }
            catch (Exception ex)
            {
                return new ContractFinancialDetails();
            }
        }
        /// <summary>
        /// بروزرسانی مشخصات مالی قرارداد
        /// </summary>
        /// <param name="contractFinancial"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> UpdateFinancialDetaile(ContractFinancialDetails contractFinancial)
        {
            try
            {
                return await _unitOfWork.ContractFinancialDetailsRepository.Update(contractFinancial);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
