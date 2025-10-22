using ApplicationService.DtoModels.PriceListsDtos;
using AppCore.Entities.PriceListEntities.PriceListClauses;
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
    internal class PriceListClauseService : IPriceListClauseService
    {
        private IErrorLoggerService _errorLoggerService;
        private IUnitOfWork _unitOfWork;
        public PriceListClauseService(IErrorLoggerService errorLoggerService, IUnitOfWork unitOfWork)
        {
            _errorLoggerService = errorLoggerService;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Clause"></param>
        /// <returns></returns>
        public async Task<(Guid ID, string message, bool isSuccess)> Add(PriceListClauseDto Clause)
        {
            try
            {
                var model = PriceListClauseAutoMapperProfile.DtoToEntity(Clause, _errorLoggerService);
                model.ID=Guid.NewGuid();    
                return await _unitOfWork.PriceListClauseRepository.Add(model);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (Guid.Empty, "خطا در سرویس افزودن فصل", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listClauses"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> AddList(List<PriceListClauseDto> listClauses)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cluadId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid cluadId)
        {
            try
            {
                return await _unitOfWork.PriceListClauseRepository.Delete(cluadId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در سرویس حذف فصل", false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListClause>> GetAll()
        {
            try
            {
                return await _unitOfWork.PriceListClauseRepository.GetAll();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceListClause>();
            }
        }
        /// <summary>
        /// گرفتن فصل های یک رشته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<PriceListClauseDto>> GetAllByFieldId(Guid id)
        {
            try
            {
                return PriceListClauseAutoMapperProfile.EntitesToDtos(await _unitOfWork.PriceListClauseRepository.GetAllByFieldId(id),_errorLoggerService);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<PriceListClauseDto>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Clause"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> Update(PriceListClauseDto Clause)
        {
            try
            {
                return await _unitOfWork.PriceListClauseRepository.Update(PriceListClauseAutoMapperProfile.DtoToEntity(Clause, _errorLoggerService));
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در سرویس بروزرسانی فصل",false);
            }
        }
    }
}
