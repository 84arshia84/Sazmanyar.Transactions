using AppCore.Entities.PriceListEntities.PriceListFields;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PriceListsRepository
{
    internal class PriceListFieldRepository : IPriceListFieldRepository
    {
        private readonly AppDbContext _context;
        public PriceListFieldRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// اضافه کردن رشته به دیتابیس
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(Guid ID, string message, bool isSuccess)> Add(PriceListField priceList)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceList))
                {
                    var result = _context.PriceListFields.AddAsync(priceList);
                    await _context.SaveChangesAsync();
                    if (result.IsCompletedSuccessfully)
                    {
                        return (priceList.ID, "ثبت رشته با موفقیت انجام شد.", true);
                    }
                    return (Guid.Empty, "ثبت رشته با موفقیت انجام نشد.", false);
                }
                return (Guid.Empty, "مقدار تکراری نمی توان ذخیره کرد.", false);
            }
            catch (Exception ex)
            {

                //return (Guid.Empty, "خطا در ثبت رشته.", false);
                throw ex;
            }
        }
        /// <summary>
        /// حذف رشته از دیتابیس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                if(!PriceListCrudValidation.CheckIsItUsedField(_context, id))
                {
                    var model = await Get(id);
                    if (model == null)
                    {
                        return ("مورد مد نظر جهت حذف پیدا نشد.", false);
                    }
                    _context.PriceListFields.Remove(model);
                    await _context.SaveChangesAsync();
                    return ("حذف رشته با موفقیت انجام شد.", true);
                }
                return ("این فهرست بها در یک متر برآورد استفاده شده.", false);

            }
            catch (Exception ex)
            {
                // return ("خطا در حذف رشته", false);
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن رشته از دیتابیس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PriceListField> Get(Guid id)
        {
            try
            {
                var model =_context.PriceListFields.FirstOrDefault(x => x.ID == id);
                return model;
            }
            catch (Exception ex)
            {
                // return new PriceListField();
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی رشته ها از دیتابیس
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListField>> GetAll()
        {
            try
            {
                var models =await  _context.PriceListFields.ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                throw ex;
               // return new List<PriceListField>();
            }
        }
        /// <summary>
        /// رشته های متعلق به یک سال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListField>> GetAllByYearId(Guid id)
        {
            try
            {
                return await _context.PriceListFields.Where(x=>x.PriceListID==id).OrderBy(x=>x.FieldTitle).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// بروزرسانی رشته در دیتابیس
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(PriceListField priceList)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceList))
                {
                    var existingEntity = await _context.PriceListFields.FirstOrDefaultAsync(x=>x.ID==priceList.ID);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(priceList);
                        _context.SaveChangesAsync();
                        return ("بروزرسانی با موفقیت انجام شد.", true);
                    }
                    return ("مورد مد نظر جهت بروزرسانی یافت نشد.", false);
                }
                return ("مقدار تکراری نمی توان ذخیره کرد.", false);

            }
            catch (Exception ex)
            {
                throw ex;
                //return ("خطا در بروزرسانی رشته", false);
                
            }
        }
    }
}
