using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.InvoiceInformations
{
    public interface IServiceExplanationFinancialService
    {
        public Task<List<GetAllCSFDto>> GetAllCFS(GetAllCSFIdsDto getAllCFSIdsDto, bool mode);
        public Task<(string message, bool isSuccess)> CUDSERequestedFinancial(CUDSERequestedFinancialDto cUDSERequestedFinancialDto, LoginUserDto userDto);
        public Task<(string message, bool isSuccess)> SetSEApprovedFinancial(SetSEApprovedFinancialDto setSEApprovedFinancialDto, LoginUserDto userDto);
    }
}
