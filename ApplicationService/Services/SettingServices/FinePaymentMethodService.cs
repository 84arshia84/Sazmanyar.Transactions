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
    internal class FinePaymentMethodService:IFinePaymentMethodService
    {
        private IUnitOfWork _unitOfWork;
        public FinePaymentMethodService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن روش های پرداخت جریمه به دیتابیس
        /// </summary>
        /// <param name="finePaymentMethodDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(FinePaymentMethodDto finePaymentMethodDto)
        {
            try
            {
                finePaymentMethodDto.key = Guid.NewGuid();
                var model = FinePaymentMethodAutoMapperProfile.DtoToEntity(finePaymentMethodDto);
                return await _unitOfWork.FinePaymentMethodRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  روش های پرداخت جریمه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.FinePaymentMethodRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام روش های پرداخت جریمه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<FinePaymentMethodDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.FinePaymentMethodRepository.GetAll();
                return FinePaymentMethodAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<FinePaymentMethodDto>();
            }
        }
        /// <summary>
        /// بروزرسانی روش های پرداخت جریمه 
        /// </summary>
        /// <param name="finePaymentMethodDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(FinePaymentMethodDto finePaymentMethodDto)
        {
            try
            {
                var model = FinePaymentMethodAutoMapperProfile.DtoToEntity(finePaymentMethodDto);
                return await _unitOfWork.FinePaymentMethodRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
