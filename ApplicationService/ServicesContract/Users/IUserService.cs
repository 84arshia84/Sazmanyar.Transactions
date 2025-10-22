using ApplicationService.DtoModels.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.ServicesContract.Users
{
    public interface IUserService
    {
        public Task<List<LoginUserDto>> GetAll(string connectionString);
        public Task<LoginUserDto> GetByFullQualifyName(string fullqualify, string connectionString);
        public Task<UserAccessOnPermissionsDto> GetUserPermissionsAndSaveAccess(string fullqualify, string connectionString);
    }
}
