using ApplicationService.ServicesContract.Users;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Sazmanyar.Transactions.Controllers.UserControllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var connectionString = _config["ConnectionStrings:DbConnection"];
            var result = await _userService.GetAll(connectionString);
            return Ok(result);
        }
        [HttpPost("Get")]
        public async Task<IActionResult> Get([FromQuery] string fullQualifyName)
        {
            var connectionString = _config["ConnectionStrings:DbConnection"];
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.GetByFullQualifyName(UserName, connectionString);
            return Ok(result);
        }
        [HttpPost("GetUserPermissionsAndSaveAccess")]
        public async Task<IActionResult> GetUserPermissionsAndSaveAccess()
        {
            var connectionString = _config["ConnectionStrings:DbConnection"];
            var UserName = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var result = await _userService.GetUserPermissionsAndSaveAccess(UserName, connectionString);
            return Ok(result);
        }
    }
}
