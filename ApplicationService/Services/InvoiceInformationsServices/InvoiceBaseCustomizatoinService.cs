using AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseCustomizationDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    public class InvoiceBaseCustomizatoinService : IInoviceBaseCustomizationService
    {
        private readonly IInoviceBaseCustomization _repo;
        private readonly IErrorLoggerService _errorLogger;
        public InvoiceBaseCustomizatoinService(IInoviceBaseCustomization repo, IErrorLoggerService errorLogger)
        {
            _errorLogger = errorLogger;
            _repo = repo;
        }
        public async Task<InvoiceBaseCustomizationDto> Get(Guid id)
        {
            try
            {
                var exisitng = await _repo.Get(id);
                if (exisitng == null)
                {
                    return null;
                }
                var invoiceBase = InvoiceBaseCustomizationMapper.EntityToDto(exisitng, _errorLogger);
                return invoiceBase;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task Update(InvoiceBaseCustomizationDto dto)
        {
            try
            {
                var invoiceBase =  InvoiceBaseCustomizationMapper.DtoToEntity(dto, _errorLogger);
                await _repo.Update(invoiceBase);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
