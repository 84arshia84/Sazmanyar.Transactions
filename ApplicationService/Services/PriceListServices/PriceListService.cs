using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceLists;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.PriceListMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.PriceLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.PriceListServices
{
    internal class PriceListService : IPriceListService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;

        public PriceListService(IUnitOfWork unitOfWork ,IErrorLoggerService errorLoggerService) {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        /// <summary>
        /// اضافه کردن فهرست بها به وسیله اکسل
        /// </summary>
        /// <param name="file"></param>
        /// <returns>bool</returns>
        /// <exception cref="false"></exception>
        public async Task<(string message,bool isSuccess)> AddPriceListByExcel(Stream file)
        {
            try
            {
                var datas = await PriceListExcelService.ImportExcel(file, _errorLoggerService);
                var result =await  _unitOfWork.PriceListRepository.ImportExcel(datas);
                await _unitOfWork.Save();
                return result;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("سرویس دریافت اکسل با خطا روبرو شد.",false);
            }
        }
        public async Task<(List<PriceList>, string)> GetAllPriceLists()
        {
            try
            {
                return  await _unitOfWork.PriceListRepository.GetAllPriceList();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (new List<PriceList>(), "خطادر سرویس گرفتن داده");
            }
        }
        public async Task<(Guid ID, string message, bool isSuccess)> Add(PriceListDtos priceListDtos)
        {
            try
            {
                var model = PriceListAutoMapperProfile.DtoToEntity(priceListDtos, _errorLoggerService);
                model.ID=Guid.NewGuid();
               return await _unitOfWork.PriceListRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (Guid.Empty,"خطا در سرویس اضافه کردن", false);
            }
        }
        public async Task<(string message, bool isSuccess)> Update(PriceListDtos priceListDtos)
        {
            try
            {
                return await _unitOfWork.PriceListRepository.Update(PriceListAutoMapperProfile.DtoToEntity(priceListDtos, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در سرویس بروزرسانی کردن", false);
            }
        }
        /// <summary>
        /// حذف فهرست بها
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                return await _unitOfWork.PriceListRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در سرویس حذف کردن", false);
            }
        }
        /// <summary>
        /// گرفتن تمامی سال ها
        /// </summary>
        /// <returns></returns>
        public async Task<List<PriceList>> GetAllYears()
        {
            try
            {
                return await _unitOfWork.PriceListRepository.GetAllYears();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceList>();
            }
        }
    }
}
