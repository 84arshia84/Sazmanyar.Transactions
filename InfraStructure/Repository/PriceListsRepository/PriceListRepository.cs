using AppCore.Entities.PriceListEntities.PriceLists;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PriceListsRepository
{
    internal class PriceListRepository : IPriceListRepository
    {
        private readonly AppDbContext _context;
        public PriceListRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// اضافه کردن فهرست بها به دیتابیس به وسیله فایل اکسل
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string, bool)> ImportExcel(List<PriceList> priceList)
        {
            try
            {
                var SieveData = PriceListCrudValidation.CheckExists(priceList, _context);
                if (SieveData != null)
                {
                    await _context.PriceLists.AddRangeAsync(SieveData);
                }
                return ("فهرست بها با موفقیت ثبت شد.", true);

            }
            catch (Exception ex)
            {
                // return ("خطا در ذخیره سازی در دیتابیس.", false);
                throw ex;
            }
        }
        /// <summary>
        /// گرفتن تمامی فهرست بها ها
        /// </summary>
        /// <returns></returns>
        public async Task<(List<PriceList> models,string result)> GetAllPriceList()
        {
            try
            {

                var model= await _context.PriceLists.Include(p=>p.PriceListFields).ThenInclude(f=>f.PriceListClauses).ToListAsync();
                return (model, "");
            }
            catch (Exception ex)
            {
                //return (new List<PriceList>() , "خطا در دریافت فهرست بها");
                throw ex;
            }
        }
        /// <summary>
        /// بروزرسانی سال
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(PriceList priceList)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context, priceList))
                {
                    var existingEntity = _context.PriceLists.Find(priceList.ID);
                    if (existingEntity != null)
                    {
                        _context.Entry(existingEntity).CurrentValues.SetValues(priceList);
                       await  _context.SaveChangesAsync();
                        return ("ویرایش سال با موفقیت انجام شد.", true);
                    }
                }
                return ("مقدار تکراری نمی توان ذخیره کرد.", false);

            }
            catch (Exception ex)
            {
                throw ex;
                // return ("خطا در ویرایش سال", false);
            }
        }
        /// <summary>
        /// افزودن سال
        /// </summary>
        /// <param name="priceList"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(Guid ID,string message, bool isSuccess)> Add(PriceList priceList)
        {
            try
            {
                if (!PriceListCrudValidation.CheckDuplicate(_context,priceList))
                {
                    var result = _context.PriceLists.AddAsync(priceList);
                    if (result.IsCompleted)
                    {
                        await _context.SaveChangesAsync();
                        return (priceList.ID, "ثبت سال با موفقیت انجام شد", true);
                    }
                }
                return (Guid.Empty, "مقدار تکراری نمی توان ذخیره کرد", false);
            }
            catch (Exception ex)
            {
                throw ex;
                // return (Guid.Empty, "خطا در ذخیره سازی در دیتابیس", false);
            }
        }
        /// <summary>
        /// حذف سال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                if (!PriceListCrudValidation.CheckIsItUsed(_context,id))
                {
                    var model = _context.PriceLists.FirstOrDefault(p => p.ID == id);
                    if (model == null)
                    {
                        return ("مورد جهت حذف پیدا نشد", false);
                    }
                    _context.PriceLists.Remove(model);
                    _context.SaveChanges(true);
                    return ("حذف با موفقیت انجام شد.", true);
                }
             return ("این فهرست بها در یک متر برآورد استفاده شده.", false);
            }
            catch (Exception ex)
            {
                throw ex;
               // return ("حذف با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PriceList> Get(Guid id)
        {
            try
            {
                return  await _context.PriceLists.FirstOrDefaultAsync(x => x.ID == id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        ///  گرفتن تمامی سال ها
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceList>> GetAllYears()
        {
            try
            {
                return await _context.PriceLists.OrderBy(x=>x.Year).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
