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
    internal class TransActionTypeService:ITransActionTypeService
    {
        private IUnitOfWork _unitOfWork;
        public TransActionTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// اضافه کردن تیپ معامله به دیتابیس
        /// </summary>
        /// <param name="transActionTypeDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Add(TransActionTypeDto transActionTypeDto)
        {
            try
            {
                transActionTypeDto.key = Guid.NewGuid();
                var model = TransActionTypeAutoMapperProfile.DtoToEntity(transActionTypeDto);
                return await _unitOfWork.TransActionTypeRepository.Add(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف  تیپ معامله 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.TransActionTypeRepository.Delete(id);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن تمام تیپ معامله ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<TransActionTypeDto>> GetAll()
        {
            try
            {
                var models = await _unitOfWork.TransActionTypeRepository.GetAll();
                return TransActionTypeAutoMapperProfile.EntitiesToDtos(models);
            }
            catch (Exception ex)
            {

                return new List<TransActionTypeDto>();
            }
        }
        /// <summary>
        /// بروزرسانی تیپ معامله 
        /// </summary>
        /// <param name="transActionTypeDto"></param>
        /// <returns></returns>
        public async Task<Tuple<string, bool>> Update(TransActionTypeDto transActionTypeDto)
        {
            try
            {
                var model = TransActionTypeAutoMapperProfile.DtoToEntity(transActionTypeDto);
                return await _unitOfWork.TransActionTypeRepository.Update(model);
            }
            catch (Exception ex)
            {
                return Tuple.Create("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
    }
}
