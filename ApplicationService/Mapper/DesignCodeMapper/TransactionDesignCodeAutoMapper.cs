using AppCore.Entities.DesignCodesProperties.TransActionExecutionDesignCodes;
using ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.DesignCodeMapper
{
    internal class TransactionDesignCodeAutoMapper
    {
        public static TransActionExecutionDesignCode DtoToEntityAdd(TransactionDesignCodeAddDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new TransActionExecutionDesignCode();
                entity.Id = Guid.NewGuid();
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Counter = 0;
                entity.Parameters = new List<TransActionExecutionDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                    {
                        entity.Parameters.Add(new TransActionExecutionDesignCodeParameterRel
                        {
                            Id = Guid.NewGuid(),
                            TransactionExecutionDesignCodeId = entity.Id,
                            ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                            OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null
                        });
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TransActionExecutionDesignCode();
            }
        }
        public static TransActionExecutionDesignCode DtoToEntityUpdate(TransactionDesignCodeUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new TransActionExecutionDesignCode();
                entity.Id = dto.Id;
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Parameters = new List<TransActionExecutionDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                    {
                        entity.Parameters.Add(new TransActionExecutionDesignCodeParameterRel
                        {
                            Id = Guid.NewGuid(),
                            TransactionExecutionDesignCodeId = entity.Id,
                            ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                            OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null
                        });
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TransActionExecutionDesignCode();
            }
        }
        public static TransactionDesignCodeGetDto EntityToDto(TransActionExecutionDesignCode entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new TransactionDesignCodeGetDto();
                dto.Id = entity.Id;
                dto.Preview = entity.Preview;
                dto.Counter = entity.Counter;
                dto.ParameterPreview = entity.ParameterPreview;
                var contractTypeIds = entity.Parameters
                                    .Where(p => p.ContractTypeId.HasValue)
                                    .Select(p => p.ContractTypeId.Value)
                                    .ToList();
                var organizationIds = entity.Parameters
                                    .Where(p => p.OrganizationUnitId.HasValue)
                                    .Select(p => p.OrganizationUnitId.Value)
                                    .ToList();

                dto.ContractTypeIds = contractTypeIds.Any() ? contractTypeIds : null;
                dto.OrganizationUnitId = organizationIds.Any() ? organizationIds : null;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TransactionDesignCodeGetDto();
            }
        }
        public static List<TransactionDesignCodeGetDto> EntitiesToDtos(List<TransActionExecutionDesignCode> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<TransactionDesignCodeGetDto>();
                foreach (var item in entities)
                {
                    var dto = new TransactionDesignCodeGetDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<TransactionDesignCodeGetDto>();
            }
        }
        public static List<TransactionDesignCodeGetParameterDto> EntitiesToDtosParameter(List<TransActionExecutionDesignCodeParameterRel> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<TransactionDesignCodeGetParameterDto>();
                foreach (var item in entities)
                {
                    var dto = new TransactionDesignCodeGetParameterDto();
                    dto.ContractTypeIds = item.ContractTypeId;
                    dto.OrganizationUnitId = item.OrganizationUnitId;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<TransactionDesignCodeGetParameterDto>();
            }
        }
    }
}
