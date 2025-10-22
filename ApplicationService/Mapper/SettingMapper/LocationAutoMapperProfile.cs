using AppCore.Entities.SettingEntities.Locations;
using ApplicationService.DtoModels.LocationDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.SettingMapper
{
    internal static class LocationAutoMapperProfile
    {
        public static City DtoToEntity(CityDto dto, IErrorLoggerService errorLoggerService)
        {
			try
			{
                var entity=new City();
                entity.Id = dto.Id;
                entity.Name = dto.Name;
                entity.ProvinceId = dto.ProvinceId;
                entity.CountyId = dto.CountyId;
                return entity;
			}
			catch (Exception ex)
			{
                errorLoggerService.SaveError(ex);
                return new City();
            }
        }
        public static CityDto EntityToDto(City entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new CityDto();
                dto.Id = entity.Id;
                dto.Name = entity.Name;
                dto.CountyId= entity.CountyId;
                dto.ProvinceId= entity.ProvinceId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new CityDto();
            }
        }
        public static County DtoToEntity(CountyDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity= new County();
                entity.Id = dto.Id;
                entity.Name = dto.Name;
                entity.ProvinceId= dto.ProvinceId;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new County();
            }
        }
        public static CountyDto EntityToDto(County entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new CountyDto();
                dto.Id = entity.Id;
                dto.Name = entity.Name;
                dto.ProvinceId = entity.ProvinceId;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new CountyDto();
            }
        }
        public static Province DtoToEntity(ProvinceDto dto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entity = new Province();
                entity.Id = dto.Id;
                entity.Name = dto.Name;
                return entity;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new Province();
            }
        }
        public static ProvinceDto EntityToDto(Province entity, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new ProvinceDto();
                dto.Id = entity.Id;
                dto.Name = entity.Name;
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new ProvinceDto();
            }
        }
        public static List<City> DtosToEntities(List<CityDto> dtos,IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities= new List<City>();
                foreach (var d in dtos)
                {
                    var entity = new City();
                    entity=DtoToEntity(d, errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<City>();
            }
        }
        public static List<CityDto> EntitiesToDtos(List<City> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<CityDto>();
                foreach (var e in entities)
                {
                    var dto = new CityDto();
                    dto = EntityToDto(e, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<CityDto>();
            }
        }
        public static List<County> DtosToEntities(List<CountyDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<County>();
                foreach (var d in dtos)
                {
                    var entity = new County();
                    entity = DtoToEntity(d, errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<County>();
            }
        }
        public static List<CountyDto> EntitiesToDtos(List<County> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<CountyDto>();
                foreach (var e in entities)
                {
                    var dto = new CountyDto();
                    dto = EntityToDto(e, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<CountyDto>();
            }
        }
        public static List<Province> DtosToEntities(List<ProvinceDto> dtos, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var entities = new List<Province>();
                foreach (var d in dtos)
                {
                    var entity = new Province();
                    entity = DtoToEntity(d, errorLoggerService);
                    entities.Add(entity);
                }
                return entities;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<Province>();
            }
        }
        public static List<ProvinceDto> EntitiesToDtos(List<Province> entities, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<ProvinceDto>();
                foreach (var e in entities)
                {
                    var dto = new ProvinceDto();
                    dto = EntityToDto(e, errorLoggerService);
                    dtos.Add(dto);
                }
                return dtos;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<ProvinceDto>();
            }
        }
    }
}
