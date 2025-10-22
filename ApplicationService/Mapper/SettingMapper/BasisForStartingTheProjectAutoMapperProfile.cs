using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class BasisForStartingTheProjectAutoMapperProfile
    {
        public static BasisForStartingTheProject DtoToEntity(BasisForStartingTheProjectDto dto)
        {
            var entity = new BasisForStartingTheProject();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static BasisForStartingTheProjectDto EntityToDto(BasisForStartingTheProject entity, Int64 row)
        {
            var dto = new BasisForStartingTheProjectDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<BasisForStartingTheProject> DtosToEntities(List<BasisForStartingTheProjectDto> dtos)
        {
            var entities = new List<BasisForStartingTheProject>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new BasisForStartingTheProject();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<BasisForStartingTheProjectDto> EntitiesToDtos(List<BasisForStartingTheProject> entities)
        {
            var dtos = new List<BasisForStartingTheProjectDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new BasisForStartingTheProjectDto();
                dto = EntityToDto(entities[i], i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
