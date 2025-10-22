using AppCore.Entities.SettingEntities.FinePaymentMethods;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class FinePaymentMethodRepository : IFinePaymentMethodRepository
    {
        private readonly AppDbContext _context;
        public FinePaymentMethodRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert finepaymentmethod in database
        /// </summary>
        /// <param name="finepaymentmethod"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(FinePaymentMethod finepaymentmethod)
        {
            try
            {
                if (!FinePaymentMethodCrudValidation.CheckDuplicate(_context, finepaymentmethod))
                {
                    var result = _context.FinePaymentMethods.AddAsync(finepaymentmethod);
                    await _context.SaveChangesAsync();
                    if (result.IsCompleted)
                    {
                        return Tuple.Create("ثبت با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("ثبت با خطا روبرو شد.", false);
                }
                return Tuple.Create("اجازه ثبت مجدد ندارید.", false);
            }
            catch (Exception ex)
            {
                return Tuple.Create("ثبت با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        /// Delete  finepaymentmethod in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!FinePaymentMethodCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingfinepaymentmethod = await Get(id);
                    if (existingfinepaymentmethod != null)
                    {
                        existingfinepaymentmethod.IsDeleted = true;
                       await _context.SaveChangesAsync();
                        return Tuple.Create("حذف با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("مورد مد نظر جهت حذف وجود ندارد.", false);
                }
                return Tuple.Create("این مورد قبلا در قراردادی استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                return Tuple.Create("حذف با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        ///  Get FinePaymentMethod by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FinePaymentMethod</returns>
        public async Task<FinePaymentMethod> Get(Guid id)
        {
            try
            {
                var model = _context.FinePaymentMethods.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
                if (model == null)
                {
                    return null;
                }
                return model;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// Get all FinePaymentMethod from database
        /// </summary>
        /// <returns>List<FinePaymentMethod></returns>
        /// <exception cref="List<FinePaymentMethod>"></exception>
        public async Task<List<FinePaymentMethod>> GetAll()
        {
            try
            {
                var models = await _context.FinePaymentMethods.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<FinePaymentMethod>();
            }
        }
        /// <summary>
        /// Update FinePaymentMethod from database
        /// </summary>
        /// <param name="finepaymentmethod"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(FinePaymentMethod finepaymentmethod)
        {
            try
            {
                var existingfinepaymentmethod = await Get(finepaymentmethod.ID);
                if (existingfinepaymentmethod != null)
                {
                    if (!FinePaymentMethodCrudValidation.CheckDuplicate(_context, finepaymentmethod))
                    {
                        existingfinepaymentmethod.IsDeleted = false;
                        existingfinepaymentmethod.Title = finepaymentmethod.Title;
                        await _context.SaveChangesAsync();
                        return Tuple.Create("ویرایش با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("این عنوان وجود دارد.", false);
                }
                return Tuple.Create("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception)
            {
                return Tuple.Create("ویرایش با خطا روبرو شد.", false);
            }
        }
    }
}
