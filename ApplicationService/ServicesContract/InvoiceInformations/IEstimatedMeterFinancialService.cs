using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IEstimatedMeterFinancialService
    {
        public Task<List<GetAllEMFDto>> GetAllEMF(GetAllEMFIdsDto getAllEMFIdsDto);
        public Task<(string message, bool isSuccess)> CUDEMRequestedFinancial(CUDEMRequestedFinancialDto cUDEMRequestedFinancialDto, LoginUserDto userDto);
        public Task<(string message, bool isSuccess)> SetEMApprovedFinancial(SetEMApprovedFinancialDto setEMApprovedFinancialDto, LoginUserDto userDto);
    }
}
