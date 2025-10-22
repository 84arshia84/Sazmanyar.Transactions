using AppCore.Entities.SettingEntities.Organizationalunits;
using AppCore.Entities.SettingEntities.Organizationalunits;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class OrganizationalunitRepository : IOrganizationalunitRepository
    {
       
        private readonly AppDbContext _context;
        public OrganizationalunitRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert organizationalunit in database
        /// </summary>
        /// <param name="organizationalunit"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(Organizationalunit organizationalunit)
        {
            try
            {
                if (!OrganizationalunitCrudValidation.CheckDuplicate(_context, organizationalunit))
                {
                    var result = _context.Organizationalunits.AddAsync(organizationalunit);
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
        /// Delete  organizationalunit in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!OrganizationalunitCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingorganizationalunit = await Get(id);
                    if (existingorganizationalunit != null)
                    {
                        existingorganizationalunit.IsDeleted = true;
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
        ///  Get Organizationalunit by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Organizationalunit</returns>
        public async Task<Organizationalunit> Get(Guid id)
        {
            try
            {
                var model = _context.Organizationalunits.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all Organizationalunit from database
        /// </summary>
        /// <returns>List<Organizationalunit></returns>
        /// <exception cref="List<Organizationalunit>"></exception>
        public async Task<List<Organizationalunit>> GetAll()
        {
            try
            {
                var models = await _context.Organizationalunits.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<Organizationalunit>();
            }
        }
        /// <summary>
        /// Update Organizationalunit from database
        /// </summary>
        /// <param name="organizationalunit"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(Organizationalunit organizationalunit)
        {
            try
            {
                var existingorganizationalunit = await Get(organizationalunit.ID);
                if (existingorganizationalunit != null)
                {
                    if (!OrganizationalunitCrudValidation.CheckDuplicate(_context, organizationalunit))
                    {
                        existingorganizationalunit.IsDeleted = false;
                        existingorganizationalunit.Title = organizationalunit.Title;
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
