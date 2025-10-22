using AppCore.Entities.PwaEntities.Projects;
using AppCore.Entities.User;
using Dapper;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PwaRepository
{
    internal class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;
        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// پروژه های تعریف شده
        /// </summary>
        /// <param name="pwaDb"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<Project>> GetAll(string query,string connectionString)
        {
            try
            {
                //var connectionString = pwaDb.GetType().GetProperty("connectionString").GetValue(pwaDb, null);
                //var PwaDbName = pwaDb.GetType().GetProperty("PwaDbName").GetValue(pwaDb, null);
                //var PWASchemaName = pwaDb.GetType().GetProperty("PWASchemaName").GetValue(pwaDb, null);

                var dbConnection = new SqlConnection(connectionString);
                var datas = await dbConnection.QueryAsync<Project>(query);
                var result = datas.ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
