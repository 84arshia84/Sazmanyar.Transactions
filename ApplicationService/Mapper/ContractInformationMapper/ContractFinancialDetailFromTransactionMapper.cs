using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using ApplicationService.DtoModels.ContractDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal class ContractFinancialDetailFromTransactionMapper
    {
        public static async Task<ContractFinancialDetails> DtoToEntity(ContractFromTransactionDto dto)
        {
            var contractFinancialDetails = new ContractFinancialDetails();
            contractFinancialDetails.ContractInsurance_Percent = dto.contractInsurance_Percent;
            contractFinancialDetails.ContractValue_Added_Percent = dto.contractValue_Added_Percent;
            contractFinancialDetails.ContractTax_Percent = dto.contractTax_Percent;
            contractFinancialDetails.PercentageOfChanges = dto.percentageOfChanges;
            contractFinancialDetails.GoodJob_Percent = dto.goodJob_Percent;
            contractFinancialDetails.Amount_of_timeExtension = dto.amount_of_timeExtension;
            contractFinancialDetails.Basis_of_Receipt = dto.basis_of_Receipt;
            return contractFinancialDetails;
        }
    }
}
