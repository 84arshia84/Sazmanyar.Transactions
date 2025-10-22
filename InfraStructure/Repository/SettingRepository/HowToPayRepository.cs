using AppCore.Entities.SettingEntities.HowToPays;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class HowToPayRepository : IHowToPayRepository
    {
    
        private readonly AppDbContext _context;
        public HowToPayRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert howtopay in database
        /// </summary>
        /// <param name="howtopay"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(HowToPay howtopay)
        {
            try
            {
                if (!HowToPayCrudValidation.CheckDuplicate(_context, howtopay))
                {
                    var result = _context.HowToPay.AddAsync(howtopay);
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
        /// Delete  howtopay in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!HowToPayCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existinghowToPay = await Get(id);
                    if (existinghowToPay != null)
                    {
                        existinghowToPay.IsDeleted = true;
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
        ///  Get HowToPay by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HowToPay</returns>
        public async Task<HowToPay> Get(Guid id)
        {
            try
            {
                var model = _context.HowToPay.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all HowToPay from database
        /// </summary>
        /// <returns>List<HowToPay></returns>
        /// <exception cref="List<HowToPay>"></exception>
        public async Task<List<HowToPay>> GetAll()
        {
            try
            {
                var models = await _context.HowToPay.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<HowToPay>();
            }
        }
        /// <summary>
        /// Update HowToPay from database
        /// </summary>
        /// <param name="howtopay"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(HowToPay howtopay)
        {
            try
            {
                var existinghowToPay = await Get(howtopay.ID);
                if (existinghowToPay != null)
                {
                    if (!HowToPayCrudValidation.CheckDuplicate(_context, howtopay))
                    {
                        existinghowToPay.IsDeleted = false;
                        existinghowToPay.Title = howtopay.Title;
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
