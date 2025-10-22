using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal class ContractTimeProfileFromTransactionMapper
    {
        public static async Task<ContractTimeProfile> DtoToEntity(ContractFromTransactionDto dto)
        {
            var contractTimeProfiles = new ContractTimeProfile();
            contractTimeProfiles.ContractDateOfNotification = dto.ContractDateOfNotification;
            contractTimeProfiles.ContractExchangeDate = dto.ContractExchangeDate;
            contractTimeProfiles.ContractPeriod = dto.ContractPeriod;
            contractTimeProfiles.ContractStartDate = dto.ContractStartDate;
            contractTimeProfiles.ContractEndDate = dto.ContractEndDate;
            contractTimeProfiles.BasisForStartingProjectID = dto.BasisForStartingProjectID;
            return contractTimeProfiles;
        }
    }
}
