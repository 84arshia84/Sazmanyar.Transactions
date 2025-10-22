using AppCore.Entities.SettingEntities.Locations;
using ApplicationService.DtoModels.LocationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Setting
{
    public interface ILocationService
    {
        public Task<CityDto> GetCityById(int id);
        public Task<List<CityDto>> GetAllCities();
        public Task<List<CityDto>> GetAllCitiesByCounty(int countyId);
        public Task<List<CityDto>> GetAllCitiesByProvince(int provinceId);
        public Task<CountyDto> GetCountyById(int id);
        public Task<List<CountyDto>> GetAllCounties();
        public Task<List<CountyDto>> GetAllCountiesByProvince(int provinceId);
        public Task<ProvinceDto> GetProvinceById(int id);
        public Task<List<ProvinceDto>> GetAllProvince();
    }
}
