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
    public class CreditSourceService:ICreditSourceService
    {
        private IUnitOfWork _unitOfWork;
        public CreditSourceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن محل تامین اعتبار به دیتابیس
        /// </summary>
        /// <param name="creditSourceDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(CreditSourceDto creditSourceDto)
        {
            try
            {
                creditSourceDto.key = Guid.NewGuid();
                var model = CreditSourceAutoMapperProfile.DtoToEntity(creditSourceDto);
                return await _unitOfWork.CreditSourceRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف محل تامین اعتبار
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.CreditSourceRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام محل تامین اعتبار ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CreditSourceDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.CreditSourceRepository.GetAll();
                return CreditSourceAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<CreditSourceDto>();
            }
        }
        /// <summary>
        /// بروزرسانی محل تامین اعتبار
        /// </summary>
        /// <param name="creditSourceDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(CreditSourceDto creditSourceDto)
        {
            try
            {
                var model = CreditSourceAutoMapperProfile.DtoToEntity(creditSourceDto);
                return await _unitOfWork.CreditSourceRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
