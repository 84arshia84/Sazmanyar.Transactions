using AppCore.Entities.FactorInformation.FactorFinancialDetailes;
using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorFinancialDetaileAutoMapperProfile
    {
        public static FactorFinancialDetaile DtoToEntityAdd(FactorFinancialDetaileAddDto dto,Guid financialDetailId ,Guid factorId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorFinancialDetaile();
                entity.Id = financialDetailId;
                entity.FactorInsurance_Percent = dto.FactorInsurance_Percent;
                entity.FactorValue_Added_Percent = dto.FactorValue_Added_Percent;
                entity.FactorTax_Percent = dto.FactorTax_Percent;
                entity.GoodJob_Percent = dto.GoodJob_Percent;
                entity.FactorId =factorId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorFinancialDetaile DtoToEntityUpdate(FactorFinancialDetaileUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorFinancialDetaile();
                entity.Id = dto.Id;
                entity.FactorInsurance_Percent = dto.FactorInsurance_Percent;
                entity.FactorValue_Added_Percent = dto.FactorValue_Added_Percent;
                entity.FactorTax_Percent = dto.FactorTax_Percent;
                entity.GoodJob_Percent = dto.GoodJob_Percent;
                entity.FactorId = dto.FactorId;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorFinancialDetaileGetDto EntityToDto(FactorFinancialDetaile entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorFinancialDetaileGetDto();
                dto.Id = entity.Id;
                dto.FactorId = entity.FactorId;
                dto.FactorInsurance_Percent = entity.FactorInsurance_Percent;
                dto.FactorValue_Added_Percent = entity.FactorValue_Added_Percent;
                dto.FactorTax_Percent = entity.FactorTax_Percent;
                dto.GoodJob_Percent = entity.GoodJob_Percent;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorFinancialDetaileGetDto();
            }
        }
        public static List<FactorFinancialDetaileGetDto> EntitiesToDtos(List<FactorFinancialDetaile> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorFinancialDetaileGetDto>();
            foreach (FactorFinancialDetaile entity in entities)
            {
                try
                {
                    var dto = new FactorFinancialDetaileGetDto();
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
