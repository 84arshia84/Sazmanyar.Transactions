using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class CheckListController : BaseController
    {
        private readonly ICheckListService _checkListService;
        public CheckListController(ICheckListService checkListService)
        {
            _checkListService = checkListService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CheckListDto obj)
        {
            var result = await _checkListService.Add(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] Guid id)
        {
            var result = await _checkListService.GetAll(id);
            return Ok(result);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var result = await _checkListService.Get(id);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CheckListDto obj)
        {
            var result = await _checkListService.Update(obj);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id, [FromQuery] Guid contractTypeId)
        {
            var result = await _checkListService.Delete(id,contractTypeId);
            return Ok(new { message = result.message, isSuccess = result.isSuccess });
        }

        [HttpGet("{checkListId}/lookup-tree")]
        public async Task<IActionResult> GetLookUpTreeByCheckListId(Guid checkListId)
        {
            var result = await _checkListService.GetLookUpTreeByCheckListId(checkListId);
            return Ok(result);
        }
    }
}
