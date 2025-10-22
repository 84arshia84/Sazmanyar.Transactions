using AppCore.Entities.ContractAccessGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.User
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll(string connectionString);
        Task<User> GetByFullQualifyName(string fullqualify, string connectionString);
        Task<Guid> GetLoggedInUserId(string fullqualify, string connectionString);

    }
}
