using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpPost("GetCity")]
        public async Task<IActionResult> GetCity([FromQuery] int id)
        {
            var result = await _locationService.GetCityById(id);
            return Ok(result);
        }
        [HttpGet("GetAllCities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _locationService.GetAllCities();
            return Ok(result);
        }
        [HttpPost("GetAllCitiesByCounty")]
        public async Task<IActionResult> GetAllCitiesByCounty([FromQuery] int id)
        {
            var result = await _locationService.GetAllCitiesByCounty(id);
            return Ok(result);
        }
        [HttpPost("GetAllCitiesByProvince")]
        public async Task<IActionResult> GetAllCitiesByProvince([FromQuery] int id)
        {
            var result = await _locationService.GetAllCitiesByProvince(id);
            return Ok(result);
        }
        [HttpPost("GetCounty")]
        public async Task<IActionResult> GetCounty([FromQuery] int id)
        {
            var result = await _locationService.GetCountyById(id);
            return Ok(result);
        }
        [HttpGet("GetAllCounties")]
        public async Task<IActionResult> GetAllCounties()
        {
            var result = await _locationService.GetAllCounties();
            return Ok(result);
        }
        [HttpPost("GetAllCountiesByProvince")]
        public async Task<IActionResult> GetAllCountiesByProvince([FromQuery] int id)
        {
            var result = await _locationService.GetAllCountiesByProvince(id);
            return Ok(result);
        }
        [HttpPost("GetProvince")]
        public async Task<IActionResult> GetProvince([FromQuery] int id)
        {
            var result = await _locationService.GetProvinceById(id);
            return Ok(result);
        }
        [HttpGet("GetAllProvince")]
        public async Task<IActionResult> GetAllProvince()
        {
            var result = await _locationService.GetAllProvince();
            return Ok(result);
        }
       
    }
}
