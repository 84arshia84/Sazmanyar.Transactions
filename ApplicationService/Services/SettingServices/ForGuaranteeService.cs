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
    internal class ForGuaranteeService:IForGuaranteeService
    {
        private IUnitOfWork _unitOfWork;
        public ForGuaranteeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن بابت (تضمین ) به دیتابیس
        /// </summary>
        /// <param name="forGuaranteeDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(ForGuaranteeDto forGuaranteeDto)
        {
            try
            {
                forGuaranteeDto.key = Guid.NewGuid();
                var model = ForGuaranteeAutoMapperProfile.DtoToEntity(forGuaranteeDto);
                return await _unitOfWork.ForGuaranteeRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  بابت (تضمین ) جریمه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.ForGuaranteeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام بابت (تضمین ) جریمه ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<ForGuaranteeDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.ForGuaranteeRepository.GetAll();
                return ForGuaranteeAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<ForGuaranteeDto>();
            }
        }
        /// <summary>
        /// بروزرسانی بابت (تضمین ) جریمه 
        /// </summary>
        /// <param name="forGuaranteeDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(ForGuaranteeDto forGuaranteeDto)
        {
            try
            {
                var model = ForGuaranteeAutoMapperProfile.DtoToEntity(forGuaranteeDto);
                return await _unitOfWork.ForGuaranteeRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
