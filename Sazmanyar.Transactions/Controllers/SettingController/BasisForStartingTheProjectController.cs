using ApplicationService.DtoModels.SettingDtos;
using AppCore.Entities.SettingEntities.BasisForStartingTheProjects;
using ApplicationService.Services.SettingServices;
using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class BasisForStartingTheProjectController : BaseController
    {
        private readonly IBasisForStartingTheProjectService _basisForStartingTheProject;
        public BasisForStartingTheProjectController(IBasisForStartingTheProjectService basisForStartingTheProjectService)
        {
            _basisForStartingTheProject = basisForStartingTheProjectService;
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] BasisForStartingTheProjectDto obj)
        {
            var result = await _basisForStartingTheProject.Add(obj);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _basisForStartingTheProject.GetAll();
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] BasisForStartingTheProjectDto obj)
        {
            var result = await _basisForStartingTheProject.Update(obj);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _basisForStartingTheProject.Delete(id);
            return Ok(result);
        }
    }
}
