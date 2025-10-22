using AppCore.Entities.DesignCodesProperties.ContractDesignCodes;
using ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.DesignCodeMapper
{
    internal static class ContractDesignCodeAutoMapper
    {
        public static ContractDesignCode DtoToEntityAdd(ContractDesignCodeAddDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractDesignCode();
                entity.Id = Guid.NewGuid();
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Counter = 0;
                entity.Parameters = new List<ContractDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            entity.Parameters.Add(new ContractDesignCodeParameterRel
                            {
                                Id = Guid.NewGuid(),
                                ContractDesignCodeId = entity.Id,
                                ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                                RoleOfOrganizationId = roleIds.Any() ? roleId : null,
                                OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null
                            });
                        }
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractDesignCode();
            }
        }
        public static ContractDesignCode DtoToEntityUpdate(ContractDesignCodeUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new ContractDesignCode();
                entity.Id = dto.Id;
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Parameters = new List<ContractDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            entity.Parameters.Add(new ContractDesignCodeParameterRel
                            {
                                Id = Guid.NewGuid(),
                                ContractDesignCodeId = entity.Id,
                                ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                                RoleOfOrganizationId = roleIds.Any() ? roleId : null,
                                OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null
                            });
                        }
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractDesignCode();
            }
        }
        public static ContractDesignCodeGetDto EntityToDto(ContractDesignCode entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractDesignCodeGetDto();
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
                var roleIds = entity.Parameters
                                    .Where(p => p.RoleOfOrganizationId.HasValue)
                                    .Select(p => p.RoleOfOrganizationId.Value)
                                    .ToList();

                dto.ContractTypeIds = contractTypeIds.Any() ? contractTypeIds : null;
                dto.OrganizationUnitId = organizationIds.Any() ? organizationIds : null;
                dto.RoleOfOrganizationId = roleIds.Any() ? roleIds : null;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractDesignCodeGetDto();
            }
        }
        public static List<ContractDesignCodeGetDto> EntitiesToDtos(List<ContractDesignCode> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractDesignCodeGetDto>();
                foreach (var item in entities)
                {
                    var dto = new ContractDesignCodeGetDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractDesignCodeGetDto>();
            }
        }
        public static List<ContractDesignCodeGetParameterDto> EntitiesToDtosParameter(List<ContractDesignCodeParameterRel> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ContractDesignCodeGetParameterDto>();
                foreach (var item in entities)
                {
                    var dto = new ContractDesignCodeGetParameterDto();
                    dto.ContractTypeIds = item.ContractTypeId;
                    dto.OrganizationUnitId = item.OrganizationUnitId;
                    dto.RoleOfOrganizationId = item.RoleOfOrganizationId;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ContractDesignCodeGetParameterDto>();
            }
        }
    }
}
