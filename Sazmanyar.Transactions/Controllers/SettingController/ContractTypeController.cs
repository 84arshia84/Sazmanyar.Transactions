using AppCore.Enums;
using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class ContractTypeController : BaseController
    {
        private readonly IContractTypeService _contractTypeService;
        public ContractTypeController(IContractTypeService contractTypeService)
        {
            _contractTypeService = contractTypeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ContractTypeDto obj)
        {
            var result = await _contractTypeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contractTypeService.GetAll();
            return Ok(result);
        }
        [HttpGet("GetAllWithAccessGroupEffect")]
        public async Task<IActionResult> GetAllWithAccessGroupEffect([FromQuery] Guid id, [FromQuery] int part, [FromQuery] int property, [FromQuery] int mode)
        {
            var result = await _contractTypeService.GetAllWithAccessGroupEffect(id, part, property, mode);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContractTypeDto obj)
        {
            var result = await _contractTypeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateOfficeOnline")]
        public async Task<IActionResult> UpdateOfficeOnline([FromQuery] Guid contractTypeId, Guid? filenameId)
        {
            var result = await _contractTypeService.UpdateOfficeOnline(contractTypeId, filenameId);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _contractTypeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }

        
    }
}
