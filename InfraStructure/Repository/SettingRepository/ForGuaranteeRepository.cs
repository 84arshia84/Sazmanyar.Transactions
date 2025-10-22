using AppCore.Entities.SettingEntities.ForGuarantees;
using AppCore.Entities.SettingEntities.ForGuarantees;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class ForGuaranteeRepository : IForGuaranteeRepository
    {

        private readonly AppDbContext _context;
        public ForGuaranteeRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert forguarantee in database
        /// </summary>
        /// <param name="forguarantee"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(ForGuarantee forguarantee)
        {
            try
            {
                if (!ForGuaranteeCrudValidation.CheckDuplicate(_context, forguarantee))
                {
                    var result = _context.ForGuarantees.AddAsync(forguarantee);
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
        /// Delete  forguarantee in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!ForGuaranteeCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingforguarantee = await Get(id);
                    if (existingforguarantee != null)
                    {
                        existingforguarantee.IsDeleted = true;
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
        ///  Get ForGuarantee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ForGuarantee</returns>
        public async Task<ForGuarantee> Get(Guid id)
        {
            try
            {
                var model = _context.ForGuarantees.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all ForGuarantee from database
        /// </summary>
        /// <returns>List<ForGuarantee></returns>
        /// <exception cref="List<ForGuarantee>"></exception>
        public async Task<List<ForGuarantee>> GetAll()
        {
            try
            {
                var models = await _context.ForGuarantees.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<ForGuarantee>();
            }
        }
        /// <summary>
        /// Update ForGuarantee from database
        /// </summary>
        /// <param name="forguarantee"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(ForGuarantee forguarantee)
        {
            try
            {
                var existingforguarantee = await Get(forguarantee.ID);
                if (existingforguarantee != null)
                {
                    if (!ForGuaranteeCrudValidation.CheckDuplicate(_context, forguarantee))
                    {
                        existingforguarantee.IsDeleted = false;
                        existingforguarantee.Title = forguarantee.Title;
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
