using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.FactorDtos.FactorPayment;
using ApplicationService.ServicesContract.FactorInformation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.FactorInformationControllers
{
    public class FactorPaymentController : BaseController
    {
        private readonly IFactorPaymentService _factorPaymentService;
        public FactorPaymentController(IFactorPaymentService factorPaymentService)
        {
            _factorPaymentService = factorPaymentService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FactorPaymentAddDto obj)
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorPaymentService.Add(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid factorId)
        {
            var result = await _factorPaymentService.GetAll(factorId);
            return Ok(result);
        }

        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _factorPaymentService.Get(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FactorPaymentUpdateDto obj)
        {
            var result = await _factorPaymentService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorPaymentService.Delete(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
    }
}
