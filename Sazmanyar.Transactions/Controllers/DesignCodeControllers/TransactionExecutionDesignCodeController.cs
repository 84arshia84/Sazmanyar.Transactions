using ApplicationService.DtoModels.DesignCodeDtos.InvoiceDesignCodeDtos;
using ApplicationService.DtoModels.DesignCodeDtos.TransactionExecutionDesignCodeDtos;
using ApplicationService.ServicesContract.DesignCode;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.DesignCodeControllers
{
    public class TransactionExecutionDesignCodeController : BaseController
    {
        private readonly ITransactionExecutionDesignCodeService _transactionDesignCodeService;
        public TransactionExecutionDesignCodeController(ITransactionExecutionDesignCodeService transactionDesignCodeService)
        {
            _transactionDesignCodeService = transactionDesignCodeService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TransactionDesignCodeAddDto obj)
        {
            var result = await _transactionDesignCodeService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _transactionDesignCodeService.GetAll();
            return Ok(result);
        }
        [HttpPost("GetAllParameter")]
        public async Task<IActionResult> GetAllParameter()
        {
            var result = await _transactionDesignCodeService.GetAllParameter();
            return Ok(result);
        }
        [HttpPost("GetAllParameterById")]
        public async Task<IActionResult> GetAllForContract([FromQuery] Guid id)
        {
            var result = await _transactionDesignCodeService.GetAllParameterById(id);
            return Ok(result);
        }
        [HttpPost("GenerateDesignCode")]
        public async Task<IActionResult> GenerateDesignCode([FromBody] TransactionDesignCodeSearchParameterDto obj)
        {
            var result = await _transactionDesignCodeService.GenerateDesignCode(obj);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] TransactionDesignCodeUpdateDto obj)
        {
            var result = await _transactionDesignCodeService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpPut("UpdateCounter")]
        public async Task<IActionResult> UpdateCounter([FromQuery] Guid id)
        {
            await _transactionDesignCodeService.UpdateCounter(id);
            return Ok();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _transactionDesignCodeService.Delete(id);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
