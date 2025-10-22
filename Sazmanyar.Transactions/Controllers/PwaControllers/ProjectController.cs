using ApplicationService.ServicesContract.Pwa;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.PwaControllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectServices;
        public ProjectController(IProjectService projectServices)
        {
            _projectServices = projectServices;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _projectServices.GetAll();
            return Ok(result);
        }
    }
}
