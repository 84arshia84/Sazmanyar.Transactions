
using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ServiceExplanationService : IServiceExplanationService
    {
        private IUnitOfWork _unitOfWork;
        private IContractEstimatedmeterService _contractEstimatedmeterService;
        private IErrorLoggerService _errorLoggerService;
        public ServiceExplanationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IContractEstimatedmeterService contractEstimatedmeterService)
        {
            _unitOfWork = unitOfWork;
            _contractEstimatedmeterService = contractEstimatedmeterService;
            _errorLoggerService = errorLoggerService;
        }
        /// <summary>
        /// افزودن شرح خدمت به دیتابیس
        /// </summary>
        /// <param name="serviceExplanation"></param>
        /// <returns>Tuple{string,bool}</returns>
        public async Task<(string message, bool isSuccess)> AddServiceExplanation(List<ServiceExplanation> serviceExplanation)
        {
            try
            {
                var result = await _unitOfWork.ServiceExplanationRepository.Add(serviceExplanation);
                return result;
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف شرح خدمات قرارداد
        /// </summary>
        /// <param name="serviceExplanationId"></param>
        /// <returns>Tuple{string,bool}</returns>
        /// <exception cref="Tuple{string,false}"></exception>
        public async Task<(string message, bool isSuccess)> DeleteServiceExplanation(Guid serviceExplanationId)
        {
            try
            {
                return await _unitOfWork.ServiceExplanationRepository.Delete(serviceExplanationId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// این تابع موارد را به صورت کامل از دیتابیس 
        /// حذف نمی کند و فقط 
        /// isdeleted = true
        /// میکند.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> DeleteInAddendum(ServiceExplanation serviceExplanation, Guid addendumId)
        {
            try
            {
                var unUpdatedExplanation = await _unitOfWork.ServiceExplanationRepository.Get(serviceExplanation.ID);
                if (unUpdatedExplanation != null)
                {
                    unUpdatedExplanation.IsDeleted = true;
                    unUpdatedExplanation.DeleteDate = DateTime.Now;
                    unUpdatedExplanation.DeletedInAddedumId = addendumId;
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("مورد مد نظر حهت حذف وجود نداشت", false);
                //return await _unitOfWork.ServiceExplanationRepository.DeleteInAddendum(serviceExplanation);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف کردن تمامی شرح خدمات یک الحاقیه که حذف شده است
        /// </summary>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteAllServiceExplanationForThisAddendum(Guid addendumId)
        {
            try
            {
                await _unitOfWork.ServiceExplanationRepository.DeleteServiceExplanationOfAddendum(addendumId);
                await _unitOfWork.Save();
                return true;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
        public async Task<ServiceExplanationDto> GetById(Guid id)
        {
            try
            {
                return ServiceExplanationAutoMapperProfile.EntityToDto(await _unitOfWork.ServiceExplanationRepository.Get(id),_errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        /// <summary>
        /// گرفتن شرح خدمات قرارداد
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="List{ServiceExplanationDto}"></exception>
        public async Task<List<ServiceExplanationDto>> GetAllServiceExplanations(Guid contractId)
        {
            try
            {
                return ServiceExplanationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ServiceExplanationRepository.GetAll(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ServiceExplanationDto>();
            }
        }
        /// <summary>
        /// گرفتن تمامی شرح خدمات برای آخرین الحاقیه 
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ServiceExplanationDto>> GetAllServiceExplenationForAddendum(Guid contractId, Guid? addendumId)
        {
            try
            {
               
                if (addendumId != null)
                {
                   var sieveDatas = await SieveData(
                    await _unitOfWork.ServiceExplanationRepository.GetAllForAddendum(contractId),
                    await _unitOfWork.ContractAddendumRepository.GetAll(contractId),
                    addendumId
                 );
                    return ServiceExplanationAutoMapperProfile.EntitiesToDtos(sieveDatas, _errorLoggerService);
                }
                return ServiceExplanationAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.ServiceExplanationRepository.GetAll(contractId)
                    , _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ServiceExplanationDto>();
            }
        }
        /// <summary>
        /// گرفتن تمامی شرح خدمات برای آخرین الحاقیه 
        /// برای نمایش در صورت وضعیت
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ServiceExplanation>> GetAllServiceExplenationOfAddendumForInvoice(Guid contractId, Guid? addendumId)
        {
            try
            {

                if (addendumId != null)
                {
                    var sieveDatas = await SieveData(
                     await _unitOfWork.ServiceExplanationRepository.GetAllForAddendum(contractId),
                     await _unitOfWork.ContractAddendumRepository.GetAll(contractId),
                     addendumId
                  );
                    if( sieveDatas != null )
                    {
                    return sieveDatas;
                    }
                }
                return new List<ServiceExplanation>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ServiceExplanation>();
            }
        }
        /// <summary>
        /// بروزرسانی شرح خدمات
        /// </summary>
        /// <param name="serviceExplanation"></param>
        /// <returns>Tuple{string,bool}</returns>
        /// <exception cref="Tuple{string,false}"></exception>
        public async Task<(string message, bool isSuccess)> UpdateServiceExplanation(List<ServiceExplanation> serviceExplanation)
        {
            try
            {
                /// زمانی که در ویرایش شرح خدمت متربرآورد دار اضافه شود 
                /// چون یکبار با شرح خدمت ثبت می شود و بار دیگر با سرویس متربرآورد
                /// داپلیکیت می افتد و در دیتابیس ذخیره نمی شود.
                /// پس اینجا متر برآورد شرح خدمت را خالی می کنیم بعد همه را با سرویس متربرآورد اضافه می کنیم.
                var serviceExplanationWithEstimatedMeter = serviceExplanation.Select(item => new ServiceExplanation
                {
                    ContractEstimatedmeters = item.ContractEstimatedmeters,
                }).ToList();
                foreach (var item in serviceExplanation)
                {
                    item.ContractEstimatedmeters = null;
                    if (item.IsDeleted == true)
                    {
                        await DeleteServiceExplanation(item.ID);
                        continue;
                    }
                    await _unitOfWork.ServiceExplanationRepository.Update(item);
                }
                foreach (var item in serviceExplanationWithEstimatedMeter)
                {
                    if (item.ContractEstimatedmeters == null) continue;
                    await _contractEstimatedmeterService.Update(item.ContractEstimatedmeters);
                }
                return ("ویرایش شرح خدمت موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> UpdateServiceExplanationInAddendum(List<ServiceExplanation> serviceExplanation, Guid AddendumId)
        {
            try
            {
                /// زمانی که در ویرایش شرح خدمت متربرآورد دار اضافه شود 
                /// چون یکبار با شرح خدمت ثبت می شود و بار دیگر با سرویس متربرآورد
                /// داپلیکیت می افتد و در دیتابیس ذخیره نمی شود.
                /// پس اینجا متر برآورد شرح خدمت را خالی می کنیم بعد همه را با سرویس متربرآورد اضافه می کنیم.
                var serviceExplanationWithEstimatedMeter = serviceExplanation.Select(item => new ServiceExplanation
                {
                    ContractEstimatedmeters = item.ContractEstimatedmeters,
                }).ToList();
                foreach (var item in serviceExplanation)
                {
                    item.ContractEstimatedmeters = null;
                    if ((item.ContractAddendumId == Guid.Empty || item.ContractAddendumId == null) && item.IsAddendum == true) { item.ContractAddendumId = AddendumId; }
                    await CheckForCrudInAddendumMode(item, AddendumId);
                }
                foreach (var item in serviceExplanationWithEstimatedMeter)
                {
                    if (item.ContractEstimatedmeters == null) continue;
                    await _contractEstimatedmeterService.UpdateInAddendum(item.ContractEstimatedmeters, AddendumId);
                }
                return ("ویرایش شرح خدمت موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// این تابع حالت های زیر را بررسی میکند
        /// 1- اگر در الحاقیه جدید، موردی اضافه و بعد حذف شد کاملا از دیتابیس حذف شود.
        /// 2- اگر شرح خدمت الحاقیه دیگر حذف بشود سافت دلیت انجام میشه
        /// 3- اگر شرح خدمت الحاقیه دیگر بروزرسانی بشود 
        /// UpdatedInAddendum= true  
        /// می شود و مقدار بروزشده به عنوان شرح خدمت جدید در دیتابیس ذخیره می شود
        /// این فرآیند  ها برای این هست که در الحاقیه های جدید مقدار جدید نمایش داده شود 
        /// و در الحاقیه های قبلی همان مقادیر خودشان باقی بماند.
        /// </summary>
        /// <param name="contractCoefficient"></param>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        internal async Task CheckForCrudInAddendumMode(ServiceExplanation serviceExplanation, Guid addendumId)
        {
            try
            {
                if (serviceExplanation.ContractAddendumId == addendumId)
                {
                    if (serviceExplanation.IsDeleted == true)
                    {
                        await DeleteServiceExplanation(serviceExplanation.ID);
                        return;
                    }
                    await _unitOfWork.ServiceExplanationRepository.Update(serviceExplanation);
                    return;
                }
                if (serviceExplanation.IsDeleted == true)
                {
                    await DeleteInAddendum(serviceExplanation, addendumId);
                    return;
                }
                if (serviceExplanation.UpdatedInAddendum == true)
                {
                    var unUpdatedExplanation = await _unitOfWork.ServiceExplanationRepository.Get(serviceExplanation.ID);
                    if (unUpdatedExplanation != null)
                    {
                        unUpdatedExplanation.UpdatedInAddendum = true;
                        unUpdatedExplanation.UpdateInAddendumId = addendumId;
                    }
                    //await _unitOfWork.ServiceExplanationRepository.UpdateInAddendum(serviceExplanation);
                    serviceExplanation.ID = Guid.NewGuid();
                    serviceExplanation.UpdatedInAddendum = false;
                    serviceExplanation.UpdateInAddendumId = null;
                    serviceExplanation.ContractAddendumId = addendumId;
                    await _unitOfWork.ServiceExplanationRepository.Add(serviceExplanation);
                    return;
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        /// این تابع برای غربال کردن داده های شرح خدمت متعلق به یک الحاقیه می باشد.
        /// </summary>
        /// <param name="AllServiceInAllAddendums"></param>
        /// <param name="addendums"></param>
        /// <param name="currentAddendumId"></param>
        /// <returns></returns>
        internal async Task<List<ServiceExplanation>> SieveData(List<ServiceExplanation> AllServiceInAllAddendums, List<ContractAddendum> addendums, Guid? currentAddendumId)
        {
            var SieveData = new List<ServiceExplanation>();
            var me = addendums.FirstOrDefault(a => a.Id == currentAddendumId);
            var contractId = Guid.Empty;
            var addendumId = Guid.Empty;
            var deleteAddendumId = Guid.Empty;
            var serviceExplanationId = Guid.Empty;
            for (int i = 0; i < AllServiceInAllAddendums.Count(); i++)
            {
                try
                {
                    var item = new ServiceExplanation();
                    /// اول بررسی می کنیم شرح خدمت متعلق به الحاقیه کنونی هست یا خیر
                    if (AllServiceInAllAddendums[i].ContractAddendumId == currentAddendumId)
                    {
                        item = AllServiceInAllAddendums[i];
                        SieveData.Add(item);
                        continue;
                    }
                    if (AllServiceInAllAddendums[i].ContractAddendumId != currentAddendumId)
                    {
                        /// اگر شرح خدمت متعلق به الحاقیه کنونی نبود
                        /// اول بررسی میکنیم ببینیم که حذف شده هست یا خیر
                        if (AllServiceInAllAddendums[i].IsDeleted == true)
                        {
                            /// اگر حذف شده هست 
                            /// کدام الحاقیه آن را حذف کرده است
                            /// اگر الحاقیه کنونی آن را حذف کرده باشد نیازی به آن نیست
                            /// در غیر این صورت بررسی میکنیم که الحاقیه که حذف کرده تاریخ ثبتش بعد از الحاقیه کنونی هست یا خیر
                            var WhitchAddendumDeleted = addendums.FirstOrDefault(a => a.Id == AllServiceInAllAddendums[i].DeletedInAddedumId);
                            if (WhitchAddendumDeleted.InsertAddendumDate != me.InsertAddendumDate)
                            {
                                if (WhitchAddendumDeleted.InsertAddendumDate > me.InsertAddendumDate)
                                {
                                    /// اگر تاریخ الحاقیه حذف کننده بعد از الحاقیه کنونی بود پس یعنی تا این الحاقیه وجود داشته 
                                    /// پس به لیست من اضافه بشود
                                    item = AllServiceInAllAddendums[i];
                                    SieveData.Add(item);
                                    continue;
                                }
                            }
                            continue;
                        }
                        /// در این مرحله بررسی میکنیم ببینیم که شرح خدمت ویرایش شده هست یا خیر
                        if (AllServiceInAllAddendums[i].UpdatedInAddendum == false)
                        {
                            /// اگر ویرایش نشده باشد بررسی میکنیم که مطعلق به کدام الحاقیه هست
                            /// اگر متعلق به هیچ الحاقیه نبود پس یعنی برای قرارداد بوده و اضافه می شود
                            /// در غیر این صورت تاریخ الحاقیه راچک میکنیم ببینیم برای قبل الحاقیه کنونی هست یاخیر
                            /// اگر برای قبل باشد پس در این الحاقیه می تواند وجود داشته باشد 
                            /// در غیر این صورت یعنی برای الحاقیه های بعدی است و نمی توان در این الحاقیه وجود داشته باشد
                            if (AllServiceInAllAddendums[i].ContractAddendumId != null)
                            {
                                var WhitchAddendumAdded = addendums.FirstOrDefault(a => a.Id == AllServiceInAllAddendums[i].ContractAddendumId);
                                if (WhitchAddendumAdded.InsertAddendumDate < me.InsertAddendumDate)
                                {
                                    item = AllServiceInAllAddendums[i];
                                    SieveData.Add(item);
                                    continue;
                                }
                            }
                            if (AllServiceInAllAddendums[i].ContractAddendumId == null)
                            {
                                item = AllServiceInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                            continue;
                        }
                        /// اگر شرح خدمت ویرایش شده باشد
                        /// اول بررسی میکنیم که برای قرارداد هست یا الحاقیه
                        if (AllServiceInAllAddendums[i].UpdatedInAddendum == true)
                        {
                            /// اگر برای الحاقیه باشد 
                            /// بررسی میکنیم که متعلق به کدام الحاقیه هست
                            /// اگر برای الحاقیه بعد از این الحاقیه باشد آن را اضافه نمیکنیم
                            /// اگر برای قبل من باشد اضافه می شود
                            /// نکته شرح خدماتی که متعلق به خود این الحاقیه هستند و در همین الحاقیه ویرایش شدند در این جا
                            /// در نظر گرفته نمی شوند
                            if (AllServiceInAllAddendums[i].ContractAddendumId != null)
                            {
                                var WhitchAddendumUpdated = addendums.FirstOrDefault(a => a.Id == AllServiceInAllAddendums[i].UpdateInAddendumId);
                                if (WhitchAddendumUpdated.InsertAddendumDate != me.InsertAddendumDate)
                                {
                                    if (WhitchAddendumUpdated.InsertAddendumDate < me.InsertAddendumDate)
                                    {
                                        item = AllServiceInAllAddendums[i];
                                        SieveData.Add(item);
                                        continue;
                                    }
                                }

                                continue;
                            }
                            var WhitchAddendumUpdatedForContract = addendums.FirstOrDefault(a => a.Id == AllServiceInAllAddendums[i].UpdateInAddendumId);
                            if (WhitchAddendumUpdatedForContract.InsertAddendumDate != me.InsertAddendumDate)
                            {
                                if (WhitchAddendumUpdatedForContract.InsertAddendumDate > me.InsertAddendumDate)
                                {
                                    item = AllServiceInAllAddendums[i];
                                    SieveData.Add(item);
                                    continue;
                                }
                            }
                            continue;
                        }
                    }
                }
                catch (Exception error)
                {
                    _errorLoggerService.SaveError(error);
                    continue;
                }
              
            }
            return SieveData;
        }
        // GetAll method for ServiceExplanations with IsFromExecution == true
        public async Task<List<ServiceExplanationDto>> GetWithIsFromExecution(Guid contractId)
        {
            try
            {
                return ServiceExplanationAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ServiceExplanationRepository.GetWithIsFromExecution(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ServiceExplanationDto>();
            }

        }
        
    }
}
