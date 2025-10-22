using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.Stages;
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
    internal class InvoiceBaseInformationRepository : IInvoiceBaseInformationRepository
    {
        private readonly AppDbContext _context;
        public InvoiceBaseInformationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(string message, bool isSuccess, Guid invoiceBaseInformationId)> Add(InvoiceBaseInformation invoiceBaseInformation)
        {
            try
            {
                await _context.InvoiceBaseInformations.AddAsync(invoiceBaseInformation);
                await _context.SaveChangesAsync();
                Guid invoiceBaseInformationId = invoiceBaseInformation.Id;
                return ("اطلاعات پایه صورت وضعیت با موفقیت ثبت شد.", true, invoiceBaseInformationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<InvoiceBaseInformation> Get(Guid invoiceBaseInformationId)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations/*.AsNoTracking().AsSplitQuery()*/.Where(ibi => ibi.Id == invoiceBaseInformationId && ibi.IsDeleted == false).Include(ibi => ibi.Contract).ThenInclude(c => c.ContractFinancialDetails)
                    .Include(c => c.Contract).ThenInclude(c => c.CreditSource)/*.Include(ibi=>ibi.NettingProcessItems)*/.Include(ibi => ibi.InvoiceType).FirstOrDefaultAsync();
                if (model != null)
                {
                    return model;
                }
                return new InvoiceBaseInformation();
            }
            catch (Exception ex)
            {
                throw ex;
                //return new InvoiceBaseInformation();
            }
        }
        public async Task<List<InvoiceBaseInformation>> GetAll()
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false).Include(ibi => ibi.Contract).ThenInclude(c => c.ContratTimeProfile).OrderByDescending(ibi => ibi.InsertDate).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<InvoiceBaseInformation>> GetAllMine(Guid userId)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false && ibi.InsertBy == userId).Include(ibi => ibi.Contract).ThenInclude(c => c.ContratTimeProfile).OrderByDescending(ibi => ibi.InsertDate).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                return new List<InvoiceBaseInformation>();
            }
        }
        public async Task<List<Guid>> GetAllMineIds(Guid userId)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false && ibi.InsertBy == userId).Include(ibi => ibi.Contract)
                    .OrderByDescending(ibi => ibi.InsertDate).Select(ibi => ibi.Id).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                return new List<Guid>();
            }
        }
        public async Task<List<InvoiceBaseInformation>> GetAllWaitingForAction(List<Guid>? waitingForActionIds)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false && waitingForActionIds.Contains(ibi.Id)).Include(ibi => ibi.Contract).ThenInclude(c => c.ContratTimeProfile).OrderByDescending(ibi => ibi.InsertDate).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                return new List<InvoiceBaseInformation>();
            }
        }
        public async Task<List<Guid>> GetAllIdsByUserAccessGroups(Guid userId, string connectionString)
        {
            try
            {
                var dbConnection = new SqlConnection(connectionString);
                var resultm = await dbConnection.QueryAsync<Guid>("TAM.InvoiceAccessGroupEntityIds", commandType: System.Data.CommandType.StoredProcedure);
                var result = resultm.ToList();
                return result;
            }
            catch (Exception ex)
            {
                return new List<Guid>();
            }
        }
        public async Task<List<InvoiceBaseInformation>> GetInvoiceBaseInformationByIds(List<Guid>? Ids)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false && Ids.Contains(ibi.Id)).Include(ibi => ibi.Contract).ThenInclude(c => c.ContratTimeProfile).OrderByDescending(ibi => ibi.InsertDate).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                return new List<InvoiceBaseInformation>();
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid invoiceBaseInformationId)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.Id == invoiceBaseInformationId && ibi.IsDeleted == false).FirstOrDefaultAsync();
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeleteDate = DateTime.Now;
                    model.DeleteBy = Guid.Empty;
                    await _context.SaveChangesAsync();
                    return ("حذف اطلاعات پایه صورت وضعیت با موفقیت انجام شد.", true);
                }
                return ("مورد مد نظر جهت حذف یافت نشد.", false);
            }
            catch (Exception ex)
            {
                return ("حذف با خطا روبرو شد.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> Update(InvoiceBaseInformation invoiceBaseInformation)
        {
            try
            {
                var model = await Get(invoiceBaseInformation.Id);
                if (model != null)
                {
                    model.InvoiceTitle = invoiceBaseInformation.InvoiceTitle;
                    model.LeadingToDate = invoiceBaseInformation.LeadingToDate;
                    model.SendDate = invoiceBaseInformation.SendDate;
                    model.Description = invoiceBaseInformation.Description;
                    model.InvoiceCode = invoiceBaseInformation.InvoiceCode;
                    model.InvoiceNumber = invoiceBaseInformation.InvoiceNumber;
                    model.AccountId = invoiceBaseInformation.AccountId;
                    //model.InsertDate = invoiceBaseInformation.InsertDate;
                    //model.InsertBy = invoiceBaseInformation.InsertBy;
                    //model.DeleteBy = invoiceBaseInformation.DeleteBy;
                    //model.DeleteDate = invoiceBaseInformation.DeleteDate;
                    //model.IsDeleted = invoiceBaseInformation.IsDeleted;
                    model.ContractId = invoiceBaseInformation.ContractId;
                    model.InvoiceTypeId = invoiceBaseInformation.InvoiceTypeId;
                    await _context.SaveChangesAsync();
                    return ("ویرایش با موفقیت انجام شد", true);
                }
                return ("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {

                return ("ویرایش با خطا روبرو شد.", false);
            }
        }

        public async Task<List<InvoiceBaseInformation>> GetAllByContractId(Guid contractId)
        {
            try
            {
                var model = await _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false && ibi.ContractId == contractId).Include(ibi => ibi.Contract).ThenInclude(c => c.ContratTimeProfile).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {
                return new List<InvoiceBaseInformation>();
            }
        }
        /// <summary>
        /// تمامی صورت وضعیت هایی که قبل از تاریخ صورت وضعیت داده شده 
        /// به ما می دهد
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="beforThisInvoice"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<InvoiceBaseInformation>> GetAllByContractIdBefor(Guid contractId, DateTime beforThisInvoice)
        {
            try
            {
                return await _context.InvoiceBaseInformations.Where(x => x.ContractId == contractId && x.IsDeleted == false && x.InsertDate < beforThisInvoice)
                    .Include(x => x.ServiceExplanationFinancials)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


public async Task<List<InvoiceBaseInformation>> SearchAllAsync(string? term)
{
    var q = _context.InvoiceBaseInformations.Where(ibi => ibi.IsDeleted == false);

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(ibi =>
            ibi.InvoiceTitle.Contains(t) ||                         // موجود است
            ibi.InvoiceCode.Contains(t)  ||                         // موجود است
            (ibi.InvoiceNumber.HasValue &&                          // موجود است
             EF.Functions.Like(ibi.InvoiceNumber.ToString(), $"%{t}%")));
    }

    return await q.OrderByDescending(ibi => ibi.InsertDate)         // موجود است
                  .ThenBy(ibi => ibi.Id)                            // موجود است
                  .ToListAsync();
}

public async Task<List<InvoiceBaseInformation>> SearchMineAsync(Guid userId, string? term)
{
    var q = _context.InvoiceBaseInformations
        .Where(ibi => ibi.IsDeleted == false && ibi.InsertBy == userId); // موجود است

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(ibi =>
            ibi.InvoiceTitle.Contains(t) ||
            ibi.InvoiceCode.Contains(t)  ||
            (ibi.InvoiceNumber.HasValue && EF.Functions.Like(ibi.InvoiceNumber.ToString(), $"%{t}%")));
    }

    return await q.OrderByDescending(ibi => ibi.InsertDate)
                  .ThenBy(ibi => ibi.Id)
                  .ToListAsync();
}

public async Task<List<InvoiceBaseInformation>> SearchWaitForActionAsync(IEnumerable<Guid> ids, string? term)
{
    var q = _context.InvoiceBaseInformations
        .Where(ibi => ibi.IsDeleted == false && ids.Contains(ibi.Id));

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(ibi =>
            ibi.InvoiceTitle.Contains(t) ||
            ibi.InvoiceCode.Contains(t)  ||
            (ibi.InvoiceNumber.HasValue && EF.Functions.Like(ibi.InvoiceNumber.ToString(), $"%{t}%")));
    }

    return await q.OrderByDescending(ibi => ibi.InsertDate)
                  .ThenBy(ibi => ibi.Id)
                  .ToListAsync();
}

public async Task<List<InvoiceBaseInformation>> SearchByContractAsync(Guid contractId, string? term)
{
    var q = _context.InvoiceBaseInformations
        .Where(ibi => ibi.IsDeleted == false && ibi.ContractId == contractId); // موجود است

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(ibi =>
            ibi.InvoiceTitle.Contains(t) ||
            ibi.InvoiceCode.Contains(t)  ||
            (ibi.InvoiceNumber.HasValue && EF.Functions.Like(ibi.InvoiceNumber.ToString(), $"%{t}%")));
    }

    return await q.OrderByDescending(ibi => ibi.InsertDate)
                  .ThenBy(ibi => ibi.Id)
                  .ToListAsync();
}

        public Task<List<InvoiceBaseInformation>> SearchWaitForActionAsync(List<Guid> waitingForActionIds, string? term)
        {
            throw new NotImplementedException();
        }

        public async Task<InvoiceBaseInformation?> GetByInvoiceNumber(string invoiceCode)
        {
            var entity = await _context.InvoiceBaseInformations.FirstOrDefaultAsync(i => i.InvoiceCode == invoiceCode);
            if (entity == null)
                return null;
            return entity;
        }


    }
}
