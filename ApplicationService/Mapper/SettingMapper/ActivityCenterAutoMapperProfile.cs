using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    public static class ActivityCenterAutoMapperProfile 
    {
        public static Activitycenter DtoToEntity(ActivitycenterDto dto)
        {
            var entity=new Activitycenter();
            entity.Title = dto.title;
            entity.Code = dto.code;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static ActivitycenterDto EntityToDto(Activitycenter entity,Int64 row)
        {
            var dto = new ActivitycenterDto();
            dto.title = entity.Title;
            dto.code = entity.Code;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<Activitycenter> DtosToEntities(List<ActivitycenterDto> dtos)
        {
            var entities=new List<Activitycenter>();
            for (int i = 0;i < dtos.Count ; i++)
            {
                var entity =new Activitycenter();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ActivitycenterDto> EntitiesToDtos(List<Activitycenter> entities)
        {
            var dtos=new List<ActivitycenterDto>();
            for (int i = 0; i < entities.Count ; i++)
            {
                var dto=new ActivitycenterDto();
                dto= EntityToDto(entities[i],i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
