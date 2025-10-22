using AppCore.UnitOfWork;
using ApplicationService.DtoModels.LocationDtos;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    internal class LocationService : ILocationService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;

        public LocationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<List<CityDto>> GetAllCities()
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllCities(),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CityDto>();
            }
        }

        public async Task<List<CityDto>> GetAllCitiesByCounty(int countyId)
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllCitiesByCounty(countyId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CityDto>();
            }
        }

        public async Task<List<CityDto>> GetAllCitiesByProvince(int provinceId)
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllCitiesByProvince(provinceId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CityDto>();
            }
        }

        public async Task<List<CountyDto>> GetAllCounties()
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllCounties(),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CountyDto>();
            }
        }

        public async Task<List<CountyDto>> GetAllCountiesByProvince(int provinceId)
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllCountiesByProvince(provinceId),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<CountyDto>();
            }
        }

        public async Task<List<ProvinceDto>> GetAllProvince()
        {
            try
            {
                return LocationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.LocationRepository.GetAllProvince(),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ProvinceDto>();
            }
        }

        public async Task<CityDto> GetCityById(int id)
        {
            try
            {
                return LocationAutoMapperProfile.EntityToDto(await _unitOfWork.LocationRepository.GetCityById(id),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new CityDto();
            }
        }

        public async Task<CountyDto> GetCountyById(int id)
        {
            try
            {
                return LocationAutoMapperProfile.EntityToDto(await _unitOfWork.LocationRepository.GetCountyById(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new CountyDto();
            }
        }

        public async Task<ProvinceDto> GetProvinceById(int id)
        {
            try
            {
                return LocationAutoMapperProfile.EntityToDto(await _unitOfWork.LocationRepository.GetProvinceById(id), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new ProvinceDto();
            }
        }
    }
}
