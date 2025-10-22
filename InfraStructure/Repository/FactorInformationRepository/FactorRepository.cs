using AppCore.Entities.FactorInformation.Factors;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.FactorInformationRepository
{
    internal class FactorRepository : IFactorRepository
    {
        private readonly AppDbContext _context;
        public FactorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Factor factor)
        {
            try
            {
                await _context.Factors.AddAsync(factor);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(Guid factorId, Guid userId)
        {
            try
            {
                var model = await Get(factorId);
                if (model != null)
                {
                    model.IsDeleted = true;
                    model.DeleteDate = DateTime.Now;
                    model.DeleteBy = userId;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteExceptinalFactor(Guid factorId)
        {
            try
            {
                var model = await Get(factorId);
                if (model != null)
                {
                    _context.Factors.Remove(model);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Factor> Get(Guid factorId)
        {
            try
            {
                return await _context.Factors
                    .Include(x => x.FactorTimeProfile)
                    .Include(x => x.FactorFinancialDetaile)
                    .FirstOrDefaultAsync(x => x.Id == factorId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Factor>> GetAll(List<Guid> factorIds)
        {
            try
            {
                return await _context.Factors.Where(x => x.IsDeleted == false && factorIds.Contains(x.Id))
                    .Include(x => x.FactorTimeProfile)
                    .Include(x => x.FactorFinancialDetaile)
                    .OrderByDescending(x => x.InsertFactorDate)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Factor>> GetAllMine(Guid userId)
        {
            try
            {
                return await _context.Factors.Where(x => x.InsertFactorBy == userId && x.IsDeleted == false)
                    .Include(x => x.FactorTimeProfile)
                    .Include(x => x.FactorFinancialDetaile)
                    .OrderByDescending(x => x.InsertFactorDate)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Factor>> GetAllWaitForAction(List<Guid> factorId)
        {
            try
            {
                return await _context.Factors.Where(x => x.IsDeleted == false && factorId.Contains(x.Id))
                    .Include(x => x.FactorTimeProfile)
                    .Include(x => x.FactorFinancialDetaile)
                    .OrderByDescending(x => x.InsertFactorDate)
                    .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Factor>> GetAllWithFinalApprove()
        {
            try
            {
                return await _context.Factors.Where(x => x.IsDeleted == false && x.IsFinalApprove == true)
                     .Include(x => x.FactorTimeProfile)
                     .Include(x => x.FactorFinancialDetaile)
                     .OrderByDescending(x => x.InsertFactorDate)
                     .ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Factor factor)
        {
            try
            {
                var model = await Get(factor.Id);
                if (model != null)
                {
                    _context.Entry(model).CurrentValues.SetValues(factor);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        // FactorRepository.cs


public async Task<List<Factor>> SearchAllAsync(string? term)
{
    var q = _context.Factors.Where(f => f.IsDeleted == false);

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(f =>
            f.FactorTitle.Contains(t) ||       // موجود است
            f.FactorNumber.Contains(t));       // موجود است
    }

    return await q.OrderByDescending(f => f.InsertFactorDate) // موجود است
                  .ThenBy(f => f.Id)                           // موجود است
                  .ToListAsync();
}

public async Task<List<Factor>> SearchMineAsync(Guid userId, string? term)
{
    var q = _context.Factors.Where(f => f.IsDeleted == false && f.InsertFactorBy == userId); // موجود است

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(f => f.FactorTitle.Contains(t) || f.FactorNumber.Contains(t));
    }

    return await q.OrderByDescending(f => f.InsertFactorDate)
                  .ThenBy(f => f.Id)
                  .ToListAsync();
}

public async Task<List<Factor>> SearchWaitForActionAsync(List<Guid> ids, string? term)
{
    var q = _context.Factors.Where(f => f.IsDeleted == false && ids.Contains(f.Id));

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(f => f.FactorTitle.Contains(t) || f.FactorNumber.Contains(t));
    }

    return await q.OrderByDescending(f => f.InsertFactorDate)
                  .ThenBy(f => f.Id)
                  .ToListAsync();
}

public async Task<List<Factor>> SearchWithFinalApproveAsync(string? term)
{
    var q = _context.Factors.Where(f => f.IsDeleted == false && f.IsFinalApprove == true); // موجود است (nullable)

    if (!string.IsNullOrWhiteSpace(term))
    {
        var t = term.Trim();
        q = q.Where(f => f.FactorTitle.Contains(t) || f.FactorNumber.Contains(t));
    }

    return await q.OrderByDescending(f => f.InsertFactorDate)
                  .ThenBy(f => f.Id)
                  .ToListAsync();
}

        public async Task<Factor?> GetByFactorNumber(string? factorNumber)
        {
            var entity = await _context.Factors.FirstOrDefaultAsync(f=>f.FactorNumber == factorNumber);
            if (entity == null)
                return null;
            return entity;
        }
    }
}
