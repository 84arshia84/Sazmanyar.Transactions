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
    internal static class ContractFromTransactionMapper
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
            List<ContractCoefficient> contractCoefficients,
            List<ContractGuarantee> contractGuarantees)
            > DtoToEntity(ContractFromTransactionDto dto, LoginUserDto userDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var contracts = new Contract();
                var contractTimeProfiles = new ContractTimeProfile();
                var contractFinancialDetails = new ContractFinancialDetails();
                var contractEstimatedmeters = new List<ContractEstimatedmeter>();
                var contractCoefficient = new List<ContractCoefficient>();
                var contractGuarantee = new List<ContractGuarantee>();
                var contractCheckList = new ContractCheckListValue();

                if (dto.TransactionId == Guid.Empty)
                {
                    contracts.LastActionTitle = $"ثبت شده توسط کاربر {userDto.FullQualifyName}";
                    contracts.CurrentStatusTitle = $"در انتظار ارسال کاربر {userDto.FullQualifyName}";
                    contracts.InsertContractBy = userDto.ID;
                }
                contracts.InsertContractBy = contracts.InsertContractBy;
                contracts.ID = dto.TransactionId;
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

                contractTimeProfiles = await ContractTimeProfileFromTransactionMapper.DtoToEntity(dto);
                contractTimeProfiles.ID = contracts.TimeProfileID;
                contractTimeProfiles.ContractID = contracts.ID;

                contractFinancialDetails = await ContractFinancialDetailFromTransactionMapper.DtoToEntity(dto);
                contractFinancialDetails.ContractID = contracts.ID;
                contractFinancialDetails.ID = contracts.FinancialDetailsID;


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

                return (contracts, contractTimeProfiles, contractFinancialDetails, contractCoefficient, contractGuarantee);
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new Contract(), new ContractTimeProfile(), new ContractFinancialDetails(),new List<ContractCoefficient>(), new List<ContractGuarantee>());
            }

        }
        
        }
    }

