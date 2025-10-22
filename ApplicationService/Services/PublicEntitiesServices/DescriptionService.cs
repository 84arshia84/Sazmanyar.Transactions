using AppCore.UnitOfWork;
using ApplicationService.DtoModels.PublicEntitiesDtos;
using ApplicationService.Mapper.PublicEntitiesMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.PublicEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationService.Services.PublicEntitiesServices
{
    

    internal class DescriptionService : IDescriptionService
    {
        private IErrorLoggerService _errorLoggerService;
        private IUnitOfWork _unitOfWork;
        public DescriptionService(IErrorLoggerService errorLoggerService, IUnitOfWork unitOfWork)
        {
            _errorLoggerService = errorLoggerService;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="description"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Add(DescriptionDto description, bool autosave, Guid? sectionId = null)
        {
            try
            {
                await _unitOfWork.DescriptionRepository.Add(DescriptionAutoMapperProfile.DtoToEntity(description, _errorLoggerService, sectionId));
                if (autosave)
                {
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="description"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Add(List<DescriptionDto> description, bool autosave, Guid? sectionId = null)
        {
            try
            {
                await _unitOfWork.DescriptionRepository.Add(DescriptionAutoMapperProfile.DtosToEntities(description, _errorLoggerService, sectionId));
                if (autosave)
                {
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="description"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Delete(Guid description, bool autosave)
        {
            try
            {
                await _unitOfWork.DescriptionRepository.Delete(description);
                if (autosave)
                {
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public async Task<List<DescriptionDto>> GetAll(Guid sectionId)
        {
            try
            {
                return DescriptionAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.DescriptionRepository.GetAll(sectionId), _errorLoggerService
                    );
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<DescriptionDto>();
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="description"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Update(DescriptionDto description, bool autosave)
        {
            try
            {
                await _unitOfWork.DescriptionRepository.Update(
                    DescriptionAutoMapperProfile.DtoToEntity(description, _errorLoggerService)
                    );
                if (autosave)
                {
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="description"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Update(List<DescriptionDto> description, bool autosave, Guid? sectionId = null)
        {
            try
            {
                foreach (var item in description)
                {
                    if (item.IsDeleted == true)
                    {
                        await Delete(item.Id, autosave);
                        continue;
                    }
                    await _unitOfWork.DescriptionRepository.Update(
                           DescriptionAutoMapperProfile.DtoToEntity(item, _errorLoggerService, sectionId)
                            );
                }
                if (autosave)
                {
                    await _unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
    }
}

