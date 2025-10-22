using AppCore.Entities.SettingEntities.CreditSources;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class CreditSourceRepository : ICreditSourceRepository
    {
        private readonly AppDbContext _context;
        public CreditSourceRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert creditsource in database
        /// </summary>
        /// <param name="creditsource"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(CreditSource creditsource)
        {
            try
            {
                if (!CreditSourceCrudValidation.CheckDuplicate(_context, creditsource))
                {
                    var result = _context.CreditSources.AddAsync(creditsource);
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
        /// Delete  creditsource in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!CreditSourceCrudValidation.CheckIsItUsed(_context, id))
                {
                    //  var existingCreditSources = await Get(id);
                    var MeAndMyChildren = _context.CreditSources.Where(x => x.ID == id || x.ParentID == id).ToList();

                    if (MeAndMyChildren != null)
                    {
                        foreach (var m in MeAndMyChildren)
                        {
                            m.IsDeleted = true;
                        }
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
        ///  Get CreditSource by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CreditSource</returns>
        public async Task<CreditSource> Get(Guid id)
        {
            try
            {
                var model = _context.CreditSources.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all CreditSource from database
        /// </summary>
        /// <returns>List<CreditSource></returns>
        /// <exception cref="List<CreditSource>"></exception>
        public async Task<List<CreditSource>> GetAll()
        {
            try
            {
                var models = await _context.CreditSources.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<CreditSource>();
            }
        }
        /// <summary>
        /// Update CreditSource from database
        /// </summary>
        /// <param name="creditsource"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(CreditSource creditsource)
        {
            try
            {
                var existingCreditSources = await Get(creditsource.ID);
                if (existingCreditSources != null)
                {
                    if (!CreditSourceCrudValidation.CheckDuplicate(_context, creditsource))
                    {
                        existingCreditSources.IsDeleted = false;
                        existingCreditSources.Title = creditsource.Title;
                        existingCreditSources.CreditSourceCode = creditsource.CreditSourceCode;
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
