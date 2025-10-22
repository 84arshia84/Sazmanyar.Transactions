using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.FinePaymentMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class FinePaymentMethodAutoMapperProfile
    {
        public static FinePaymentMethod DtoToEntity(FinePaymentMethodDto dto)
        {
            var entity = new FinePaymentMethod();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static FinePaymentMethodDto EntityToDto(FinePaymentMethod entity, Int64 row)
        {
            var dto = new FinePaymentMethodDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<FinePaymentMethod> DtosToEntities(List<FinePaymentMethodDto> dtos)
        {
            var entities = new List<FinePaymentMethod>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new FinePaymentMethod();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<FinePaymentMethodDto> EntitiesToDtos(List<FinePaymentMethod> entities)
        {
            var dtos = new List<FinePaymentMethodDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new FinePaymentMethodDto();
                dto = EntityToDto(entities[i], i+1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
