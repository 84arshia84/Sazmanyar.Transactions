using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.Payments;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.PaymentDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    public static class PaymentMapper
    {
        public static Payment DtoToEntity(AddPaymentDto dto, LoginUserDto userDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var payment = new Payment();
                payment.Id = Guid.NewGuid();
                payment.InvoiceBaseInformationId = dto.InvoiceBaseInformationId;
                payment.InsertDate = DateTime.Now;
                payment.PaymentDate = dto.PaymentDate;
                payment.PaymentAmount = dto.PaymentAmount;
                payment.ExchangeRate = dto.ExchangeRate;
                payment.HowToPayId = dto.HowToPayId;
                payment.CurrencyId = dto.CurrencyId;
                payment.InsertBy = userDto.ID;
                payment.DeleteBy = Guid.Empty;
                payment.DeleteDate = null;
                return payment;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new Payment());
            }
        }
        public static List<GetAllPaymentDto> EntitiesToDtos(List<Payment> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<GetAllPaymentDto>();
                var i = 1;
                foreach (Payment entity in entities)
                {
                    try
                    {
                        var dto = new GetAllPaymentDto();
                        dto.Key = entity.Id;
                        dto.InvoiceBaseInformationId = entity.InvoiceBaseInformationId;
                        dto.PaymentDate = entity.PaymentDate;
                        dto.ExchangeRate = entity.ExchangeRate;
                        dto.PaymentAmount = entity.PaymentAmount;
                        //Should Be Developed
                        dto.CurrencyAmountEquivalent = entity.PaymentAmount;
                        dto.CurrencyId = entity.CurrencyId;
                        dto.CurrencyTitle = entity.Currency.Title;
                        dto.HowToPayId = entity.HowToPayId;
                        dto.HowToPayTitle = entity.HowToPay.Title;
                        dto.Row = i;
                        dtos.Add(dto);
                        i++;
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
                return new List<GetAllPaymentDto>();
            }
        }

    }
}
