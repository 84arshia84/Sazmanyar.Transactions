using AppCore.Entities.DesignCodesProperties.FactorDesignCodes;
using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.DesignCodeMapper
{
    internal class FactorDesignCodeAutoMapper
    {
        public static FactorDesignCode DtoToEntityAdd(FactorDesignCodeAddDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorDesignCode();
                entity.Id = Guid.NewGuid();
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Counter = 0;
                entity.Parameters = new List<FactorDesignCodeParameterRel>();
                var factorTypeIds = dto.FactorTypeId ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var factorTypeId in factorTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            entity.Parameters.Add(new FactorDesignCodeParameterRel
                            {
                                Id = Guid.NewGuid(),
                                FactorDesignCodeId = entity.Id,
                                FactorTypeId = factorTypeIds.Any() ? factorTypeId : null,
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
                return new FactorDesignCode();
            }
        }
        public static FactorDesignCode DtoToEntityUpdate(FactorDesignCodeUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new FactorDesignCode();
                entity.Id = dto.Id;
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Parameters = new List<FactorDesignCodeParameterRel>();
                var factorTypeIds = dto.FactorTypeId ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in factorTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            entity.Parameters.Add(new FactorDesignCodeParameterRel
                            {
                                Id = Guid.NewGuid(),
                                FactorDesignCodeId = entity.Id,
                                FactorTypeId = factorTypeIds.Any() ? contractTypeId : null,
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
                return new FactorDesignCode();
            }
        }
        public static FactorDesignCodeGetDto EntityToDto(FactorDesignCode entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorDesignCodeGetDto();
                dto.Id = entity.Id;
                dto.Preview = entity.Preview;
                dto.Counter = entity.Counter;
                dto.ParameterPreview = entity.ParameterPreview;
                var factorTypeIds = entity.Parameters
                                    .Where(p => p.FactorTypeId.HasValue)
                                    .Select(p => p.FactorTypeId.Value)
                                    .ToList();
                var organizationIds = entity.Parameters
                                    .Where(p => p.OrganizationUnitId.HasValue)
                                    .Select(p => p.OrganizationUnitId.Value)
                                    .ToList();
                var roleIds = entity.Parameters
                                    .Where(p => p.RoleOfOrganizationId.HasValue)
                                    .Select(p => p.RoleOfOrganizationId.Value)
                                    .ToList();

                dto.FactorTypeId = factorTypeIds.Any() ? factorTypeIds : null;
                dto.OrganizationUnitId = organizationIds.Any() ? organizationIds : null;
                dto.RoleOfOrganizationId = roleIds.Any() ? roleIds : null;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorDesignCodeGetDto();
            }
        }
        public static List<FactorDesignCodeGetDto> EntitiesToDtos(List<FactorDesignCode> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<FactorDesignCodeGetDto>();
                foreach (var item in entities)
                {
                    var dto = new FactorDesignCodeGetDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorDesignCodeGetDto>();
            }
        }
        public static List<FactorDesignCodeGetParameterDto> EntitiesToDtosParameter(List<FactorDesignCodeParameterRel> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<FactorDesignCodeGetParameterDto>();
                foreach (var item in entities)
                {
                    var dto = new FactorDesignCodeGetParameterDto();
                    dto.FactorTypeId = item.FactorTypeId;
                    dto.OrganizationUnitId = item.OrganizationUnitId;
                    dto.RoleOfOrganizationId = item.RoleOfOrganizationId;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<FactorDesignCodeGetParameterDto>();
            }
        }
    }
}
