using AppCore.Entities.PwaEntities.Proposals;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PwaRepository
{
    internal class ProposalRepository : IProposalRepository
    {
        public async Task<List<DeliverablePen>> GetAllDeliverablePens(string query, string connectionstring)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionstring);
                var datas = await dbConnection.QueryAsync<DeliverablePen>(query);
                var result = datas.ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Proposal>> GetAllProposals(string query, string connectionstring)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionstring);
                var datas = await dbConnection.QueryAsync<Proposal>(query);
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
