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
    internal class CorespondentLegalService: ICorespondentLegalService
    {
        private IUnitOfWork _unitOfWork;
        public CorespondentLegalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن طرف معامله حقوقی به دیتابیس
        /// </summary>
        /// <param name="corespondentLegal"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(CorespondentLegalDto corespondentLegal)
        {
            try
            {
                corespondentLegal.key =Guid.NewGuid();
                var model = CorespondentLegalAutoMapperProfile.DtoToEntity(corespondentLegal);
                return await _unitOfWork.CorespondentLegalRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف طرف معامله حقوقی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.CorespondentLegalRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام طرف معامله حقوقی ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CorespondentLegalDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.CorespondentLegalRepository.GetAll();
                return CorespondentLegalAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<CorespondentLegalDto>();
            }
        }
        /// <summary>
        /// بروزرسانی طرف معامله حقوقی 
        /// </summary>
        /// <param name="basisForEndingTheProject"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(CorespondentLegalDto corespondentLegal)
        {
            try
            {
                var model = CorespondentLegalAutoMapperProfile.DtoToEntity(corespondentLegal);
                return await _unitOfWork.CorespondentLegalRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
