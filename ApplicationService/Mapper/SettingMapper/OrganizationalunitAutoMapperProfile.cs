using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.CreditSources;
using AppCore.Entities.SettingEntities.Organizationalunits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal class OrganizationalunitAutoMapperProfile
    {
        public static Organizationalunit DtoToEntity(OrganizationalunitDto dto)
        {
            var entity = new Organizationalunit();
            entity.Title = dto.title;
            entity.ID = dto.key;
            entity.Order = (int)dto.row;
            return entity;
        }
        public static OrganizationalunitDto EntityToDto(Organizationalunit entity, Int64 row)
        {
            var dto = new OrganizationalunitDto();
            dto.title = entity.Title;
            dto.row = row;
            dto.key = entity.ID;
            return dto;
        }
        public static List<Organizationalunit> DtosToEntities(List<OrganizationalunitDto> dtos)
        {
            var entities = new List<Organizationalunit>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Organizationalunit();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<OrganizationalunitDto> EntitiesToDtos(List<Organizationalunit> entities)
        {
            var dtos = new List<OrganizationalunitDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new OrganizationalunitDto();
                dto = EntityToDto(entities[i], i + 1);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
