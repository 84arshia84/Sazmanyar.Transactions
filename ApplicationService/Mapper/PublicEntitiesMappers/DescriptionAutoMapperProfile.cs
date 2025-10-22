using AppCore.Entities.Descriptions;
using AppCore.Entities.SettingEntities.UnitOfMeasurements;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PublicEntitiesMappers
{
    internal class DescriptionAutoMapperProfile
    {
        public static Description DtoToEntity(DescriptionDto dto, IErrorLoggerService errorLogger, Guid? sectionId = null)
        {
            try
            {
                var entity = new Description();
                entity.Text = dto.Text;
                entity.Id = dto.Id;
                entity.AuthorName = dto.AuthorName;
                entity.AuthorFullQualifyName = dto.AuthorFullQualifyName;
                entity.SectionId = sectionId != null ? (Guid)sectionId : dto.SectionId;
                entity.WriteTime = dto.WriteTime;
                entity.CreateTime = dto.CreateTime;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new Description();
            }

        }
        public static DescriptionDto EntityToDto(Description entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new DescriptionDto();
                dto.Text = entity.Text;
                dto.Id = entity.Id;
                dto.AuthorName = entity.AuthorName;
                dto.AuthorFullQualifyName = entity.AuthorFullQualifyName;
                dto.SectionId = entity.SectionId;
                dto.WriteTime = entity.WriteTime;
                dto.CreateTime = entity.CreateTime;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new DescriptionDto();
            }

        }
        public static List<Description> DtosToEntities(List<DescriptionDto> dtos, IErrorLoggerService errorLogger , Guid? sectionId = null)
        {
            var entities = new List<Description>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Description();
                entity = DtoToEntity(dtos[i], errorLogger,sectionId);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<DescriptionDto> EntitiesToDtos(List<Description> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<DescriptionDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new DescriptionDto();
                dto = EntityToDto(entities[i], errorLogger);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
