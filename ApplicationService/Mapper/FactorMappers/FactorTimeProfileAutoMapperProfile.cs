using AppCore.Entities.FactorInformation.FactorTimeProfiles;
using ApplicationService.DtoModels.FactorDtos.FactorTimeProfile;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorTimeProfileAutoMapperProfile
    {
        public static FactorTimeProfile DtoToEntityAdd(FactorTimeProfileAddDto dto, Guid timeProfileId,Guid factorId, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorTimeProfile();
                entity.Id = timeProfileId;
                entity.FactorDate = dto.FactorDate;
                entity.FactorStartDate = dto.FactorStartDate;
                entity.FactorEndDate = dto.FactorEndDate;
                entity.FactorPeriod = dto.FactorPeriod;
                entity.FactorId = factorId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorTimeProfile DtoToEntityUpdate(FactorTimeProfileUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorTimeProfile();
                entity.Id = dto.Id;
                entity.FactorDate = dto.FactorDate;
                entity.FactorStartDate = dto.FactorStartDate;
                entity.FactorEndDate = dto.FactorEndDate;
                entity.FactorPeriod = dto.FactorPeriod;
                entity.FactorId = dto.FactorId;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorTimeProfileGetDto EntityToDto(FactorTimeProfile entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorTimeProfileGetDto();
                dto.Id = entity.Id;
                dto.FactorId = entity.FactorId;
                dto.FactorDate = entity.FactorDate;
                dto.FactorStartDate = entity.FactorStartDate;
                dto.FactorEndDate = entity.FactorEndDate;
                dto.FactorPeriod = entity.FactorPeriod;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorTimeProfileGetDto();
            }
        }
        public static List<FactorTimeProfileGetDto> EntitiesToDtos(List<FactorTimeProfile> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorTimeProfileGetDto>();
            foreach (FactorTimeProfile entity in entities)
            {
                try
                {
                    var dto = new FactorTimeProfileGetDto();
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
