using AppCore.Entities.SettingEntities.CheckLists;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class CheckListAutoMapperProfile
    {
        public static CheckList DtoToEntity(CheckListDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new CheckList();
                entity.Title = dto.Title;
                entity.Type = dto.Type;
                entity.Id = dto.Id;
                entity.IsRequired = dto.IsRequired;
                entity.ContractTypeId = dto.ContractTypeId;
                entity.Order = dto.row;
                entity.LookUpTableId = dto.LookUpTableId;
                //entity.SystemParts = dto.SystemParts.ToList();
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new CheckList();
            }

        }
        public static CheckListDto EntityToDto(CheckList entity,IErrorLoggerService errorLoggerService, int? row = null)
        {
            try
            {
                var dto = new CheckListDto();
                dto.Title = entity.Title;
                dto.Id = entity.Id;
                dto.Type = entity.Type;
                dto.ContractTypeId = entity.ContractTypeId;
                dto.row = row;
                dto.IsRequired = entity.IsRequired;
                dto.LookUpTableId = entity.LookUpTableId;
                //dto.SystemParts = entity.SystemParts.ToList();
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new CheckListDto();
            }

        }
        public static List<CheckList> DtosToEntities(List<CheckListDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<CheckList>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new CheckList();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<CheckList>();
            }
        }
        public static List<CheckListDto> EntitiesToDtos(List<CheckList> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<CheckListDto>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new CheckListDto();
                    dto = EntityToDto(entities[i], errorLoggerService, i + 1);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<CheckListDto>();
            }

        }
    }
}
