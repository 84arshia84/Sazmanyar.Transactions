using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ContractInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractTimeProfileService : IContractTimeProfileService
    {
        private IUnitOfWork _unitOfWork;
        public ContractTimeProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن مشخصات زمانی قرارداد
        /// </summary>
        /// <param name="contractTime"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> AddProfile(ContractTimeProfile contractTime)
        {
            try
            {
                return await _unitOfWork.ContractTimeProfileRepository.Add(contractTime);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// گرفتن مشخصات زمانی یک قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns>ContractTimeProfile</returns>
        /// <exception cref="ContractTimeProfile"></exception>
        public async Task<ContractTimeProfile> GetProfile(Guid contractId)
        {
            try
            {
                return await _unitOfWork.ContractTimeProfileRepository.Get(contractId);
            }
            catch (Exception ex)
            {

                return new ContractTimeProfile();
            }
        }
        /// <summary>
        ///  بروزرسانی مشخصات زمانی قرارداد
        /// </summary>
        /// <param name="contractTime"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<bool> UpdateProfile(ContractTimeProfile contractTime)
        {
            try
            {

                return await _unitOfWork.ContractTimeProfileRepository.Update(contractTime);
            }
            catch (Exception ex)
            {
               return false;
            }
        }
    }
}
