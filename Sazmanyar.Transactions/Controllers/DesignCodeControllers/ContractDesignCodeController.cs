using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.DesignCode;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.DesignCodeControllers
{
    public class ContractDesignCodeController : BaseController
    {
        private readonly IContractDesignCodeService _contractDesignCodeService;
        public ContractDesignCodeController(IContractDesignCodeService contractDesignCodeService)
        {
            _contractDesignCodeService = contractDesignCodeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ContractDesignCodeAddDto obj)
        {
            var result = await _contractDesignCodeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contractDesignCodeService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllParameter")]
        public async Task<IActionResult> GetAllParameter()
        {
            var result = await _contractDesignCodeService.GetAllParameter();
            return Ok(result);
        }
        [HttpPost("GetAllParameterById")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            var result = await _contractDesignCodeService.GetAllParameterById(id);
            return Ok(result);
        }
        [HttpPost("GenerateDesignCode")]
        public async Task<IActionResult> GenerateDesignCode([FromBody] ContractDesignCodeSearchParameterDto obj)
        {
            var result = await _contractDesignCodeService.GenerateDesignCode(obj);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ContractDesignCodeUpdateDto obj)
        {
            var result = await _contractDesignCodeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateCounter")]
        public async Task<IActionResult> UpdateCounter([FromQuery] Guid id)
        {
            await _contractDesignCodeService.UpdateCounter(id);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _contractDesignCodeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
