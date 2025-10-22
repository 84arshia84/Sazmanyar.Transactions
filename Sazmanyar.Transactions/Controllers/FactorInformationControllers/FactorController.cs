using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.Factor;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.FactorInformation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.FactorInformationControllers
{
    public class FactorController : BaseController
    {
        private readonly IFactorService _factorService;
        public FactorController(IFactorService factorService)
        {
            _factorService = factorService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] FactorAddDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.Add(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.GetAll(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllMine")]
        public async Task<IActionResult> GetAllMine()
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.GetAllMine(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitForAction")]
        public async Task<IActionResult> GetAllWaitForAction()
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.GetAllWaitForAction(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWaitForApprove")]
        public async Task<IActionResult> GetAllWaitForApprove()
        {
            string UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.GetAllWaitForApprove(UserName);
            return Ok(result);
        }
        [HttpGet("GetAllWithForApprove")]
        public async Task<IActionResult> GetAllWithFinalApprove()
        {
            var result = await _factorService.GetAllWithFinalApprove();
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _factorService.Get(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FactorUpdateDto obj)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.Update(obj, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, int m)
        {
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _factorService.Delete(id, UserName);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string userName, [FromQuery] int situation, [FromQuery] string? term)
        {
            var list = await _factorService.Search(userName, situation, term);
            return Ok(list);
        }
    }
}
