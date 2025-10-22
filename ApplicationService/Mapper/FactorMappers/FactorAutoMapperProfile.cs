using AppCore.Entities.FactorInformation.Factors;
using AppCore.Entities.User;
using ApplicationService.DtoModels.FactorDtos.Factor;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ApplicationService.Mapper.FactorMappers
{
    internal static class FactorAutoMapperProfile
    {
        public static Factor DtoToEntityAdd(FactorAddDto dto , Guid userId ,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new Factor();
                entity.Id = Guid.NewGuid();
                entity.FactorTitle = dto.FactorTitle;
                entity.FactorNumber = dto.FactorNumber; 
                entity.FactorExpertID = dto.FactorExpertID;
                entity.RequirementToCloseTheAccount = dto.RequirementToCloseTheAccount;
                entity.InsertFactorDate = DateTime.Now;
                entity.InsertFactorBy = userId;
                entity.IsDeleted = false;
                entity.FactorTypeId = dto.FactorTypeId;
                entity.OrganizationalunitId = dto.OrganizationalunitId;
                entity.RoleOfOrganizationId = dto.RoleOfOrganizationId;
                entity.CreditSourceID = dto.CreditSourceID;
                entity.CorespondentID = dto.CorespondentID;
                entity.CorespondentRealOrLegal = dto.CorespondentRealOrLegal;
                entity.FactorTimeProfileId = Guid.NewGuid();
                entity.FactorFinancialDetaileId = Guid.NewGuid();
                entity.IsFinalApprove = false;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static Factor DtoToEntityUpdate(FactorUpdateDto dto , Factor LastData ,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new Factor();
                entity.Id = dto.Id;
                entity.FactorTitle = dto.FactorTitle;
                entity.FactorNumber = dto.FactorNumber;
                entity.FactorExpertID = dto.FactorExpertID;
                entity.RequirementToCloseTheAccount = dto.RequirementToCloseTheAccount;
                entity.InsertFactorDate = LastData.InsertFactorDate;
                entity.InsertFactorBy = LastData.InsertFactorBy;
                entity.IsDeleted = false;
                entity.FactorTypeId = dto.FactorTypeId;
                entity.OrganizationalunitId = dto.OrganizationalunitId;
                entity.RoleOfOrganizationId = dto.RoleOfOrganizationId;
                entity.CreditSourceID = dto.CreditSourceID;
                entity.CorespondentID = dto.CorespondentID;
                entity.CorespondentRealOrLegal = dto.CorespondentRealOrLegal;
                entity.FactorTimeProfileId =LastData.FactorTimeProfileId;
                entity.FactorFinancialDetaileId = LastData.FactorFinancialDetaileId;
                entity.CurrentStageId = LastData.CurrentStageId;
                entity.CurrentStatusTitle = LastData.CurrentStatusTitle;
                entity.LastActionTitle = LastData.LastActionTitle;
                entity.IsFinalApprove = LastData.IsFinalApprove;
                return entity;

            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return null;
            }
        }
        public static FactorGetDto EntityToDto(Factor entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new FactorGetDto();
                dto.Id = entity.Id;
                dto.FactorTitle = entity.FactorTitle;
                dto.FactorNumber = entity.FactorNumber;
                dto.FactorExpertID = entity.FactorExpertID;
                dto.RequirementToCloseTheAccount = entity.RequirementToCloseTheAccount;
                dto.FactorTypeId = entity.FactorTypeId;
                dto.OrganizationalunitId = entity.OrganizationalunitId;
                dto.RoleOfOrganizationId = entity.RoleOfOrganizationId;
                dto.CreditSourceID = entity.CreditSourceID;
                dto.CorespondentID = entity.CorespondentID;
                dto.CorespondentRealOrLegal = entity.CorespondentRealOrLegal;
                dto.IsFinalApproved = entity.IsFinalApprove != null ? (bool)entity.IsFinalApprove : false ;
                dto.FactorTimeProfile = FactorTimeProfileAutoMapperProfile.EntityToDto(entity.FactorTimeProfile,errorLoggerService);
                dto.FactorFinancialDetaile = FactorFinancialDetaileAutoMapperProfile.EntityToDto(entity.FactorFinancialDetaile, errorLoggerService);
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new FactorGetDto();
            }
        }
        public static List<FactorGetDto> EntitiesToDtos(List<Factor> entities,IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<FactorGetDto>();
            foreach (Factor entity in entities)
            {
                try
                {
                    var dto = new FactorGetDto();
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
