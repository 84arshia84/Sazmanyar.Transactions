using AppCore.Entities.PwaEntities.Projects;
using ApplicationService.DtoModels.PwaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.PwaMapper
{
    internal class ProjectAutoMapprProfile
    {
        public static Project DtoToEntity(ProjectDto dto)
        {
            var entity = new Project();
            entity.ProjectName = dto.Title;
            entity.ProjectUID = dto.Id;
            return entity;
        }
        public static ProjectDto EntityToDto(Project entity)
        {
            var dto = new ProjectDto();
            dto.Title = entity.ProjectName;
            dto.Id = entity.ProjectUID;
            return dto;
        }
        public static List<Project> DtosToEntities(List<ProjectDto> dtos)
        {
            var entities = new List<Project>();
            for (int i = 0; i < dtos.Count; i++)
            {
                var entity = new Project();
                entity = DtoToEntity(dtos[i]);
                entities.Add(entity);
            }
            return entities;
        }
        public static List<ProjectDto> EntitiesToDtos(List<Project> entities)
        {
            var dtos = new List<ProjectDto>();
            for (int i = 0; i < entities.Count; i++)
            {
                var dto = new ProjectDto();
                dto = EntityToDto(entities[i]);
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
