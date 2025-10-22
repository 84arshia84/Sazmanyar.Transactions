using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Enums;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    internal static class NettingProcessItemMapper
    {
        public static NettingProcessItem DtoToEntity(AddNettingProcessItemDto dto, LoginUserDto userDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var nettingProcessItem = new NettingProcessItem();
                nettingProcessItem.Id = Guid.NewGuid();
                nettingProcessItem.InsertDate = DateTime.Now;
                nettingProcessItem.InvoiceBaseInformationId = dto.InvoiceBaseInformationId;
                nettingProcessItem.Title = dto.Title;
                nettingProcessItem.Percentage = dto.Percentage;
                nettingProcessItem.Amount = dto.Amount;
                nettingProcessItem.IsDeduction = dto.IsDeduction;
                nettingProcessItem.IsEditable = dto.IsEditable;
                nettingProcessItem.InsertBy = userDto.ID;
                nettingProcessItem.IsDeleted = false;
                nettingProcessItem.DeleteBy = Guid.Empty;
                nettingProcessItem.DeleteDate = null;
                nettingProcessItem.NettingProcessTypes = (NettingProcessTypesEnum)dto.NettingProcessType;
                nettingProcessItem.CurrencyId = dto.CurrencyId;
                return nettingProcessItem;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new NettingProcessItem());
            }
        }
        public static List<GetAllNettingProcessItemsDto> EntitiesToDtos(List<NettingProcessItem> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<GetAllNettingProcessItemsDto>();
                foreach (NettingProcessItem entity in entities)
                {
                    try
                    {
                        var dto = new GetAllNettingProcessItemsDto();
                        dto.Key = entity.Id;
                        dto.Title = entity.Title;
                        dto.Percentage = entity.Percentage;
                        dto.Amount = entity.Amount;
                        dto.IsDeduction = entity.IsDeduction;
                        dto.IsEditable = entity.IsEditable;
                        dto.NettingProcessType = (int)entity.NettingProcessTypes;
                        dto.CurrencyId = entity.CurrencyId;
                        dtos.Add(dto);
                    }
                    catch (Exception ex)
                    {
                        errorLoggerService.SaveError(ex);
                        continue;
                    }
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<GetAllNettingProcessItemsDto>();
            }
        }
    }
}
