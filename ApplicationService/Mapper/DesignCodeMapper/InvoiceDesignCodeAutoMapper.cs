using AppCore.Entities.DesignCodesProperties.InvoiceDesignCodes;
using ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.DesignCodeMapper
{
    internal class InvoiceDesignCodeAutoMapper
    {
        public static InvoiceDesignCode DtoToEntityAdd(InvoiceDesignCodeAddDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new InvoiceDesignCode();
                entity.Id = Guid.NewGuid();
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Counter = 0;
                entity.Parameters = new List<InvoiceDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                var invoiceTypesIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            foreach (var invoiceTypeId in invoiceTypesIds.DefaultIfEmpty())
                            {
                                entity.Parameters.Add(new InvoiceDesignCodeParameterRel
                                {
                                    Id = Guid.NewGuid(),
                                    InvoiceDesignCodeId = entity.Id,
                                    ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                                    RoleOfOrganizationId = roleIds.Any() ? roleId : null,
                                    OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null,
                                    InvoiceTypeId = invoiceTypesIds.Any() ? invoiceTypeId : null,
                                });
                            }

                        }
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new InvoiceDesignCode();
            }
        }
        public static InvoiceDesignCode DtoToEntityUpdate(InvoiceDesignCodeUpdateDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new InvoiceDesignCode();
                entity.Id = dto.Id;
                entity.DesignCode = dto.DesignCode;
                entity.Preview = dto.Preview;
                entity.ParameterPreview = dto.ParameterPreview;
                entity.Parameters = new List<InvoiceDesignCodeParameterRel>();
                var contractTypeIds = dto.ContractTypeIds ?? new List<Guid>();
                var roleIds = dto.RoleOfOrganizationId ?? new List<Guid>();
                var orgUnitIds = dto.OrganizationUnitId ?? new List<Guid>();
                var invoiceTypesIds = dto.OrganizationUnitId ?? new List<Guid>();
                foreach (var contractTypeId in contractTypeIds.DefaultIfEmpty())
                {
                    foreach (var roleId in roleIds.DefaultIfEmpty())
                    {
                        foreach (var orgUnitId in orgUnitIds.DefaultIfEmpty())
                        {
                            foreach (var invoiceTypeId in invoiceTypesIds.DefaultIfEmpty())
                            {
                                entity.Parameters.Add(new InvoiceDesignCodeParameterRel
                                {
                                    Id = Guid.NewGuid(),
                                    InvoiceDesignCodeId = entity.Id,
                                    ContractTypeId = contractTypeIds.Any() ? contractTypeId : null,
                                    RoleOfOrganizationId = roleIds.Any() ? roleId : null,
                                    OrganizationUnitId = orgUnitIds.Any() ? orgUnitId : null,
                                    InvoiceTypeId = invoiceTypesIds.Any() ? invoiceTypeId : null,
                                });
                            }
                        }
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new InvoiceDesignCode();
            }
        }
        public static InvoiceDesignCodeGetDto EntityToDto(InvoiceDesignCode entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new InvoiceDesignCodeGetDto();
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
                var invoiceTypeIds = entity.Parameters
                                    .Where(p => p.InvoiceTypeId.HasValue)
                                    .Select(p => p.InvoiceTypeId.Value)
                                    .ToList();
                dto.ContractTypeIds = contractTypeIds.Any() ? contractTypeIds : null;
                dto.OrganizationUnitId = organizationIds.Any() ? organizationIds : null;
                dto.RoleOfOrganizationId = roleIds.Any() ? roleIds : null;
                dto.InvoiceTypeId = invoiceTypeIds.Any() ? invoiceTypeIds : null;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new InvoiceDesignCodeGetDto();
            }
        }
        public static List<InvoiceDesignCodeGetDto> EntitiesToDtos(List<InvoiceDesignCode> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<InvoiceDesignCodeGetDto>();
                foreach (var item in entities)
                {
                    var dto = new InvoiceDesignCodeGetDto();
                    dto = EntityToDto(item, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<InvoiceDesignCodeGetDto>();
            }
        }
        public static List<InvoiceDesignCodeGetParameterDto> EntitiesToDtosParameter(List<InvoiceDesignCodeParameterRel> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<InvoiceDesignCodeGetParameterDto>();
                foreach (var item in entities)
                {
                    var dto = new InvoiceDesignCodeGetParameterDto();
                    dto.ContractTypeIds = item.ContractTypeId.Value;
                    dto.OrganizationUnitId = item.OrganizationUnitId.Value;
                    dto.RoleOfOrganizationId = item.RoleOfOrganizationId.Value;
                    dto.InvoiceTypeId = item.InvoiceTypeId.Value;
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<InvoiceDesignCodeGetParameterDto>();
            }
        }
    }
}
