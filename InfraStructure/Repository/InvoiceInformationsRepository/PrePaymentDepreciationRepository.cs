using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.User;
using Dapper;
using InfrStructure.DataBase;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.InvoiceInformationsRepository
{
    internal class PrePaymentDepreciationRepository : IPrePaymentDepreciationRepository
    {
        private readonly AppDbContext _context;
        public PrePaymentDepreciationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(PrePaymentDepreciation paymentDepreciation)
        {
            try
            {
                await _context.PrePaymentDepreciations.AddAsync(paymentDepreciation);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PrePaymentDepreciation> Get(Guid id)
        {
            try
            {
                return await _context.PrePaymentDepreciations.FirstOrDefaultAsync(x=>x.Id == id && x.IsDeleted == false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PrePaymentDepreciation>> GetAllByFinancialId(Guid financialId)
        {
            try
            {
                return await _context.PrePaymentDepreciations.Where(x=>x.ServiceExplenationFinancialId == financialId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PrePaymentDepreciation>> GetAllByInvoiceId(Guid invoiceId)
        {
            try
            {
                return await _context.PrePaymentDepreciations.Where(x=>x.InvoiceId == invoiceId && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<PrePaymentDepreciation>> GetAllByServiceExplenationId(Guid explenationId)
        {
            try
            {
                return await _context.PrePaymentDepreciations.Where(x => x.ServiceExplenationId == explenationId && x.IsDeleted==false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// تمامی استهلاکات متعلق به یک قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PrePaymentDepreciation>> GetAllForContract(Guid contractId)
        {
            try
            {
                var invoices = await _context.InvoiceBaseInformations.Where(x => x.ContractId == contractId).ToListAsync();
                return await _context.PrePaymentDepreciations.Where(x => invoices.Select(y => y.Id).Contains(x.InvoiceId) && x.IsDeleted == false).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<PrePaymentDepreciation> GetByFinancialId(Guid id)
        {
            try
            {
                return await _context.PrePaymentDepreciations.FirstOrDefaultAsync(x => x.ServiceExplenationFinancialId == id && x.IsDeleted==false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(PrePaymentDepreciation prePaymentDepreciation)
        {
            try
            {
                var model = await Get(prePaymentDepreciation.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(prePaymentDepreciation);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public async Task<List<Guid>> guids(string connectionString)
        {
            var dbConnection = new SqlConnection(connectionString);
            //string Query = @"select sef.Id from tam.ServiceExplanationFinancials sef
            //                 inner join tam.InvoiceBaseInformations  ibi on ibi.Id=sef.InvoiceBaseInformationId
            //                 where ibi.IsDeleted=0 and ibi.InvoiceTypeId in ('C4BE10DB-C477-4D86-B38A-5709D4A64828','DBB3CF0D-5B6E-406B-A033-B6448EB9FBE4') and sef.Id in(
            //                 select sef.Id from tam.ServiceExplanationFinancials as sef 
            //                 left join tam.PrePaymentDepreciations as ppd on ppd.ServiceExplenationFinancialId= sef.Id
            //                 where ServiceExplenationFinancialId is null
            //                 )
            //                 order by ibi.InsertDate";
            string Query = @"select sef.Id from tam.ServiceExplanationFinancials sef
                             inner join tam.InvoiceBaseInformations  ibi on ibi.Id=sef.InvoiceBaseInformationId
                             where ibi.IsDeleted=0 and ibi.InvoiceTypeId in ('C4BE10DB-C477-4D86-B38A-5709D4A64828','DBB3CF0D-5B6E-406B-A033-B6448EB9FBE4') and sef.Id in(
                             select sef.Id from tam.ServiceExplanationFinancials as sef 
                             left join tam.OnAccountDepreciation as ppd on ppd.ServiceExplenationFinancialId= sef.Id
                             where ServiceExplenationFinancialId is null
                             )
                             order by ibi.InsertDate";
            var datas = await dbConnection.QueryAsync<Guid>(Query);
            var result = datas.ToList();
            return result;
        }
    }
}
