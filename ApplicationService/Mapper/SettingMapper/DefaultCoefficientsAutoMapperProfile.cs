using AppCore.Entities.SettingEntities.DefaultCoefficients;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class DefaultCoefficientsAutoMapperProfile
    {
        public static DefaultCoefficients DtoToEntity(DefaultCoefficientsDto dto,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity= new DefaultCoefficients();
                entity.Id=dto.Id;
                entity.Title=dto.Title;
                entity.DefaultRows=dto.DefaultRows;
                entity.DefaultCoefficient=dto.DefaultCoefficient;
                entity.Order = dto.row;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new DefaultCoefficients();
            }
        }
        public static DefaultCoefficientsDto EntityToDto(DefaultCoefficients entity ,IErrorLoggerService errorLoggerService,int? row=null)
        {
            try
            {
                var dto= new DefaultCoefficientsDto();
                dto.Id=entity.Id;
                dto.Title=entity.Title;
                dto.DefaultRows=entity.DefaultRows;
                dto.DefaultCoefficient=entity.DefaultCoefficient;
                dto.row = (int)row != null ? (int)row : 1;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new DefaultCoefficientsDto();    
            }
        }
        public static List<DefaultCoefficients> DtosToEntites(List<DefaultCoefficientsDto> dtos,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities=new List<DefaultCoefficients>();
               
                foreach (var item in dtos)
                {
                    var entity= new DefaultCoefficients();
                    entity= DtoToEntity(item,errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<DefaultCoefficients>(); 
            }
        }
        public static List<DefaultCoefficientsDto> EntitiesToDtos (List<DefaultCoefficients> entities,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos=new List<DefaultCoefficientsDto>();
                int count = 1;
                foreach (var item in entities)
                {
                    var dto = new DefaultCoefficientsDto();
                    dto = EntityToDto(item, errorLoggerService,count);
                    dtos.Add(dto);
                    count++;
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<DefaultCoefficientsDto>();
            }
        }
    }
}
