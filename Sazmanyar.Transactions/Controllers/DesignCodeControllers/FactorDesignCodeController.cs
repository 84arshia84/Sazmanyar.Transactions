using ApplicationService.DtoModels.DesignCodeDtos.ContractDesignCodeDtos;
using ApplicationService.DtoModels.DesignCodeDtos.FactorDesignCodeDtos;
using ApplicationService.ServicesContract.DesignCode;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.DesignCodeControllers
{
    public class FactorDesignCodeController : BaseController
    {
        private readonly IFactorDesignCodeService _factorDesignCodeService;
        public FactorDesignCodeController(IFactorDesignCodeService factorDesignCodeService)
        {
            _factorDesignCodeService = factorDesignCodeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FactorDesignCodeAddDto obj)
        {
            var result = await _factorDesignCodeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _factorDesignCodeService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllParameter")]
        public async Task<IActionResult> GetAllParameter()
        {
            var result = await _factorDesignCodeService.GetAllParameter();
            return Ok(result);
        }
        [HttpPost("GetAllParameterById")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            var result = await _factorDesignCodeService.GetAllParameterById(id);
            return Ok(result);
        }
        [HttpPost("GenerateDesignCode")]
        public async Task<IActionResult> GenerateDesignCode([FromBody] FactorDesignCodeSearchParameterDto obj)
        {
            var result = await _factorDesignCodeService.GenerateDesignCode(obj);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FactorDesignCodeUpdateDto obj)
        {
            var result = await _factorDesignCodeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateCounter")]
        public async Task<IActionResult> UpdateCounter([FromQuery] Guid id)
        {
            await _factorDesignCodeService.UpdateCounter(id);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _factorDesignCodeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
