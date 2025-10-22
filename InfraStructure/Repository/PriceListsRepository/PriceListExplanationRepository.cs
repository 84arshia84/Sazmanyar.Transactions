using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceLists;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PriceListsRepository
{
    internal class PriceListExplanationRepository : IPriceListExplanationRepository
    {
        private readonly AppDbContext _context;
        public PriceListExplanationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// اضافه کردن شرح فصل
        /// </summary>
        /// <param name="priceListExplanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Add(PriceListExplanation priceListExplanation)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceListExplanation))
                {
                    var result = _context.PriceListExplanations.AddAsync(priceListExplanation);
                    await _context.SaveChangesAsync();
                    if (result.IsCompletedSuccessfully)
                    {
                        return ("ثبت شرح برای فصل با موفقیت انجام شد.", true);
                    }
                    return ("ثبت شرح جدید با خطا مواجه شد.", false);
                }
                return ("مقدار تکراری نمی توان ذخیره کرد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// حذف شرح 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                if (!PriceListCrudValidation.CheckIsItUsedExplenation(_context, id))
                {
                    var model = await Get(id);
                    if (model != null)
                    {
                        _context.PriceListExplanations.Remove(model);
                        await _context.SaveChangesAsync();
                        return ("حذف شرح با موفقیت انجام شد.", true);
                    }
                    return ("مورد مد نظر جهت حذف یافت نشد.", false);
                }
                return ("این فهرست بها در یک متر برآورد استفاده شده.", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// گرفتن شرح
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PriceListExplanation> Get(Guid id)
        {
            try
            {
                return await _context.PriceListExplanations.FirstOrDefaultAsync(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمام شرح های یک فصل
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListExplanation>> GetPriceListExplanationByClauseID(Guid id)
        {
            try
            {
                var model = await _context.PriceListExplanations.Where(e => e.PriceListClauseID == id).ToListAsync();
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// بروزرسانی یک شرح در دیتابیس
        /// </summary>
        /// <param name="priceListExplanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(PriceListExplanation priceListExplanation)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceListExplanation))
                {
                    var existingEntity = _context.PriceListExplanations.Find(priceListExplanation.ID);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(priceListExplanation);
                        await _context.SaveChangesAsync();
                        return ("بروزرسانی با موفقیت انجام شد.", true);
                    }
                    return ("مورد مد نظر جهت بروزرسانی یافت نشد.", false);
                }
                return ("مقدار تکراری نمی توان ذخیره کرد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
