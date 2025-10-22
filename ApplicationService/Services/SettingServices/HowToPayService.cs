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
    internal class HowToPayService:IHowToPayService
    {
        private IUnitOfWork _unitOfWork;
        public HowToPayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن نحوه پرداخت به دیتابیس
        /// </summary>
        /// <param name="howToPayDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(HowToPayDto howToPayDto)
        {
            try
            {
                howToPayDto.key = Guid.NewGuid();
                var model = HowToPayAutoMapperProfile.DtoToEntity(howToPayDto);
                return await _unitOfWork.HowToPayRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  نحوه پرداخت جریمه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.HowToPayRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام نحوه پرداخت جریمه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<HowToPayDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.HowToPayRepository.GetAll();
                return HowToPayAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<HowToPayDto>();
            }
        }
        /// <summary>
        /// بروزرسانی نحوه پرداخت جریمه 
        /// </summary>
        /// <param name="howToPayDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(HowToPayDto howToPayDto)
        {
            try
            {
                var model = HowToPayAutoMapperProfile.DtoToEntity(howToPayDto);
                return await _unitOfWork.HowToPayRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
