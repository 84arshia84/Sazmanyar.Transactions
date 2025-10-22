using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.ContractCoefficients;
using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractCoefficientService : IContractCoefficientService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public ContractCoefficientService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public Task<(string message, bool isSuccess)> AddCoefficient(ContractCoefficientDto contractCoefficient)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> AddCoefficient(List<ContractCoefficient> contractCoefficient)
        {
            try
            {
                await _unitOfWork.ContractCoefficientRepository.Add(contractCoefficient);
                return ("ثبت ضرایب موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("افزودن ضرایب قرارداد با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> DeleteCoefficient(Guid id)
        {
            try
            {
                return await _unitOfWork.ContractCoefficientRepository.Delete(id);
            }
            catch (Exception ex)
            {

                throw;
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
        public async Task<(string message, bool isSuccess)> DeleteCoefficientInAddendum(ContractCoefficient contractCoefficient,Guid addendumId)
        {
            try
            {
                var unUpdatedCoefficient = await _unitOfWork.ContractCoefficientRepository.Get(contractCoefficient.Id);
                if(unUpdatedCoefficient != null)
                {
                    unUpdatedCoefficient.IsDeleted = true;
                    unUpdatedCoefficient.DeleteDate = DateTime.Now;
                    unUpdatedCoefficient.DeletedInAddedumId = addendumId;
                    return ("حذف موفقیت آمیز بود", true);
                }
                return ("مورد مد نظر حهت حذف وجود نداشت", false);
               // contractCoefficient.IsDeleted = true;
              //  contractCoefficient.DeleteDate = DateTime.Now;
                //return await _unitOfWork.ContractCoefficientRepository.DeleteInAddendum(contractCoefficient);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Task<ContractCoefficientDto> GetCoefficient(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContractCoefficientDto>> GetAllCoefficient(Guid contractId)
        {
            try
            {
                return ContractCoefficientAutoMapperProfile.EntitiesToDtos(
                    await _unitOfWork.ContractCoefficientRepository.GetAll(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractCoefficientDto>();
            }
        }
        /// <summary>
        /// تمامی ضرایبی که در الحاقیه اضافه و یا کم شده است برای آخرین الحاقیه
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractCoefficientDto>> GetAllCoefficientForAddendum(Guid contractId, Guid? addendumId)
        {
            try
            {
                if(addendumId != null)
                {
                    var sieveDatas = await SieveData(
                        await _unitOfWork.ContractCoefficientRepository.GetAllForAddendum(contractId),
                        await _unitOfWork.ContractAddendumRepository.GetAll(contractId),
                        addendumId);
                    return ContractCoefficientAutoMapperProfile.EntitiesToDtos(sieveDatas, _errorLoggerService);

                }
                return ContractCoefficientAutoMapperProfile.EntitiesToDtos(
                        await _unitOfWork.ContractCoefficientRepository.GetAll(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {

                return new List<ContractCoefficientDto>();
            }
        }
        public Task<(string message, bool isSuccess)> UpdateCoefficient(ContractCoefficientDto contractCoefficient)
        {
            throw new NotImplementedException();
        }

        public async Task<(string message, bool isSuccess)> UpdateCoefficient(List<ContractCoefficient> contractCoefficient)
        {
            try
            {
                foreach (var item in contractCoefficient)
                {
                    if (item.IsDeleted == true)
                    {
                        await DeleteCoefficient(item.Id);
                        continue;
                    }
                    await _unitOfWork.ContractCoefficientRepository.Update(item);
                }
                return ("ویرایش موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا مواجه شد.", false);
            }
        }

        public async Task<(string message, bool isSuccess)> UpdateCoefficientInAddendum(List<ContractCoefficient> contractCoefficient, Guid addendumId)
        {
            try
            {
                if(contractCoefficient is null)
                {
                    return ("چیزی برای ویرایش وجود ندارد.",true);
                }
                foreach (var item in contractCoefficient)
                {
                   if((item.AddendumId == Guid.Empty || item.AddendumId==null) && item.IsAddendum == true ) { item.AddendumId = addendumId;}
                   await CheckForCrudInAddendumMode(item, addendumId);
                }
                return ("ویرایش موفقیت آمیز بود.", true);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا مواجه شد.", false);
            }
        }
        /// <summary>
        /// این تابع حالت های زیر را بررسی میکند
        /// 1- اگر در الحاقیه جدید، موردی اضافه و بعد حذف شد کاملا از دیتابیس حذف شود.
        /// 2- اگر ضرایب الحاقیه دیگر حذف بشود سافت دلیت انجام میشه
        /// 3- اگر ضرایب الحاقیه دیگر بروزرسانی بشود 
        /// UpdatedInAddendum= true  
        /// می شود و مقدار بروزشده به عنوان ضریب جدید در دیتابیس ذخیره می شود
        /// این فرآیند  ها برای این هست که در الحاقیه های جدید مقدار جدید نمایش داده شود 
        /// و در الحاقیه های قبلی همان مقادیر خودشان باقی بماند.
        /// </summary>
        /// <param name="contractCoefficient"></param>
        /// <param name="addendumId"></param>
        /// <returns></returns>
        internal async Task CheckForCrudInAddendumMode(ContractCoefficient contractCoefficient,Guid addendumId)
        {
            try
            {
                if (contractCoefficient.AddendumId == addendumId)
                {
                    if (contractCoefficient.IsDeleted == true)
                    {
                        await DeleteCoefficient(contractCoefficient.Id);
                        return;
                    }
                    await _unitOfWork.ContractCoefficientRepository.Update(contractCoefficient);
                    return;
                }
                if (contractCoefficient.IsDeleted == true)
                {
                    await DeleteCoefficientInAddendum(contractCoefficient,addendumId);
                    return;
                }
                if (contractCoefficient.UpdatedInAddendum == true)
                {
                    var unUpdatedCoefficient = await _unitOfWork.ContractCoefficientRepository.Get(contractCoefficient.Id);
                    if (unUpdatedCoefficient != null)
                    {
                        unUpdatedCoefficient.UpdatedInAddendum = true;
                        unUpdatedCoefficient.UpdateInAddendumId = addendumId;
                    }
                   // await _unitOfWork.ContractCoefficientRepository.UpdateInAddendum(contractCoefficient);
                    contractCoefficient.Id = Guid.NewGuid();
                    contractCoefficient.UpdatedInAddendum = false;
                    contractCoefficient.UpdateInAddendumId = null;
                    contractCoefficient.AddendumId = addendumId;
                    await _unitOfWork.ContractCoefficientRepository.Add(contractCoefficient);
                    return;
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
            
        }
        /// <summary>
        /// این تابع برای غربال کردن داده های ضرایب متعلق به یک الحاقیه می باشد.
        /// </summary>
        /// <param name="AllServiceInAllAddendums"></param>
        /// <param name="addendums"></param>
        /// <param name="currentAddendumId"></param>
        /// <returns></returns>
        internal async Task<List<ContractCoefficient>> SieveData(List<ContractCoefficient> AllCoefficientInAllAddendums, List<ContractAddendum> addendums, Guid? currentAddendumId)
        {
            var SieveData = new List<ContractCoefficient>();
            var me = addendums.FirstOrDefault(a => a.Id == currentAddendumId);
            for (int i = 0; i < AllCoefficientInAllAddendums.Count(); i++)
            {
                var item = new ContractCoefficient();
                /// اول بررسی می کنیم ضریب متعلق به الحاقیه کنونی هست یا خیر
                if (AllCoefficientInAllAddendums[i].AddendumId == currentAddendumId)
                {
                    item = AllCoefficientInAllAddendums[i];
                    SieveData.Add(item);
                    continue;
                }
                if (AllCoefficientInAllAddendums[i].AddendumId != currentAddendumId)
                {
                    /// اگر ضریب متعلق به الحاقیه کنونی نبود
                    /// اول بررسی میکنیم ببینیم که حذف شده هست یا خیر
                    if (AllCoefficientInAllAddendums[i].IsDeleted == true)
                    {
                        /// اگر حذف شده هست 
                        /// کدام الحاقیه آن را حذف کرده است
                        /// اگر الحاقیه کنونی آن را حذف کرده باشد نیازی به آن نیست
                        /// در غیر این صورت بررسی میکنیم که الحاقیه که حذف کرده تاریخ ثبتش بعد از الحاقیه کنونی هست یا خیر
                        var WhitchAddendumDeleted = addendums.FirstOrDefault(a => a.Id == AllCoefficientInAllAddendums[i].DeletedInAddedumId);
                        if (WhitchAddendumDeleted.InsertAddendumDate != me.InsertAddendumDate)
                        {
                            if (WhitchAddendumDeleted.InsertAddendumDate > me.InsertAddendumDate)
                            {
                                /// اگر تاریخ الحاقیه حذف کننده بعد از الحاقیه کنونی بود پس یعنی تا این الحاقیه وجود داشته 
                                /// پس به لیست من اضافه بشود
                                item = AllCoefficientInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                        }
                        continue;
                    }
                    /// در این مرحله بررسی میکنیم ببینیم که ضریب ویرایش شده هست یا خیر
                    if (AllCoefficientInAllAddendums[i].UpdatedInAddendum == false)
                    {
                        /// اگر ویرایش نشده باشد بررسی میکنیم که مطعلق به کدام الحاقیه هست
                        /// اگر متعلق به هیچ الحاقیه نبود پس یعنی برای قرارداد بوده و اضافه می شود
                        /// در غیر این صورت تاریخ الحاقیه راچک میکنیم ببینیم برای قبل الحاقیه کنونی هست یاخیر
                        /// اگر برای قبل باشد پس در این الحاقیه می تواند وجود داشته باشد 
                        /// در غیر این صورت یعنی برای الحاقیه های بعدی است و نمی توان در این الحاقیه وجود داشته باشد
                        if (AllCoefficientInAllAddendums[i].AddendumId != null)
                        {
                            var WhitchAddendumAdded = addendums.FirstOrDefault(a => a.Id == AllCoefficientInAllAddendums[i].AddendumId);
                            if (WhitchAddendumAdded.InsertAddendumDate < me.InsertAddendumDate)
                            {
                                item = AllCoefficientInAllAddendums[i];
                                SieveData.Add(item);
                                continue;
                            }
                        }
                        if (AllCoefficientInAllAddendums[i].AddendumId == null)
                        {
                            item = AllCoefficientInAllAddendums[i];
                            SieveData.Add(item);
                            continue;
                        }
                        continue;
                    }
                    /// اگر ضریب ویرایش شده باشد
                    /// اول بررسی میکنیم که برای قرارداد هست یا الحاقیه
                    if (AllCoefficientInAllAddendums[i].UpdatedInAddendum == true)
                    {
                        /// اگر برای الحاقیه باشد 
                        /// بررسی میکنیم که متعلق به کدام الحاقیه هست
                        /// اگر برای الحاقیه بعد از این الحاقیه باشد آن را اضافه نمیکنیم
                        /// اگر برای قبل من باشد اضافه می شود
                        /// نکته متربرآورد که متعلق به خود این الحاقیه هستند و در همین الحاقیه ویرایش شدند در این جا
                        /// در نظر گرفته نمی شوند
                        if (AllCoefficientInAllAddendums[i].AddendumId != null)
                        {   
                            var WhitchAddendumUpdated = addendums.FirstOrDefault(a => a.Id == AllCoefficientInAllAddendums[i].UpdateInAddendumId);
                            if (WhitchAddendumUpdated.InsertAddendumDate != me.InsertAddendumDate)
                            {
                                if (WhitchAddendumUpdated.InsertAddendumDate < me.InsertAddendumDate)
                                {
                                    item = AllCoefficientInAllAddendums[i];
                                    SieveData.Add(item);
                                    continue;
                                }
                            }

                            continue;
                        }
                        var WhitchAddendumUpdatedForContract = addendums.FirstOrDefault(a => a.Id == AllCoefficientInAllAddendums[i].UpdateInAddendumId);
                        if (WhitchAddendumUpdatedForContract.InsertAddendumDate != me.InsertAddendumDate)
                        {
                            if (WhitchAddendumUpdatedForContract.InsertAddendumDate > me.InsertAddendumDate)
                            {
                                item = AllCoefficientInAllAddendums[i];
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
