using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractGuarantees;
using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using ApplicationService.DtoModels.WFEDto;
using static Dapper.SqlMapper;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class ContractAutoMapperProfile
    {
        /// <summary>
        /// convert a dto to an entity
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>(Contract ,ContractTimeProfile  ,ContractFinancialDetails )</returns>
        public static async Task<
            (Contract contract,
            ContractTimeProfile contractTimeProfile,
            ContractFinancialDetails contractFinancialDetails,
            List<ServiceExplanation> serviceExplanation,
            List<ContractCoefficient> contractCoefficients,
            List<ContractGuarantee> contractGuarantees)
            > DtoToEntity(ContractDto dto, LoginUserDto userDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var contracts = new Contract();
                var contractTimeProfiles = new ContractTimeProfile();
                var contractFinancialDetails = new ContractFinancialDetails();
                var contractServiceExplanations = new List<ServiceExplanation>();
                var contractEstimatedmeters = new List<ContractEstimatedmeter>();
                var contractCoefficient = new List<ContractCoefficient>();
                var contractGuarantee = new List<ContractGuarantee>();
                var contractCheckList = new ContractCheckListValue();


                if (dto.contractKey == Guid.Empty)
                {
                    contracts.LastActionTitle = $"ثبت شده توسط کاربر {userDto.FullQualifyName}";
                    contracts.CurrentStatusTitle = $"در انتظار ارسال کاربر {userDto.FullQualifyName}";
                    contracts.InsertContractBy = userDto.ID;
                }
                contracts.InsertContractBy = contracts.InsertContractBy;
                if (dto.TransactionId.HasValue)
                {
                    contracts.ID = dto.TransactionId.Value;
                }
                contracts.ID = dto.contractKey == Guid.Empty ? Guid.NewGuid() : dto.contractKey;
                contracts.TimeProfileID = dto.contractTimeProfileKey == Guid.Empty ? Guid.NewGuid() : dto.contractTimeProfileKey;
                contracts.FinancialDetailsID = dto.contractFinancialDetailsKey == Guid.Empty ? Guid.NewGuid() : dto.contractFinancialDetailsKey;
                contracts.ContractTitle = dto.contractTitle;
                contracts.ContractNumber = dto.contractNumber;
                contracts.ContractExpertID = dto.contractExpertKey;
                contracts.ContractExpertName = dto.contractExpertName;
                contracts.ConsultantID = dto.consultantID;
                contracts.ConsultantName = dto.consultantName;
                contracts.RequirementToCloseTheAccount = dto.requirementToCloseTheAccount;
                contracts.InsertContractDate = dto.contractInsertDate == null ? DateTime.Now : (DateTime)dto.contractInsertDate;
                contracts.IsDeleted = false;
                contracts.DeleteBy = Guid.Empty;
                contracts.ContractTypeID = dto.contractTypeID;
                contracts.TransActionTypeID = dto.transActionTypeID;
                contracts.RoleOFOrganizationID = dto.roleOFOrganizationID;
                contracts.CorespondentID = dto.corespondentID;
                contracts.CorespondentRealOrLegal = (bool)dto.corespondentRealOrLegal;
                contracts.OrganizationUnitID = dto.organizationUnitID;
                contracts.CreditSourceID = dto.creditSourceID;
                contracts.StatusId = dto.StatusId;

                contractTimeProfiles = await ContractTimeProfileAutoMapperProfile.DtoToEntity(dto);
                contractTimeProfiles.ID = contracts.TimeProfileID;
                contractTimeProfiles.ContractID = contracts.ID;

                contractFinancialDetails = await ContractFinancialDetailsAutoMapperProfile.DtoToEntity(dto);
                contractFinancialDetails.ContractID = contracts.ID;
                contractFinancialDetails.ID = contracts.FinancialDetailsID;

                contractServiceExplanations = dto.serviceExplanationDtos != null ? ServiceExplanationAutoMapperProfile.DtosToEntities(dto.serviceExplanationDtos, contracts.ID, errorLoggerService) : null;

                contractCoefficient = dto.contractCoefficientDtos != null ? ContractCoefficientAutoMapperProfile.DtosToEntities(dto.contractCoefficientDtos, contracts.ID, errorLoggerService) : null;

                contractGuarantee = dto.contractGuaranteeDtos != null ? await ContractGuaranteeAutoMapperProfile.DtosToEntities(dto.contractGuaranteeDtos, contracts.ID, errorLoggerService) : null;

                contracts.ContractCoefficients = contractCoefficient;

                if (dto.contractCheckListValueDto != null)
                {
                    if (dto.contractCheckListValueDto.id == Guid.Empty)
                    {
                        dto.contractCheckListValueDto.id = Guid.NewGuid();
                    }
                    contractCheckList.Id = dto.contractCheckListValueDto.id;
                    contractCheckList.CheckListValues = dto.contractCheckListValueDto.checkListValues;
                    contractCheckList.ContractId = contracts.ID;
                    contracts.ContractCheckListValueId = contractCheckList.Id;
                    contracts.ContractCheckListValues = contractCheckList;
                }

                return (contracts, contractTimeProfiles, contractFinancialDetails, contractServiceExplanations, contractCoefficient, contractGuarantee);
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new Contract(), new ContractTimeProfile(), new ContractFinancialDetails(), new List<ServiceExplanation>(), new List<ContractCoefficient>(), new List<ContractGuarantee>());
            }

        }
        public static async Task<ContractDto> EntityToDto(Contract entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ContractDto();

                dto.contractKey = entity.ID;
                dto.contractFinancialDetailsKey = entity.FinancialDetailsID;
                dto.contractTimeProfileKey = entity.TimeProfileID;
                dto.contractTitle = entity.ContractTitle;
                dto.contractNumber = entity.ContractNumber;
                dto.contractExpertKey = entity.ContractExpertID;
                dto.contractExpertName = entity.ContractExpertName;
                dto.consultantID = entity.ConsultantID;
                dto.consultantName = entity.ConsultantName;
                dto.requirementToCloseTheAccount = entity.RequirementToCloseTheAccount;
                dto.contractTypeID = entity.ContractTypeID;
                dto.transActionTypeID = entity.TransActionTypeID;
                dto.roleOFOrganizationID = entity.RoleOFOrganizationID;
                dto.corespondentID = entity.CorespondentID;
                dto.corespondentRealOrLegal = entity.CorespondentRealOrLegal;
                dto.creditSourceID = entity.CreditSourceID;
                dto.contractInsurance_Percent = entity.ContractFinancialDetails.ContractInsurance_Percent;
                dto.contractValue_Added_Percent = entity.ContractFinancialDetails.ContractValue_Added_Percent;
                dto.contractTax_Percent = entity.ContractFinancialDetails.ContractTax_Percent;
                dto.percentageOfChanges = entity.ContractFinancialDetails.PercentageOfChanges;
                dto.goodJob_Percent = entity.ContractFinancialDetails.GoodJob_Percent;
                dto.amount_of_timeExtension = entity.ContractFinancialDetails.Amount_of_timeExtension;
                dto.basis_of_Receipt = entity.ContractFinancialDetails.Basis_of_Receipt;
                dto.ContractDateOfNotification = entity.ContratTimeProfile.ContractDateOfNotification;
                dto.ContractExchangeDate = entity.ContratTimeProfile.ContractExchangeDate;
                dto.ContractPeriod = entity.ContratTimeProfile.ContractPeriod;
                dto.ContractStartDate = entity.ContratTimeProfile.ContractStartDate;
                dto.ContractEndDate = entity.ContratTimeProfile.ContractEndDate;
                dto.BasisForStartingProjectID = entity.ContratTimeProfile.BasisForStartingProjectID;
                dto.organizationUnitID = entity.OrganizationUnitID;
                dto.contractInsertDate = entity.InsertContractDate;
                dto.serviceExplanationDtos = null;
                dto.IsfinalApproved = entity.IsFinalApprove;
                dto.hasAddendum = entity.HasAddendum;
                dto.StatusId = entity.StatusId;
                if (entity.ContractCheckListValues != null)
                {
                    dto.contractCheckListValueDto = new ContractCheckListValueDto();
                    dto.contractCheckListValueDto.id = entity.ContractCheckListValues.Id;
                    dto.contractCheckListValueDto.checkListValues = entity.ContractCheckListValues.CheckListValues;
                }

                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ContractDto();
            }
        }

        public static async Task<List<ContractDto>> EntitesToDtos(List<Contract> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<ContractDto>();
            var i = 1;
            foreach (Contract entity in entities)
            {
                try
                {
                    var dto = new ContractDto();
                    dto = await EntityToDto(entity, errorLoggerService);
                    dto.row = i;
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
    }
}
