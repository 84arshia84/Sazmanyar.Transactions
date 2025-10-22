using AppCore.Entities.SettingEntities.TypeOfCooperations;
using AppCore.Entities.SettingEntities.TypeOfCooperations;
using InfrStructure.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.SettingRepository
{
    public class TypeOfCooperationRepository : ITypeOfCooperationRepository
    {
     
        private readonly AppDbContext _context;
        public TypeOfCooperationRepository(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Insert typeofcooperation in database
        /// </summary>
        /// <param name="typeofcooperation"></param>
        /// <returns>Tuple<string,bool></returns>
        /// <exception cref="Tuple<string,false>"></exception>
        public async Task<Tuple<string, bool>> Add(TypeOfCooperation typeofcooperation)
        {
            try
            {
                if (!TypeOfCooperationCrudValidation.CheckDuplicate(_context, typeofcooperation))
                {
                    var result = _context.TypeOfCooperations.AddAsync(typeofcooperation);
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
        /// Delete  typeofcooperation in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple<string, bool></returns>
        /// <exception cref="Tuple<string, false"></exception>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                if (!TypeOfCooperationCrudValidation.CheckIsItUsed(_context, id))
                {
                    var existingtypeofcooperation = await Get(id);
                    if (existingtypeofcooperation != null)
                    {
                        existingtypeofcooperation.IsDeleted = true;
                        await _context.SaveChangesAsync();
                        return Tuple.Create("حذف با موفقیت انجام شد.", true);
                    }
                    return Tuple.Create("مورد مد نظر جهت حذف وجود ندارد.", false);
                }
                return Tuple.Create("این مورد قبلا در طرف معامله استفاده شده. اجازه حذف ندارید.", false);
            }
            catch (Exception ex)
            {
                return Tuple.Create("حذف با خطا روبرو شد.", false);
            }
        }
        /// <summary>
        ///  Get TypeOfCooperation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TypeOfCooperation</returns>
        public async Task<TypeOfCooperation> Get(Guid id)
        {
            try
            {
                var model = _context.TypeOfCooperations.Where(x => x.ID == id && x.IsDeleted == false).FirstOrDefault();
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
        /// Get all TypeOfCooperation from database
        /// </summary>
        /// <returns>List<TypeOfCooperation></returns>
        /// <exception cref="List<TypeOfCooperation>"></exception>
        public async Task<List<TypeOfCooperation>> GetAll()
        {
            try
            {
                var models = await _context.TypeOfCooperations.Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToListAsync();
                return models;
            }
            catch (Exception ex)
            {
                return new List<TypeOfCooperation>();
            }
        }
        /// <summary>
        /// Update TypeOfCooperation from database
        /// </summary>
        /// <param name="typeofcooperation"></param>
        /// <returns>Tuple<string, bool></returns>
        public async Task<Tuple<string, bool>> Update(TypeOfCooperation typeofcooperation)
        {
            try
            {
                var existingtypeofcooperation = await Get(typeofcooperation.ID);
                if (existingtypeofcooperation != null)
                {
                    if (!TypeOfCooperationCrudValidation.CheckDuplicate(_context, typeofcooperation))
                    {
                        existingtypeofcooperation.IsDeleted = false;
                        existingtypeofcooperation.Title = typeofcooperation.Title;
                        existingtypeofcooperation.FirstWord = typeofcooperation.FirstWord;
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
