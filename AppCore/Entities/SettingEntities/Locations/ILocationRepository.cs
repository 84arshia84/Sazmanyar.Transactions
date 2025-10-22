using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.Locations
{
    public interface ILocationRepository
    {
        public Task<City> GetCityById(int id);
        public Task<List<City>> GetAllCities();
        public Task<List<City>> GetAllCitiesByCounty(int countyId);
        public Task<List<City>> GetAllCitiesByProvince(int provinceId);
        public Task<County> GetCountyById(int id);
        public Task<List<County>> GetAllCounties();
        public Task<List<County>> GetAllCountiesByProvince(int provinceId);
        public Task<Province> GetProvinceById(int id);
        public Task<List<Province>> GetAllProvince();


    }
}
