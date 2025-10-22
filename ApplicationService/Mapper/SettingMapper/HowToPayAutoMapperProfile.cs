using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.HowToPays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class HowToPayAutoMapperProfile
    {
        public static HowToPay DtoToEntity(HowToPayDto dto)
        {
            var entity = new HowToPay();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static HowToPayDto EntityToDto(HowToPay entity, Int64 row)
        {
            var dto = new HowToPayDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<HowToPay> DtosToEntities(List<HowToPayDto> dtos)
        {
            var entities = new List<HowToPay>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new HowToPay();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<HowToPayDto> EntitiesToDtos(List<HowToPay> entities)
        {
            var dtos = new List<HowToPayDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new HowToPayDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
