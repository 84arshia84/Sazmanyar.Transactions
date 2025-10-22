using ApplicationService.DtoModels.InvoiceDtos.InvoiceTypeDtos;
using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.InvoicesInformations.InvoiceType;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    internal static class InvoiceTypeMapper
    {
        public static InvoiceTypeDto EntityToDto(InvoiceType entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new InvoiceTypeDto();
                dto.Title = entity.Title;
                dto.Key = entity.Id;
                dto.OfficeOnlineDocumentId = entity.OfficeOnlineDocumentId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new InvoiceTypeDto();
            }
        }
        public static List<InvoiceTypeDto> EntitiesToDtos(List<InvoiceType> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<InvoiceTypeDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new InvoiceTypeDto();
                dto = EntityToDto(entities[i], errorLogger);
                dto.Row = i;
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
