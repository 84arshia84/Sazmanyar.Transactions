using ApplicationService.ServicesContract.Setting;
using Microsoft.AspNetCore.Mvc;

namespace Sazmanyar.Transactions.Controllers.SettingController
{
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAll();
            return Ok(result);
        }
    }
}
