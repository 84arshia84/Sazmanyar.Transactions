using AppCore.Entities.Attaches;
using AppCore.Entities.Organizations;
using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.OrganizationInformationDtos;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.AccountMapperProfile
{
    public class AccountMapperProfile
    {
        public static Account DtoToEntity(AccountDto dto, IErrorLoggerService errorLogger)
        {
            try
            {
                var entity = new Account();
                entity.AccountNumber = dto.AccountNumber;
                entity.Bank = dto.Bank;
                entity.Id = dto.Id;
                entity.Branch = dto.Branch;
                entity.Sheba = dto.Sheba;
                entity.IsActive = dto.IsActive;
                entity.OrganizationInformationId = dto.OrganizationInformationId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new Account();
            }

        }
        public static AccountDto EntityToDto(Account entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new AccountDto();
                dto.AccountNumber = entity.AccountNumber;
                dto.Bank = entity.Bank;
                dto.Id = entity.Id;
                dto.Branch = entity.Branch;
                dto.Sheba = entity.Sheba;
                dto.IsActive = entity.IsActive;
                dto.OrganizationInformationId = entity.OrganizationInformationId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new AccountDto();
            }

        }
        public static List<Account> DtosToEntities(List<AccountDto> dtos, IErrorLoggerService errorLogger)
        {
            var entities = new List<Account>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Account();
                entity = DtoToEntity(dtos[i], errorLogger);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<AccountDto> EntitiesToDtos(List<Account> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<AccountDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new AccountDto();
                dto = EntityToDto(entities[i], errorLogger);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}

