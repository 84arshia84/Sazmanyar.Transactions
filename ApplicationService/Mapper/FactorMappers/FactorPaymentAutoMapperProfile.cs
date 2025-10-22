using AppCore.Entities.FactorInformation.FactorPayments;
using ApplicationService.DtoModels.FactorDtos.FactorPayment;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorPaymentAutoMapperProfile
    {
        public static FactorPayment DtoToEntityAdd(FactorPaymentAddDto dto,  Guid userId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorPayment();
                entity.Id = Guid.NewGuid();
                entity.PaymentDate = dto.PaymentDate;
                entity.PaymentAmount = dto.PaymentAmount;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.HowToPayId = dto.HowToPayId;
                entity.CurrencyId = dto.CurrencyId;
                entity.IsDeleted = false;
                entity.InsertDate = DateTime.Now;
                entity.InsertBy = userId;
                entity.FactorId = dto.FactorId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorPayment DtoToEntityUpdate(FactorPaymentUpdateDto dto, FactorPayment lastData, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorPayment();
                entity.Id = dto.Id;
                entity.PaymentDate = dto.PaymentDate;
                entity.PaymentAmount = dto.PaymentAmount;
                entity.AccelerationRate = dto.AccelerationRate;
                entity.HowToPayId = dto.HowToPayId;
                entity.CurrencyId = dto.CurrencyId;
                entity.IsDeleted = false;
                entity.InsertDate = lastData.InsertDate;
                entity.InsertBy = lastData.InsertBy;
                entity.FactorId = lastData.FactorId;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorPaymentGetDto EntityToDto(FactorPayment entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorPaymentGetDto();
                dto.Id = entity.Id;
                dto.FactorId = entity.FactorId;
                dto.PaymentDate = entity.PaymentDate;
                dto.PaymentAmount = entity.PaymentAmount;
                dto.AccelerationRate = entity.AccelerationRate;
                dto.HowToPayId = entity.HowToPayId;
                dto.CurrencyId = entity.CurrencyId;

                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorPaymentGetDto();
            }
        }
        public static List<FactorPaymentGetDto> EntitiesToDtos(List<FactorPayment> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorPaymentGetDto>();
            foreach (FactorPayment entity in entities)
            {
                try
                {
                    var dto = new FactorPaymentGetDto();
                    dto = EntityToDto(entity, errorLoggerService);
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
    }
}
