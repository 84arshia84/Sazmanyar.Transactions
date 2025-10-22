using AppCore.Entities.InvoiceInformations.InvoiceBaseCustomization;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseCustomizationDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    public class InvoiceBaseCustomizationMapper
    {
        public static InvoiceBaseCustomization DtoToEntity(InvoiceBaseCustomizationDto dto, IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new InvoiceBaseCustomization();
                entity.Id = dto.Id;          
                entity.Title = dto.Title;
                entity.Width = dto.Width;
                entity.IsHidden = dto.IsHidden;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new InvoiceBaseCustomization();
            }
        }

        public static InvoiceBaseCustomizationDto EntityToDto(InvoiceBaseCustomization entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new InvoiceBaseCustomizationDto();
                dto.Id = entity.Id;         
                dto.Title = entity.Title;
                dto.Width = entity.Width;
                dto.IsHidden = entity.IsHidden;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new InvoiceBaseCustomizationDto();
            }
        }
    }
}
