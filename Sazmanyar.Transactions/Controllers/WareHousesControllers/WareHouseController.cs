using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WareHouses;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Sazmanyar.Transactions.Controllers.WareHousesControllers
{
    public class WareHouseController :BaseController
    {
        private readonly IWareHouseService _warehouseService;
        private readonly IConfiguration _config;
        public WareHouseController(IWareHouseService warehouseService, IConfiguration config)
        {
            _config = config;
            _warehouseService = warehouseService;
        }
        [HttpPost("GetAllSupplyList")]
        public async Task<IActionResult> GetAllSupplyList()
        {
            var result = await _warehouseService.GetAllSupplyList();
            return Ok(result);
        }
        [HttpPost("GetAllCommodity")]
        public async Task<IActionResult> GetAllCommodity([FromBody] List<Guid> sipplyListId)
        {
            var result = await _warehouseService.GetAllCommodity(sipplyListId);
            return Ok(result);
        }
        [HttpGet("CheckConnection")]
        public async Task<IActionResult> CheckConnection()
        {
            var result = Convert.ToBoolean(_config["ConnectWithWareHouse:HasConnection"].ToString());
            return Ok(result);
        }
    }
}
