using ApplicationService.DtoModels.SettingDtos;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.SettingMapper;
using ApplicationService.ServicesContract.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SettingServices
{
    internal class CorespondentRealService:ICorespondentRealService
    {
        private IUnitOfWork _unitOfWork;
        public CorespondentRealService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن طرف معامله حقیقی به دیتابیس
        /// </summary>
        /// <param name="corespondentReal"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(CorespondentRealDto corespondentReal)
        {
            try
            {
                corespondentReal.key =  Guid.NewGuid();
                var model = CorespondentRealAutoMapperProfile.DtoToEntity(corespondentReal);
                return await _unitOfWork.CorespondentRealRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف طرف معامله حقیقی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.CorespondentRealRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام طرف معامله حقیقی ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CorespondentRealDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.CorespondentRealRepository.GetAll();
                return CorespondentRealAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<CorespondentRealDto>();
            }
        }
        /// <summary>
        /// بروزرسانی طرف معامله حقیقی 
        /// </summary>
        /// <param name="corespondentReal"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(CorespondentRealDto corespondentReal)
        {
            try
            {
                var model = CorespondentRealAutoMapperProfile.DtoToEntity(corespondentReal);
                return await _unitOfWork.CorespondentRealRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
