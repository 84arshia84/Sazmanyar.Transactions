using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PriceListsRepository
{
    internal class PriceListClauseRepository : IPriceListClauseRepository
    {
        private readonly AppDbContext _context;
        public PriceListClauseRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceListExplanation"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(Guid ID, string message, bool isSuccess)> Add(PriceListClause priceListExplanation)
        {
            try
            {
                if(!PriceListCrudValidation.CheckDuplicate(_context, priceListExplanation))
                {
                    var result = _context.PriceListClauses.AddAsync(priceListExplanation);
                    await _context.SaveChangesAsync();
                    if (result.IsCompletedSuccessfully)
                    {
                        return (priceListExplanation.ID, "افزودن فصل با موفقیت انجام شد.", true);
                    }
                }
               
                return (Guid.Empty,"مقدار تکراری نمی توان ذخیره کرد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                if (!PriceListCrudValidation.CheckIsItUsedExplenation(_context,id))
                {
                    var model = await Get(id);
                    if (model != null)
                    {
                        _context.PriceListClauses.Remove(model);
                        _context.SaveChangesAsync();
                        return ("حذف با موفقیت انجام شد.", true);
                    }
                    return ("مورد مد نظر جهت حذف وجود ندارد", false);
                }

               return ("این فهرست بها در یک متر برآورد استفاده شده.", false);
            }
            catch (Exception ex)
            {
                throw ex;
                //return ("حذف با خطا مواجه شد.", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PriceListClause> Get(Guid id)
        {
            try
            {
                return await _context.PriceListClauses.FirstOrDefaultAsync(x => x.ID == id);  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListClause>> GetAll()
        {
            try
            {
                return await _context.PriceListClauses.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// فصل ها متعلق به یک رشته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListClause>> GetAllByFieldId(Guid id)
        {
            try
            {
                return await _context.PriceListClauses.Where(x=>x.PriceListFieldID== id).OrderBy(x=>x.ClauseTitle).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="priceListcluse"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(PriceListClause priceListcluse)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceListcluse))
                {
                    var existingEntity = _context.PriceListClauses.Find(priceListcluse.ID);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(priceListcluse);
                        _context.SaveChangesAsync();
                        return ("ویرایش با موفقیت انجام شد.", true);
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
