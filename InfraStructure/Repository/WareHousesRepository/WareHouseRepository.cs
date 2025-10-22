using AppCore.Entities.User;
using AppCore.Entities.WareHouseEntities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.WareHousesRepository
{
    internal class WareHouseRepository : IWareHouseRepository
    {
        public async Task<List<Commodity>> GetAllCommodity(List<Guid> supplyListId,string query, string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                var datas = await dbConnection.QueryAsync<Commodity>(query);
                var result = datas.ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<SupplyList>> GetAllSupplyList(string query, string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                var datas = await dbConnection.QueryAsync<SupplyList>(query);
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
