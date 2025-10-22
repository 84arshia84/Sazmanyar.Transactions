using ApplicationService.DtoModels.SettingDtos;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class BasisFortheEndOftheProjectController : BaseController
    {
        private readonly IBasisFortheEndOftheProjectService _basisFortheEndOftheProject;
        public BasisFortheEndOftheProjectController(IBasisFortheEndOftheProjectService basisFortheEndOftheProject)
        {
            _basisFortheEndOftheProject = basisFortheEndOftheProject;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] BasisFortheEndOftheProjectDto obj)
        {
            var result = await _basisFortheEndOftheProject.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _basisFortheEndOftheProject.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] BasisFortheEndOftheProjectDto obj)
        {
            var result = await _basisFortheEndOftheProject.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _basisFortheEndOftheProject.Delete(id);
            return Ok(result);
        }
    }
}
