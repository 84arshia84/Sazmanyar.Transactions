using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.Organizations;
using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.ContractInformationMapper
{
    public class TransactionExecutionRequestSubjectAndIdMapper
    {
        public static TransactionExecutionRequest DtoToEntity(TransactionExecutionRequestSubjectAndIdDto dto, IErrorLoggerService errorLogger)

        {
            try
            {
                var entity = new TransactionExecutionRequest();
                entity.SubjectOfRequest = dto.SubjectOfRequest;
  
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new TransactionExecutionRequest();
            }

        }
        public static TransactionExecutionRequestSubjectAndIdDto EntityToDto(TransactionExecutionRequest entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new TransactionExecutionRequestSubjectAndIdDto();
                dto.SubjectOfRequest = entity.SubjectOfRequest;
                dto.Id = entity.Id;

                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new TransactionExecutionRequestSubjectAndIdDto();
            }

        }
        public static List<TransactionExecutionRequest> DtosToEntities(List<TransactionExecutionRequestSubjectAndIdDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<TransactionExecutionRequest>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new TransactionExecutionRequest();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<TransactionExecutionRequestSubjectAndIdDto> EntitiesToDtos(List<TransactionExecutionRequest> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<TransactionExecutionRequestSubjectAndIdDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new TransactionExecutionRequestSubjectAndIdDto();
                dto = EntityToDto(entities[i], errorLogger);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
