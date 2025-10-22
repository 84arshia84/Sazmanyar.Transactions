using AppCore.Entities.User;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using AppCore.Entities.ContractAccessGroups;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Repository.UsersRepository
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAll(string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                string Query = @"Select ID, FirstName,LastName,FullQualifyName From dbo.users  where FirstName <> ''  and IsDeleted=0 and  IsInternal=1 order by FirstName ASC ,LastName ASC ";
                var datas = await dbConnection.QueryAsync <User>(Query);
                var result = datas.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetByFullQualifyName(string fullqualify, string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                string Query = $"Select ID, FirstName,LastName,FullQualifyName From dbo.users where FullQualifyName='{fullqualify}' ";
                var datas = await dbConnection.QueryAsync<User>(Query);
                var result = datas.FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Guid> GetLoggedInUserId(string fullqualify, string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                string Query = $"SELECT U.ID 'Id' FROM dbo.Users U WHERE U.FullQualifyName ='{fullqualify}' ";
                var datas = await dbConnection.QueryAsync<Guid>(Query);
                var result = datas.FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

    }
}
