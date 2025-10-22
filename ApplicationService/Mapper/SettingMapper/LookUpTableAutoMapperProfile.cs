using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.LookUpTables;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class LookUpTableAutoMapperProfile
    {
        public static LookUpTable DtoToEntity(LookUpTableDtos dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new LookUpTable();
                entity.Title = dto.Title;
                entity.ID = dto.Id;
                entity.Order = (int)dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new LookUpTable();
            }

        }
        public static LookUpTableInside DtoToEntity(LookUpTableInsideDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new LookUpTableInside();
                entity.Title = dto.title;
                entity.ID = dto.key;
              //  entity.Order = (int)dto.row;
                entity.LookUpTableId = dto.lookUpTableId;
                entity.ParentId = dto.parentKey;

                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new LookUpTableInside();
            }

        }
        public static LookUpTableDtos EntityToDto(LookUpTable entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new LookUpTableDtos();
                dto.Title = entity.Title;
                dto.Id = entity.ID;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new LookUpTableDtos();
            }

        }
        public static List<LookUpTable> DtosToEntities(List<LookUpTableDtos> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<LookUpTable>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new LookUpTable();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<LookUpTable>();
            }
        }
        public static List<LookUpTableInside> DtosToEntities(List<LookUpTableInsideDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<LookUpTableInside>();
                for (int i = 0; i < dtos.Count; i++)
                {
                    var entity = new LookUpTableInside();
                    entity = DtoToEntity(dtos[i], errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<LookUpTableInside>();
            }
        }
        public static List<LookUpTableDtos> EntitiesToDtos(List<LookUpTable> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<LookUpTableDtos>();
                for (int i = 0; i < entities.Count; i++)
                {
                    var dto = new LookUpTableDtos();
                    dto = EntityToDto(entities[i], errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<LookUpTableDtos>();
            }

        }
        public static List<LookUpTableInsideDto> EntitiesToDtos(List<LookUpTableInside> lookup)
        {
            var lookupInside = lookup.ToLookup(c => c.ParentId);
            return BuildChildrenDto(null, lookupInside);
        }
        public static List<LookUpTableInsideDto> BuildChildrenDto(Guid? parentId, ILookup<Guid?, LookUpTableInside> childstructure)
        {
            return childstructure[parentId]
                .Select(node => new LookUpTableInsideDto
                {
                    key = node.ID,
                    title = node.Title,
                    parentKey = node.ParentId,
                    lookUpTableId = node.LookUpTableId,
                    children = BuildChildrenDto(node.ID, childstructure),
                }).ToList();
        }
    }
}
