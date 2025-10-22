using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.CorespondAndTypeOfCoopRel;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class CorespondentLegalRepository : ICorespondentLegalRepository
    {
        private readonly AppDbContext _context;
        public CorespondentLegalRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert corespondentlegal in database
        /// </summary>
        /// <param name="corespondentlegal"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(CorespondentLegal corespondentlegal)
        {
            try
            {
                if (!CorespondentLegalCrudValidation.CheckDuplicate(_context, corespondentlegal))
                {
                   
                    var result = _context.CorespondentLegals.AddAsync(corespondentlegal);
                    await  _context.SaveChangesAsync();
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
        /// Delete  corespondentlegal in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!CorespondentLegalCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingcorespondentlegal = await Get(id);
                    if (existingcorespondentlegal != null)
                    {
                        existingcorespondentlegal.IsDeleted = true;
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
        ///  Get CorespondentLegal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CorespondentLegal</returns>
        public async Task<CorespondentLegal> Get(Guid id)
        {
            try
            {
                var model = _context.CorespondentLegals.Where(x => x.ID == id && x.IsDeleted == false).Include(x => x.CorespondAndTypeOfCoopRels).FirstOrDefault();
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
        /// Get all CorespondentLegal from database
        /// </summary>
        /// <returns>List<CorespondentLegal></returns>
        /// <exception cref="List<CorespondentLegal>"></exception>
        public async Task<List<CorespondentLegal>> GetAll()
        {
            try
            {
                var models = await _context.CorespondentLegals.Include(x => x.CorespondAndTypeOfCoopRels).Where(x => x.IsDeleted == false).OrderBy(x => x.CompanyName).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<CorespondentLegal>();
            }
        }
        /// <summary>
        /// Update CorespondentLegal from database
        /// </summary>
        /// <param name="corespondentlegal"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(CorespondentLegal corespondentlegal)
        {
            try
            {
                var existingcorespondentlegal = await Get(corespondentlegal.ID);
                if (existingcorespondentlegal != null)
                {
                    if (!CorespondentLegalCrudValidation.CheckDuplicate(_context, corespondentlegal))
                    {
                        existingcorespondentlegal.IsDeleted = false;
                        existingcorespondentlegal.Address = corespondentlegal.Address;
                        existingcorespondentlegal.RegistrationNumber = corespondentlegal.RegistrationNumber;
                        existingcorespondentlegal.ShabaNumber = corespondentlegal.ShabaNumber;
                        existingcorespondentlegal.BankAcountNumber = corespondentlegal.BankAcountNumber;
                        existingcorespondentlegal.BankName = corespondentlegal.BankName;
                        existingcorespondentlegal.BranchCodeAndName = corespondentlegal.BranchCodeAndName;
                        existingcorespondentlegal.IsReal = false;
                        existingcorespondentlegal.CompanyName = corespondentlegal.CompanyName;
                        existingcorespondentlegal.NationalID = corespondentlegal.NationalID;
                        existingcorespondentlegal.CityId = corespondentlegal.CityId;
                        existingcorespondentlegal.ProvincId = corespondentlegal.ProvincId;
                        existingcorespondentlegal.CountyId = corespondentlegal.CountyId;
                        existingcorespondentlegal.PhoneNumber = corespondentlegal.PhoneNumber;
                        existingcorespondentlegal.Email = corespondentlegal.Email;
                        existingcorespondentlegal.EconomicCode = corespondentlegal.EconomicCode;
                        existingcorespondentlegal.PostalCode = corespondentlegal.PostalCode;
                        _context.Entry(existingcorespondentlegal).Collection(c => c.CorespondAndTypeOfCoopRels).Load();
                        existingcorespondentlegal.CorespondAndTypeOfCoopRels.Clear();

                        if (corespondentlegal.CorespondAndTypeOfCoopRels != null)
                        {
                            foreach (var relation in corespondentlegal.CorespondAndTypeOfCoopRels)
                            {
                                _context.CoresponedAndTypeCoopRels.Add(relation);
                            }
                        }
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
