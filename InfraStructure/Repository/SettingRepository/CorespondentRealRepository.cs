using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class CorespondentRealRepository : ICorespondentRealRepository
    {
        private readonly AppDbContext _context;
        public CorespondentRealRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert corespondentrreal in database
        /// </summary>
        /// <param name = "corespondentreal" ></ param >
        /// < returns > Tuple<string, bool> </ returns >
        /// < exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(CorespondentReal corespondentrReal)
        {
            try
            {
                if (!CorespondentRealCrudValidation.CheckDuplicate(_context, corespondentrReal))
                {
                    var result = _context.CorespondentReals.AddAsync(corespondentrReal);
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
                throw ex;
            }
        }
        /// <summary>
        /// Delete  corespondentrreal in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!CorespondentRealCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingcorespondentrReal = await Get(id);
                    if (existingcorespondentrReal != null)
                    {
                        existingcorespondentrReal.IsDeleted = true;
                        await _context.SaveChangesAsync();
                        return Tuple.Create("حذف با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("مورد مد نظر جهت حذف وجود ندارد.", false);
                }
                return Tuple.Create("این مورد قبلا در قراردادی استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///  Get CorespondentReal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CorespondentReal</returns>
        public async Task<CorespondentReal> Get(Guid id)
        {
            try
            {
                var model = _context.CorespondentReals.Where(x => x.ID == id && x.IsDeleted == false).Include(x => x.CorespondAndTypeOfCoopRels).FirstOrDefault();
                if (model == null)
                {
                    return null;
                }
                return model;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Get all CorespondentReal from database
        /// </summary>
        /// <returns>List<CorespondentReal></returns>
        /// <exception cref="List<CorespondentReal>"></exception>
        public async Task<List<CorespondentReal>> GetAll()
        {
            try
            {
                var models = await _context.CorespondentReals.Where(x => x.IsDeleted == false).Include(x => x.CorespondAndTypeOfCoopRels).OrderBy(x => x.Address).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<CorespondentReal>();
            }
        }
        /// <summary>
        /// Update CorespondentReal from database
        /// </summary>
        /// <param name="corespondentrReal"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(CorespondentReal corespondentrReal)
        {
            try
            {
                var existingcorespondentrReal = await Get(corespondentrReal.ID);
                if (existingcorespondentrReal != null)
                {
                    if (!CorespondentRealCrudValidation.CheckDuplicate(_context, corespondentrReal))
                    {
                        existingcorespondentrReal.IsDeleted = false;
                        existingcorespondentrReal.Address = corespondentrReal.Address;
                        existingcorespondentrReal.ShabaNumber = corespondentrReal.ShabaNumber;
                        existingcorespondentrReal.BankAcountNumber = corespondentrReal.BankAcountNumber;
                        existingcorespondentrReal.BankName = corespondentrReal.BankName;
                        existingcorespondentrReal.BranchCodeAndName = corespondentrReal.BranchCodeAndName;
                        existingcorespondentrReal.IsReal = true;
                        existingcorespondentrReal.Name = corespondentrReal.Name;
                        existingcorespondentrReal.Family = corespondentrReal.Family;
                        existingcorespondentrReal.NationalCode = corespondentrReal.NationalCode;
                        existingcorespondentrReal.CityId = corespondentrReal.CityId;
                        existingcorespondentrReal.ProvincId = corespondentrReal.ProvincId;
                        existingcorespondentrReal.CountyId = corespondentrReal.CountyId;
                        existingcorespondentrReal.PhoneNumber = corespondentrReal.PhoneNumber;
                        existingcorespondentrReal.Email = corespondentrReal.Email;
                        existingcorespondentrReal.EconomicCode = corespondentrReal.EconomicCode;
                        existingcorespondentrReal.PostalCode = corespondentrReal.PostalCode;
                        existingcorespondentrReal.FatherName = corespondentrReal.FatherName;
                        existingcorespondentrReal.CertificateNumber = corespondentrReal.CertificateNumber;
                        _context.Entry(existingcorespondentrReal).Collection(c => c.CorespondAndTypeOfCoopRels).Load();
                        existingcorespondentrReal.CorespondAndTypeOfCoopRels.Clear();
                        if (corespondentrReal.CorespondAndTypeOfCoopRels != null)
                        {
                            foreach (var relation in corespondentrReal.CorespondAndTypeOfCoopRels)
                            {
                                _context.CorespondRealAndTypeOfCoopRels.Add(relation);
                            }
                        }
                        await _context.SaveChangesAsync();
                        return Tuple.Create("ویرایش با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("این عنوان وجود دارد.", false);
                }
                return Tuple.Create("مورد مد نظر جهت ویرایش وجود ندارد.", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
