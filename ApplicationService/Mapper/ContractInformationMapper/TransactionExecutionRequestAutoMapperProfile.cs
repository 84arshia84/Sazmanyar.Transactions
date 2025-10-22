using AppCore.Entities.ContractsInformation.ContractCheckListValues;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ExecutionRequestCheckListValues;
using AppCore.Entities.ContractsInformation.ExecutionRequestServiceExplanations;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    internal static class TransactionExecutionRequestAutoMapperProfile
    {
        public static async Task<
            (TransactionExecutionRequest transaction,
            List<ExecutionRequestServiceExplanation> serviceExplanation)
            > DtoToEntity(TransactionExecutionRequestDto dto, LoginUserDto userDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var transaction = new TransactionExecutionRequest();
                var ExecutionServiceExplanations = new List<ExecutionRequestServiceExplanation>();
                var contractCheckList = new ExecutionRequestCheckListValue();

                if (dto.id == Guid.Empty)
                {
                    transaction.LastActionTitle = $"ثبت شده توسط کاربر {userDto.FullQualifyName}";
                    transaction.CurrentStatusTitle = $"در انتظار ارسال کاربر {userDto.FullQualifyName}";
                    transaction.InsertBy = userDto.ID;
                }
                transaction.InsertBy = transaction.InsertBy;
                transaction.Id = dto.id == Guid.Empty ? Guid.NewGuid() : dto.id;
                transaction.SubjectOfRequest = dto.subjectOfRequest;
                transaction.DateOfRequest = dto.dateOfRequest ;
                transaction.NumberOfRequest = dto.numberOfRequest;
                transaction.EstimatedTimeForDoingRequest = dto.estimatedTimeForDoingRequest;
                transaction.ExplenationRequest = dto.explenationRequest;
                transaction.ExplenationRequestOfConsultant = dto.explenationRequestOfConsultant;
                transaction.InsertDate = dto.insertDate == null ? DateTime.Now : (DateTime)dto.insertDate;
                transaction.IsDeleted = false;
                transaction.DeletedBy = Guid.Empty;
                transaction.ContractTypeId = dto.contractTypeId;
                transaction.OrganizationId = dto.organizationId;

                ExecutionServiceExplanations = dto.serviceExplanationDtos != null ? ExecutionRequestServiceExplanationAutoMapperProfile.DtosToEntities(
                    dto.serviceExplanationDtos, transaction.Id, errorLoggerService) : null;

                if (dto.checkListValueDto != null)
                {
                    if (dto.checkListValueDto.id == Guid.Empty)
                    {
                        dto.checkListValueDto.id = Guid.NewGuid();
                    }
                    contractCheckList.Id = dto.checkListValueDto.id;
                    contractCheckList.CheckListValues = dto.checkListValueDto.checkListValues;
                    contractCheckList.TransactionExecutionRequestsId = transaction.Id;
                    transaction.ExecutionRequestCheckListValueId = contractCheckList.Id;
                    transaction.ExecutionRequestCheckListValue = contractCheckList;
                }

                return (transaction, ExecutionServiceExplanations);
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new TransactionExecutionRequest(), new List<ExecutionRequestServiceExplanation>());
            }

        }
        public static async Task<TransactionExecutionRequestDto> EntityToDto(TransactionExecutionRequest entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new TransactionExecutionRequestDto();
                dto.id = entity.Id;
                dto.subjectOfRequest = entity.SubjectOfRequest;
                dto.insertDate = entity.InsertDate;
                dto.dateOfRequest = entity.DateOfRequest;
                dto.numberOfRequest = entity.NumberOfRequest;
                dto.estimatedTimeForDoingRequest = entity.EstimatedTimeForDoingRequest;
                dto.explenationRequest = entity.ExplenationRequest;
                dto.explenationRequestOfConsultant = entity.ExplenationRequestOfConsultant;
                dto.contractTypeId = entity.ContractTypeId;
                dto.organizationId = entity.OrganizationId;
                dto.serviceExplanationDtos = null;
                dto.IsfinalApproved = entity.IsFinalApprove;
                if (entity.ExecutionRequestCheckListValue != null)
                {
                    dto.checkListValueDto = new ExecutionRequestCheckListValueDto();
                    dto.checkListValueDto.id = entity.ExecutionRequestCheckListValue.Id;
                    dto.checkListValueDto.checkListValues = entity.ExecutionRequestCheckListValue.CheckListValues;
                }

                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new TransactionExecutionRequestDto();
            }
        }

        public static async Task<List<TransactionExecutionRequestDto>> EntitesToDtos(List<TransactionExecutionRequest> entities, IErrorLoggerService errorLoggerService)
        {
            var dtos = new List<TransactionExecutionRequestDto>();
            var i = 1;
            foreach (var entity in entities)
            {
                try
                {
                    var dto = new TransactionExecutionRequestDto();
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
