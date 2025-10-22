using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AppCore.Entities.SettingEntities.Stages;
using InfrStructure.DataBase;

namespace InfraStructure.Repository.SettingRepository
{
    public class StageRepository : IStageRepository
    {
        //private readonly IDbConnection dbConnection;
        private readonly AppDbContext _context;
        public StageRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stage>> GetAll(Guid ModuleId, string connectionString)
        {
            var dbConnection = new SqlConnection(connectionString);
            string Query = @"Select ID, Title From WF.Stages Where ModuleID = '" + ModuleId.ToString() + "'";
            var resultm = await dbConnection.QueryAsync<Stage>(Query);
            var result = resultm.ToList();
            return result;
        }
    }
}
