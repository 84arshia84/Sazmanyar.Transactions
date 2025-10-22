using ApplicationService.DtoModels.ContractDtos;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.UnitOfWork;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.ContractsInformation.ContractAddendums;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractEstimatedmeterService : IContractEstimatedmeterService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractEstimatedmeterService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }

        public async Task<(string message, bool isSuccess)> AddEstimatedmeter(ContractEstimatedmeter estimatedmeter)
        {
            try
            {
                var result = await _unitOfWork.ContractEstimatedmeterRepository.Add(estimatedmeter);
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("افزودن متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن متربرآورد با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> AddEstimatedmeter(List<ContractEstimatedmeter> estimatedmeters)
        {
            try
            {
                var result = await _unitOfWork.ContractEstimatedmeterRepository.Add(estimatedmeters);
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("افزودن متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("افزودن متربرآورد با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid id)
        {
            try
            {
                var result = await _unitOfWork.ContractEstimatedmeterRepository.Delete(id);
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("حذف متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("حذف متربرآورد با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> DeleteAll(Guid contractId)
        {
            try
            {
                var result = await _unitOfWork.ContractEstimatedmeterRepository.DeleteAll(contractId);
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("حذف متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("حذف متربرآورد با خطا مواجه شد.", false);
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
        public async Task<(string message, bool isSuccess)> DeleteInAddendum(ContractEstimatedmeter estimatedmeter, Guid addendumId)
        {
            try
            {
                var unUpdatedEstimatedMeter = await _unitOfWork.ContractEstimatedmeterRepository.Get(estimatedmeter.Id);
                if (unUpdatedEstimatedMeter != null)
                {
                    unUpdatedEstimatedMeter.IsDeleted = true;
                    unUpdatedEstimatedMeter.DeleteDate = DateTime.Now;
                    unUpdatedEstimatedMeter.DeletedInAddedumId = addendumId;
                    return ("حذف موفقیت آمیز بود.", true);
                }
                return ("حذف متربرآورد با خطا مواجه شد.", false);
                // estimatedmeter.IsDeleted = true;
                //estimatedmeter.DeleteDate = DateTime.Now;
                //var result = await _unitOfWork.ContractEstimatedmeterRepository.DeleteInAddendum(estimatedmeter);
                //if (result)
                //{
                //    return ("عملیات موفقیت آمیز بود.", true);
                //}
                //return ("حذف متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<EstimatedmeterDto> Get(Guid id)
        {
            try
            {
                return ContractEstimatedmeterAutoMapperProfile.EntityToDto(await _unitOfWork.ContractEstimatedmeterRepository.Get(id), _errorLoggerService);

            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new EstimatedmeterDto();
            }
        }

        public async Task<List<EstimatedmeterDto>> GetAll(Guid ContractId)
        {
            try
            {
                return ContractEstimatedmeterAutoMapperProfile.EntitiesToDtos(await _unitOfWork.ContractEstimatedmeterRepository.GetAll(ContractId), _errorLoggerService);

            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<EstimatedmeterDto>();
            }
        }

        public async Task<List<EstimatedmeterDto>> GetAllForAddendum(Guid contractId, Guid? addendumId)
        {
            try
            {
                if (addendumId != null)
                {
                    var seiveDatas = await SieveData(
                  await _unitOfWork.ContractEstimatedmeterRepository.GetAllForAddendum(contractId),
                  await _unitOfWork.ContractAddendumRepository.GetAll(contractId), addendumId);
                    return ContractEstimatedmeterAutoMapperProfile.EntitiesToDtos(seiveDatas, _errorLoggerService);
                }
                return ContractEstimatedmeterAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.ContractEstimatedmeterRepository.GetAllForAddendum(contractId),
                    _errorLoggerService);

            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return new List<EstimatedmeterDto>();
            }
        }

        public async Task<(string message, bool isSuccess)> Update(ContractEstimatedmeter estimatedmeter)
        {
            try
            {
                var result = await _unitOfWork.ContractEstimatedmeterRepository.Update(estimatedmeter);
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("بروزرسانی متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی متربرآورد با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> Update(List<ContractEstimatedmeter> estimatedmeter)
        {
            try
            {
                var result = false;
                foreach (var item in estimatedmeter)
                {
                    if (item.IsDeleted == true)
                    {
                        await Delete(item.Id);
                        continue;
                    }
                    result = await _unitOfWork.ContractEstimatedmeterRepository.Update(item);
                }
                if (result)
                {
                    return ("عملیات موفقیت آمیز بود.", true);
                }
                return ("بروزرسانی متربرآورد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {

                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی متربرآورد با خطا مواجه شد.", false);
            }
        }
        /// <summary>
        /// ویرایش متربرآورد ها در الحاقیه
        /// </summary>
        /// <param name="estimatedmeter"></param>
        /// <param name="AddendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> UpdateInAddendum(List<ContractEstimatedmeter> estimatedmeter, Guid AddendumId)
        {
            try
            {
                foreach (var item in estimatedmeter)
                {
                    if ((item.AddendumId == Guid.Empty || item.AddendumId == null) && item.IsAddendum == true) { item.AddendumId = AddendumId; }
                    await CheckForCrudInAddendumMode(item, AddendumId);
                }
                return ("عملیات موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("بروزرسانی متربرآورد با خطا مواجه شد.", false);
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
        internal async Task CheckForCrudInAddendumMode(ContractEstimatedmeter estimatedmeter, Guid addendumId)
        {
            try
            {
                if (estimatedmeter.AddendumId == addendumId)
                {
                    if (estimatedmeter.IsDeleted == true)
                    {
                        await Delete(estimatedmeter.Id);
                        return;
                    }
                    await _unitOfWork.ContractEstimatedmeterRepository.Update(estimatedmeter);
                    return;
                }
                if (estimatedmeter.IsDeleted == true)
                {
                    await DeleteInAddendum(estimatedmeter, addendumId);
                    return;
                }
                if (estimatedmeter.UpdatedInAddendum == true)
                {
                    var unUpdatedEstimatedMeter = await _unitOfWork.ContractEstimatedmeterRepository.Get(estimatedmeter.Id);
                    if (unUpdatedEstimatedMeter != null)
                    {
                        unUpdatedEstimatedMeter.UpdatedInAddendum = true;
                        unUpdatedEstimatedMeter.UpdateInAddendumId = addendumId;
                    }
                    //await _unitOfWork.ContractEstimatedmeterRepository.UpdateInAddendum(estimatedmeter);
                    estimatedmeter.Id = Guid.NewGuid();
                    estimatedmeter.UpdatedInAddendum = false;
                    estimatedmeter.UpdateInAddendumId = null;
                    estimatedmeter.AddendumId = addendumId;
                    await _unitOfWork.ContractEstimatedmeterRepository.Add(estimatedmeter);
                    return;
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        /// <summary>
        /// این تابع برای غربال کردن داده های متربرآورد متعلق به یک الحاقیه می باشد.
        /// </summary>
        /// <param name="AllEstimatedInAllAddendums"></param>
        /// <param name="addendums"></param>
        /// <param name="currentAddendumId"></param>
        /// <returns></returns>
        internal async Task<List<ContractEstimatedmeter>> SieveData(List<ContractEstimatedmeter> AllEstimatedInAllAddendums, List<ContractAddendum> addendums, Guid? currentAddendumId)
        {
            var SieveData = new List<ContractEstimatedmeter>();
            var me = addendums.FirstOrDefault(a => a.Id == currentAddendumId);
            for (int i = 0; i < AllEstimatedInAllAddendums.Count(); i++)
            {
                var item = new ContractEstimatedmeter();
                /// اول بررسی می کنیم متربرآورد متعلق به الحاقیه کنونی هست یا خیر
                if (AllEstimatedInAllAddendums[i].AddendumId == currentAddendumId)
                {
                    item = AllEstimatedInAllAddendums[i];
                    SieveData.Add(item);
                    continue;
                }
                if (AllEstimatedInAllAddendums[i].AddendumId != currentAddendumId)
                {
                    /// اگر متربرآورد متعلق به الحاقیه کنونی نبود
                    /// اول بررسی میکنیم ببینیم که حذف شده هست یا خیر
                    if (AllEstimatedInAllAddendums[i].IsDeleted == true)
                    {
                        /// اگر حذف شده هست 
                        /// کدام الحاقیه آن را حذف کرده است
                        /// اگر الحاقیه کنونی آن را حذف کرده باشد نیازی به آن نیست
                        /// در غیر این صورت بررسی میکنیم که الحاقیه که حذف کرده تاریخ ثبتش بعد از الحاقیه کنونی هست یا خیر
                        var WhitchAddendumDeleted = addendums.FirstOrDefault(a => a.Id == AllEstimatedInAllAddendums[i].DeletedInAddedumId);
                        if (WhitchAddendumDeleted.InsertAddendumDate != me.InsertAddendumDate)
                        {
                            if (WhitchAddendumDeleted.InsertAddendumDate > me.InsertAddendumDate)
                            {
                                /// اگر تاریخ الحاقیه حذف کننده بعد از الحاقیه کنونی بود پس یعنی تا این الحاقیه وجود داشته 
                                /// پس به لیست من اضافه بشود
                                item = AllEstimatedInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                        }
                        continue;
                    }
                    /// در این مرحله بررسی میکنیم ببینیم که متربرآورد ویرایش شده هست یا خیر
                    if (AllEstimatedInAllAddendums[i].UpdatedInAddendum == false)
                    {
                        /// اگر ویرایش نشده باشد بررسی میکنیم که مطعلق به کدام الحاقیه هست
                        /// اگر متعلق به هیچ الحاقیه نبود پس یعنی برای قرارداد بوده و اضافه می شود
                        /// در غیر این صورت تاریخ الحاقیه راچک میکنیم ببینیم برای قبل الحاقیه کنونی هست یاخیر
                        /// اگر برای قبل باشد پس در این الحاقیه می تواند وجود داشته باشد 
                        /// در غیر این صورت یعنی برای الحاقیه های بعدی است و نمی توان در این الحاقیه وجود داشته باشد
                        if (AllEstimatedInAllAddendums[i].AddendumId != null)
                        {
                            var WhitchAddendumAdded = addendums.FirstOrDefault(a => a.Id == AllEstimatedInAllAddendums[i].AddendumId);
                            if (WhitchAddendumAdded.InsertAddendumDate < me.InsertAddendumDate)
                            {
                                item = AllEstimatedInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                        }
                        if (AllEstimatedInAllAddendums[i].AddendumId == null)
                        {
                            item = AllEstimatedInAllAddendums[i];
                            SieveData.Add(item);
                            continue;
                        }
                        continue;
                    }
                    /// اگر متربرآورد ویرایش شده باشد
                    /// اول بررسی میکنیم که برای قرارداد هست یا الحاقیه
                    if (AllEstimatedInAllAddendums[i].UpdatedInAddendum == true)
                    {
                        /// اگر برای الحاقیه باشد 
                        /// بررسی میکنیم که متعلق به کدام الحاقیه هست
                        /// اگر برای الحاقیه بعد از این الحاقیه باشد آن را اضافه نمیکنیم
                        /// اگر برای قبل من باشد اضافه می شود
                        /// نکته متربرآورد که متعلق به خود این الحاقیه هستند و در همین الحاقیه ویرایش شدند در این جا
                        /// در نظر گرفته نمی شوند
                        if (AllEstimatedInAllAddendums[i].AddendumId != null)
                        {
                            var WhitchAddendumUpdated = addendums.FirstOrDefault(a => a.Id == AllEstimatedInAllAddendums[i].UpdateInAddendumId);
                            if (WhitchAddendumUpdated.InsertAddendumDate != me.InsertAddendumDate)
                            {
                                if (WhitchAddendumUpdated.InsertAddendumDate < me.InsertAddendumDate)
                                {
                                    item = AllEstimatedInAllAddendums[i];
                                    SieveData.Add(item);
                                    continue;
                                }
                            }

                            continue;
                        }
                        var WhitchAddendumUpdatedForContract = addendums.FirstOrDefault(a => a.Id == AllEstimatedInAllAddendums[i].UpdateInAddendumId);
                        if (WhitchAddendumUpdatedForContract.InsertAddendumDate != me.InsertAddendumDate)
                        {
                            if (WhitchAddendumUpdatedForContract.InsertAddendumDate > me.InsertAddendumDate)
                            {
                                item = AllEstimatedInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                        }
                        continue;
                    }
                }
            }
            return SieveData;
        }


    }
}
