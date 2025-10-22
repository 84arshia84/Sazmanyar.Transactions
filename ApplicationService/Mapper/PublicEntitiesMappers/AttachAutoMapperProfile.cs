using AppCore.Entities.Attaches;
using AppCore.Entities.Descriptions;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PublicEntitiesMappers
{
    internal static class AttachAutoMapperProfile
    {
        public static Attach DtoToEntity(AttachDto dto, IErrorLoggerService errorLogger, Guid? sectionId = null)
        {
            try
            {
                var entity = new Attach();
                entity.UserUploader = dto.UserUploader;
                entity.Id = dto.Id;
                entity.FileName = dto.FileName;
                entity.FileExtention = dto.FileExtention;
                entity.UserUploaderName = dto.UserUploaderName;
                entity.SectionId = sectionId != null ? (Guid)sectionId : dto.SectionId;
                entity.UploadeDate = dto.UploadeDate;
                entity.SharePointId = dto.SharePointId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new Attach();
            }

        }
        public static AttachDto EntityToDto(Attach entity, IErrorLoggerService errorLogger)
        {
            try
            {
                var dto = new AttachDto();
                dto.UserUploader = entity.UserUploader;
                dto.UserUploaderName = entity.UserUploaderName;
                dto.Id = entity.Id;
                dto.FileName = entity.FileName;
                dto.FileExtention = entity.FileExtention;
                dto.SectionId = entity.SectionId;
                dto.UploadeDate = entity.UploadeDate;
                dto.SharePointId = entity.SharePointId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLogger.SaveError(ex);
                return new AttachDto();
            }

        }
        public static List<Attach> DtosToEntities(List<AttachDto> dtos, IErrorLoggerService errorLogger, Guid? sectionId = null)
        {
            var entities = new List<Attach>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Attach();
                entity = DtoToEntity(dtos[i], errorLogger, sectionId);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<AttachDto> EntitiesToDtos(List<Attach> entities, IErrorLoggerService errorLogger)
        {
            var dtos = new List<AttachDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new AttachDto();
                dto = EntityToDto(entities[i], errorLogger);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
