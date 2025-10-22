using AppCore.Entities.Attaches;
using AppCore.Entities.Descriptions;
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
using static System.Collections.Specialized.BitVector32;

namespace ApplicationService.Services.PublicEntitiesServices
{


    internal class AttachService : IAttachService
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly IUnitOfWork _unitOfWork;

        public AttachService(IErrorLoggerService errorLoggerService, IUnitOfWork unitOfWork)
        {
            _errorLoggerService = errorLoggerService;
            _unitOfWork = unitOfWork;
        }







        /// <summary>
        ///
        /// </summary>
        /// <param name="attach"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Add(AttachDto attach, bool autosave, Guid? sectionId = null)
        {
            try
            {
                await _unitOfWork.AttachRepository.Add(AttachAutoMapperProfile.DtoToEntity(attach, _errorLoggerService, sectionId));
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
        /// <param name="attach"></param>
        /// <param name="autosave">
        ///  این پارامتر 
        /// برای زمانی هست که همون لحظه بخواهد دیتا ذخیره شود
        /// ولی اگز مقدارش منفی   
        /// باشد یعنی نیاز بوده تا ذخیره شدن بقیه دیتا ها این دیتا ذخیره نشود.
        /// </param>
        /// <returns></returns>
        public async Task Add(List<AttachDto> attach, bool autosave, Guid? sectionId = null)
        {
            try
            {
                var modelFiltered = attach.Where(x => x.IsDeleted == false).ToList();
                await _unitOfWork.AttachRepository.Add(
                       AttachAutoMapperProfile.DtosToEntities(modelFiltered, _errorLoggerService, sectionId)
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
        public async Task Delete(Guid attach, bool autosave)
        {
            try
            {
                await _unitOfWork.AttachRepository.Delete(attach);
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

        public async Task<List<AttachDto>> GetAll(Guid sectionId)
        {
            try
            {
                return AttachAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.AttachRepository.GetAll(sectionId), _errorLoggerService
                    );
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<AttachDto>();
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
        public async Task Update(AttachDto attach, bool autosave)
        {
            try
            {
                await _unitOfWork.AttachRepository.Update(
                    AttachAutoMapperProfile.DtoToEntity(attach, _errorLoggerService)
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
        public async Task Update(List<AttachDto> attach, bool autosave, Guid? sectionId = null)
        {
            try
            {
                foreach (var item in attach)
                {
                    if (item.IsDeleted == true)
                    {
                        await Delete(item.Id, autosave);
                        continue;
                    }
                    await _unitOfWork.AttachRepository.Update(
                           AttachAutoMapperProfile.DtoToEntity(item, _errorLoggerService, sectionId)
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
