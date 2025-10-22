using AppCore.Entities.SettingEntities.Locations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    internal class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _context;
        public LocationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// گرفتن تمامی شهر ها 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<City>> GetAllCities()
        {
            try
            {
                return await _context.Citys.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی شهر های شهرستان
        /// </summary>
        /// <param name="countyId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<City>> GetAllCitiesByCounty(int countyId)
        {
            try
            {
                return await _context.Citys.Where(x=>x.CountyId == countyId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی شهر های استان
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<City>> GetAllCitiesByProvince(int provinceId)
        {
            try
            {
                return await _context.Citys.Where(x=>x.ProvinceId == provinceId).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی شهرستان ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<County>> GetAllCounties()
        {
            try
            {
                return await _context.Counties.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی شهرستان های استان
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<County>> GetAllCountiesByProvince(int provinceId)
        {
            try
            {
                return await _context.Counties.Where(x=>x.ProvinceId==provinceId).ToListAsync();    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی استان ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Province>> GetAllProvince()
        {
            try
            {
                return await _context.Provinces.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن شهر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<City> GetCityById(int id)
        {
            try
            {
                return await _context.Citys.Where(x=>x.Id==id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن شهرستان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<County> GetCountyById(int id)
        {
            try
            {
                return await _context.Counties.Where(x=>x.Id== id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن استان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Province> GetProvinceById(int id)
        {
            try
            {
                return await _context.Provinces.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
