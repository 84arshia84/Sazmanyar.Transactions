using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.Activitycenters;
using AppCore.Entities.SettingEntities.CreditSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class CreditSourceAutoMapperProfile
    {
        public static CreditSource DtoToEntity(CreditSourceDto dto)
        {
            var entity = new CreditSource();
            entity.Title = dto.title;
            entity.CreditSourceCode = dto.CreditSourceCode;
            entity.ID = dto.key;
            entity.ParentID = dto.parentKey;
            return entity;
        }
        public static CreditSourceDto EntityToDto(CreditSource entity, Int64 row)
        {
            var dto = new CreditSourceDto();
            dto.title = entity.Title;
            dto.CreditSourceCode = entity.CreditSourceCode;
            //dto.row = row.ToString();
            dto.key = entity.ID;
            dto.parentKey = entity.ParentID;
            return dto;
        }
        public static List<CreditSourceDto> EntitiesToDtos(this List<CreditSource> creditSource)
        {
            var creditSources = creditSource.ToLookup(c => c.ParentID);
            return BuildChildrenDto(null, creditSources);
        }
        public static List<CreditSourceDto> BuildChildrenDto(Guid? parentId, ILookup<Guid?, CreditSource> childstructure)
        {
            return childstructure[parentId]
                .Select(node => new CreditSourceDto
                {
                    key = node.ID,
                    title = node.Title,
                    parentKey = node.ParentID,
                    CreditSourceCode = node.CreditSourceCode,
                    //row = childstructure.Count(),
                    children = BuildChildrenDto(node.ID, childstructure),
                }).ToList();
        }
        public static List<CreditSource> DtosToEntities(List<CreditSourceDto> dtos)
        {
            var entities = new List<CreditSource>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new CreditSource();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
       
    }
}
