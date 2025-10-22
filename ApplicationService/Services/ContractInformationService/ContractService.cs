using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.User;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.Common;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ContractAccessGroups;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.History;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.PublicEntities;
using ApplicationService.ServicesContract.Users;
using ApplicationService.ServicesContract.WFEContract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationService.Services.ContractInformationService
{
    internal class ContractService : IContractService
    {
        private IUnitOfWork _unitOfWork;
        private IContractFinancialDetailsService _contractFinancialDetailsService;
        private IContractTimeProfileService _contractTimeProfileService;
        private IServiceExplanationService _serviceExplanationService;
        private IErrorLoggerService _errorLoggerService;
        private IContractCoefficientService _contractCoefficientService;
        private IContractGuaranteeService _contractGuaranteeService;
        private IContractCheckListValuesService _contractCheckListValuesService;
        private IDescriptionService _descriptionService;
        private IAttachService _attachService;
        private IWfeContractService _wfeContractService;
        private IUserService _userService;
        private IConfiguration _configuration;
        private IContractAccessGroupFilterService _filterService;
        private IInvoiceAccessGroupFilterService _invoiceAccessGroupFilterService;
        private IHistoryService _historyService;
        public ContractService(
            IUnitOfWork unitOfWork,
            IContractFinancialDetailsService contractFinancialDetailsService,
            IContractTimeProfileService contractTimeProfileService,
            IServiceExplanationService serviceExplanationService,
            IErrorLoggerService errorLoggerService,
            IContractCoefficientService contractCoefficientService,
            IContractGuaranteeService contractGuaranteeService,
            IContractCheckListValuesService contractCheckListValuesService,
            IDescriptionService descriptionService,
            IAttachService attachService,
            IWfeContractService wfeContractService,
            IUserService userService,
            IConfiguration configuration,
            IContractAccessGroupFilterService filterService,
            IInvoiceAccessGroupFilterService invoiceAccessGroupFilterService,
            IHistoryService historyService,
                IContractLogService contractLogService

            )
        {
            _unitOfWork = unitOfWork;
            _contractFinancialDetailsService = contractFinancialDetailsService;
            _contractTimeProfileService = contractTimeProfileService;
            _serviceExplanationService = serviceExplanationService;
            _contractCoefficientService = contractCoefficientService;
            _errorLoggerService = errorLoggerService;
            _contractGuaranteeService = contractGuaranteeService;
            _contractCheckListValuesService = contractCheckListValuesService;
            _descriptionService = descriptionService;
            _attachService = attachService;
            _wfeContractService = wfeContractService;
            _userService = userService;
            _configuration = configuration;
            _filterService = filterService;
            _invoiceAccessGroupFilterService = invoiceAccessGroupFilterService;
            _historyService = historyService;
            _contractLogService = contractLogService;  // 🔥 حیاتی‌ترین خط

        }
        /// <summary>
        /// اضافه کردن اطلاعات قرارداد
        /// اضافه کردن مشخصات زمانی قرارداد
        /// اضافه کردن مشخصات مالی قرارداد
        /// </summary>
        /// <param name="contractDto"></param>
        /// <returns>Tuple{string,bool}</returns>
        /// <exception cref="{string, false}"></exception>
        public async Task<(string message, bool isSuccess)> Add(ContractDto contractDto, LoginUserDto userDto)
        {
            var publicEntityId = Guid.Empty;
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var wfe = await _wfeContractService.GetFirstStage(userDto.FullQualifyName);
                var entity = await ContractAutoMapperProfile.DtoToEntity(contractDto, user, _errorLoggerService);
                if (wfe != null)
                {
                    entity.contract.CurrentStageId = wfe.Id;
                }
                // ولیدیشن کد یکسان
                var duplicate = await _unitOfWork.ContractRepository.GetByContractNumber(entity.contract.ContractNumber);
                if (duplicate != null)
                {
                    return ("کد قرارداد موجود است", false);
                }

                var insertContract = await _unitOfWork.ContractRepository.Add(entity.contract);

                (string message, bool isSuccess) insertServiceExplanations;
                if (insertContract.isSuccess == true)
                {
                    var insertContractTimeProfile = await _contractTimeProfileService.AddProfile(entity.contractTimeProfile);
                    if (insertContractTimeProfile)
                    {
                        var insertContractFinancialDetail = await _contractFinancialDetailsService.AddFinancialDetaile(entity.contractFinancialDetails);
                        if (insertContractFinancialDetail)
                        {
                            await _unitOfWork.Save();
                            publicEntityId = entity.contract.ID;
                            await _wfeContractService.InsertDefaultApprovers(user.FullQualifyName, entity.contract.ID);
                            if (entity.serviceExplanation == null)
                            {
                                return ("قرارداد بدون شرح خدمت  ثبت شد.", false);
                            }
                            insertServiceExplanations = await _serviceExplanationService.AddServiceExplanation(entity.serviceExplanation);
                            await _unitOfWork.Save();
                            if (entity.contractGuarantees != null)
                            {
                                await _contractGuaranteeService.AddGuarantee(entity.contractGuarantees);
                            }
                            if (contractDto.descriptionDtos != null)
                            {
                                await _descriptionService.Add(contractDto.descriptionDtos, false, entity.contract.ID);
                            }
                            if (contractDto.attachDtos != null)
                            {
                                await _attachService.Add(contractDto.attachDtos, false, entity.contract.ID);
                            }
                            await _historyService.AddHistory(user.ID, "افزودن  قرارداد", entity.contract.ID, user.FullName);
                            await _unitOfWork.Save();

                            return ("قرارداد با موفقیت ثبت شد", true);
                        }
                        return ("ثبت مشخصات مالی قرارداد با خطا مواجه شد.", false);
                    }
                    return ("ثبت مشخصات زمانی قرارداد با خطا مواجه شد.", false);
                }
                return ("ثبت  قرارداد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                await DeleteExceptinalContract(publicEntityId);
                await _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// حذف کردن قرارداد
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple{string, bool}</returns>
        /// <exception cref="{string, false}"></exception>
        public async Task<(string message, bool isSuccess)> Delete(Guid id, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                await _historyService.AddHistory(user.ID, "حذف قرارداد", id, user.FullName);
                return await _unitOfWork.ContractRepository.Delete(id, user.ID);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// گرفتن اطلاعات یک قرارداد،
        /// گرفتن مشخصات زمانی یک قرارداد،
        /// گرفتن مشخصات مالی یک قرارداد
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ContractDto</returns>
        /// <exception cref="ContractDto"></exception>
        public async Task<ContractDto> Get(Guid id)
        {
            try
            {
                var model = await ContractAutoMapperProfile.EntityToDto(await _unitOfWork.ContractRepository.Get(id), _errorLoggerService);
                return model;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new ContractDto();
            }
        }
        /// <summary>
        /// گرفتن اطلاعات تمامی قرارداد ها،
        /// گرفتن مشخصات زمانی تمامی قرارداد ها،
        /// گرفتن مشخصات مالی تمامی قرارداد ها
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List{ContractDto}</returns>
        /// <exception cref="List{ContractDto}"></exception>
        public async Task<List<ContractDto>> GetAll(string userName, int situation)
        {
            try
            {

                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.Mine)
                {
                    //return await ContractAutoMapperProfile.EntitesToDtos(await _unitOfWork.ContractRepository.GetAllMine(user.ID), _errorLoggerService);
                    var entites = await _unitOfWork.ContractRepository.GetAllMine(user.ID);
                    var dtos = await ContractAutoMapperProfile.EntitesToDtos(entites, _errorLoggerService);
                    return dtos.AsQueryable().OrderDefault().ToList();

                }
                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForAction)
                {
                    var ContractIds = await _wfeContractService.GetEntitiesAwaitingUserAction(userName);
                    if (ContractIds?.Any() == true)
                    {
                        // return await ContractAutoMapperProfile.EntitesToDtos(await _unitOfWork.ContractRepository.GetAllWaitForAction(ContractIds), _errorLoggerService);
                        var entities = await _unitOfWork.ContractRepository.GetAllWaitForAction(ContractIds);
                        var dtos = await ContractAutoMapperProfile.EntitesToDtos(entities, _errorLoggerService);
                        return dtos.AsQueryable().OrderDefault().ToList();
                    }
                }

                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.WaitForConfirm)
                {
                    var ContractIds = await _wfeContractService.GetEntitiesAwaitingApproval(userName);
                    if (ContractIds?.Any() == true)
                    {
                        // return await ContractAutoMapperProfile.EntitesToDtos(await _unitOfWork.ContractRepository.GetAllWaitForAction(ContractIds), _errorLoggerService);
                        var entities = await _unitOfWork.ContractRepository.GetAllWaitForAction(ContractIds);
                        var dtos = await ContractAutoMapperProfile.EntitesToDtos(entities, _errorLoggerService);
                        return dtos.AsQueryable().OrderDefault().ToList();
                    }
                }

                if ((WorkFlowSituationEnum)situation == WorkFlowSituationEnum.All)
                {
                    var ContractIds = await _wfeContractService.GetAllUserCanSeenIds(userName);
                    var filteredData = await _filterService.FilterContracts(await _unitOfWork.ContractRepository.GetAll(), ContractIds, user.ID);
                    if (filteredData != null)
                    {
                        var dtos = await ContractAutoMapperProfile.EntitesToDtos(filteredData, _errorLoggerService);
                        return dtos.AsQueryable().OrderDefault().ToList();
                    }
                }
                return new List<ContractDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDto>();
            }
        }
        /// <summary>
        /// قرارداد های که کاربر هنگام ثبت الحاقیه می تواند ببیند
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractDto>> GetAllForAddendum(string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var ContractIds = await _wfeContractService.GetAllUserCanSeenIds(userName);
                var filteredData = await _filterService.FilterContracts(await _unitOfWork.ContractRepository.GetAllWithFinalApprove(), ContractIds, user.ID);
                if (filteredData?.Any() == true)
                {
                    var dtos = await ContractAutoMapperProfile.EntitesToDtos(filteredData, _errorLoggerService);
                    return dtos.AsQueryable().OrderDefault().ToList();
                }
                return new List<ContractDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDto>();
            }
        }
        /// <summary>
        /// قراردادهایی که در ثبت صورت وضعیت می تواند ببیند
        /// به همراه آخرین مبلغ قرارداد
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ContractDto>> GetAllForInvoice(string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var contracts = await _unitOfWork.ContractRepository.GetAll();
                var filteredData = await _invoiceAccessGroupFilterService.FilterContractsInInvoice(await _unitOfWork.ContractRepository.GetAllWithFinalApprove(), user.ID);
                if (filteredData?.Any() == true)
                {
                    var dtos = await ContractAutoMapperProfile.EntitesToDtos(filteredData, _errorLoggerService);
                    return dtos.AsQueryable().OrderDefault().ToList();
                }
                return new List<ContractDto>();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<ContractDto>();
            }
        }

        public async Task<string> GetLastContractAmount(Guid contractId)
        {
            try
            {
                var AllServiceExplenations = new List<ServiceExplanation>();
                var addendumId = await _unitOfWork.ContractAddendumRepository.GetLastAddendumOfContract(contractId);
                if (addendumId != null && addendumId != Guid.Empty)
                {
                    AllServiceExplenations = await _serviceExplanationService.GetAllServiceExplenationOfAddendumForInvoice(contractId, addendumId);
                    if (AllServiceExplenations.Count > 0)
                    {
                        return ContractLastAmount.CalculateContractLastAmount(AllServiceExplenations, _errorLoggerService);
                    }
                }
                return ContractLastAmount.CalculateContractLastAmount(await _unitOfWork.ServiceExplanationRepository.GetAll(contractId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return "";
            }
        }

        public async Task<bool> HasInvoice(Guid contractId)
        {
            try
            {
                var model = await _unitOfWork.ContractRepository.HasInvoice(contractId);
                if (model == null) { return false; }
                if (model.InvoiceBaseInformations.Any() == false) { return false; }
                return true;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        /// <summary>
        /// بروزرسانی اطلاعات قرارداد
        /// بروزرسانی مشخصات زمانی قرارداد
        /// بروزرسانی مشخصات مالی قرارداد
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Tuple{string, bool}</returns>
        /// <exception cref="{string, false}"></exception>
        /// 
        private readonly IContractLogService _contractLogService;  // ← این باید بالا تعریف شده باشه

        public async Task<(string message, bool isSuccess)> Update(ContractDto contractDto, LoginUserDto userDto)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var entityBeforeUpdate = await _unitOfWork.ContractRepository.Get(contractDto.contractKey);
                var entity = await ContractAutoMapperProfile.DtoToEntity(contractDto, userDto, _errorLoggerService);

                // انتقال مقادیر ثابت از نسخه قبلی
                entity.contract.InsertContractBy = entityBeforeUpdate.InsertContractBy;
                entity.contract.InsertContractDate = entityBeforeUpdate.InsertContractDate;
                entity.contract.IsFinalApprove = entityBeforeUpdate.IsFinalApprove;
                entity.contract.LastActionTitle = entityBeforeUpdate.LastActionTitle;
                entity.contract.CurrentStatusTitle = entityBeforeUpdate.CurrentStatusTitle;
                entity.contract.CurrentStageId = entityBeforeUpdate.CurrentStageId;

                var contractUpdate = await _unitOfWork.ContractRepository.Update(entity.contract);
                if (!contractUpdate.isSuccess)
                    return ("بروزرسانی قرارداد با خطا مواجه شد.", false);

                var timeProfileUpdate = await _contractTimeProfileService.UpdateProfile(entity.contractTimeProfile);
                if (!timeProfileUpdate)
                    return ("بروزرسانی مشخصات زمانی قرارداد با خطا مواجه شد.", false);

                var financialDetail = await _contractFinancialDetailsService.UpdateFinancialDetaile(entity.contractFinancialDetails);
                if (!financialDetail)
                    return ("بروزرسانی مشخصات مالی قرارداد با خطا مواجه شد.", false);

                if (entity.contractCoefficients != null)
                    await _contractCoefficientService.UpdateCoefficient(entity.contractCoefficients);

                if (entity.contractGuarantees != null)
                    await _contractGuaranteeService.UpdateGuarantee(entity.contractGuarantees);

                if (entity.contract.ContractCheckListValues == null)
                    await _contractCheckListValuesService.DeleteCheckListValues(entity.contract.ID);
                else
                    await _contractCheckListValuesService.UpdateCheckListValues(entity.contract.ContractCheckListValues);

                if (contractDto.descriptionDtos != null)
                    await _descriptionService.Update(contractDto.descriptionDtos, false, entity.contract.ID);

                if (contractDto.attachDtos != null)
                    await _attachService.Update(contractDto.attachDtos, false, entity.contract.ID);

                await _unitOfWork.Save();

                if (entity.serviceExplanation == null)
                    return ("قرارداد بدون شرح خدمت ثبت شد.", false);

                await _serviceExplanationService.UpdateServiceExplanation(entity.serviceExplanation);

                // ثبت تاریخچه
                await _historyService.AddHistory(user.ID, "ویرایش قرارداد", entity.contract.ID, user.FullName);

                // 🟢 ثبت تغییرات در لاگ (فقط فیلدهایی که تغییر کرده‌اند)
                await _contractLogService.AddContractLog(entityBeforeUpdate, entity.contract, user.FullQualifyName);

                await _unitOfWork.Save();
                return ("قرارداد با موفقیت بروزرسانی شد", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ($"❌ خطا: {ex.Message} | Inner: {ex.InnerException?.Message}", false);
            }
        }
        /// <summary>
        /// بروزرسانی موارد قرارداد در حالت الحاقیه
        /// </summary>
        /// <param name="contractDto"></param>
        /// <param name="userDto"></param>
        /// <param name="AddendumId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(string message, bool isSuccess)> UpdateInAddendum(ContractDto contractDto, LoginUserDto userDto, Guid AddendumId)
        {
            try
            {
                var entityBeforUpdate = await _unitOfWork.ContractRepository.Get(contractDto.contractKey);
                var entity = await ContractAutoMapperProfile.DtoToEntity(contractDto, userDto, _errorLoggerService);
                entity.contract.HasAddendum = true;
                entity.contract.InsertContractBy = entityBeforUpdate.InsertContractBy;
                entity.contract.InsertContractDate = entityBeforUpdate.InsertContractDate;
                entity.contract.IsFinalApprove = entityBeforUpdate.IsFinalApprove;
                entity.contract.LastActionTitle = entityBeforUpdate.LastActionTitle;
                entity.contract.CurrentStatusTitle = entityBeforUpdate.CurrentStatusTitle;
                entity.contract.CurrentStageId = entityBeforUpdate.CurrentStageId;

                var contractUpdate = await _unitOfWork.ContractRepository.Update(entity.contract);
                if (contractUpdate.isSuccess == true)
                {
                    var TimeProfileUpdate = await _contractTimeProfileService.UpdateProfile(entity.contractTimeProfile);
                    if (TimeProfileUpdate == true)
                    {
                        var FinancialDetaile = await _contractFinancialDetailsService.UpdateFinancialDetaile(entity.contractFinancialDetails);
                        if (FinancialDetaile == true)
                        {
                            if (entity.contractCoefficients != null)
                            {
                                var contractCoefficient = await _contractCoefficientService.UpdateCoefficientInAddendum(entity.contractCoefficients, AddendumId);
                            }
                            if (entity.contract.ContractCheckListValues == null)
                            {
                                await _contractCheckListValuesService.DeleteCheckListValues(entity.contract.ID);
                            }
                            if (entity.contract.ContractCheckListValues != null)
                            {
                                await _contractCheckListValuesService.UpdateCheckListValues(entity.contract.ContractCheckListValues);
                            }
                            if (contractDto.descriptionDtos != null)
                            {
                                await _descriptionService.Update(contractDto.descriptionDtos, false, AddendumId);
                            }
                            if (contractDto.attachDtos != null)
                            {
                                await _attachService.Update(contractDto.attachDtos, false, AddendumId);
                            }
                            await _unitOfWork.Save();
                            if (entity.serviceExplanation == null)
                            {
                                return ("قرارداد بدون شرح خدمت  ثبت شد.", false);
                            }
                            var serviceExplanation = await _serviceExplanationService.UpdateServiceExplanationInAddendum(entity.serviceExplanation, AddendumId);
                            await _unitOfWork.Save();
                            return ("قرارداد با موفقیت بروزرسانی شد", true);
                        }
                        return ("بروز رسانی مشخصات مالی قرارداد با خطا مواجه شد.", false);
                    }
                    return ("بروز رسانی مشخصات زمانی قرارداد با خطا مواجه شد.", false);
                }
                return ("بروز رسانی  قرارداد با خطا مواجه شد.", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }
        /// <summary>
        /// بروزرسانی وضعیت قرارداد
        /// </summary>
        /// <param name="newId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateStatus(Guid contractId, Guid newId)
        {
            try
            {
                var result = await _unitOfWork.ContractRepository.UpdateStatus(contractId, newId);
                if (result == true)
                {
                    await _unitOfWork.Save();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
        internal async Task DeleteExceptinalContract(Guid contractId)
        {
            try
            {
                await _unitOfWork.ContractRepository.DeleteExceptinalContract(contractId);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public Task<List<ContractDto>> SearchForAddendum(string userName, string? term)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContractDto>> SearchForInvoice(string userName, string? term)
        {
            throw new NotImplementedException();
        }


        public async Task<(string message, bool isSuccess)> AddByTranaction(ContractFromTransactionDto contractDto, LoginUserDto userDto)
        {
            var publicEntityId = Guid.Empty;
            try
            {
                var user = await _userService.GetByFullQualifyName(userDto.FullQualifyName, _configuration["ConnectionStrings:DbConnection"]);
                var wfe = await _wfeContractService.GetFirstStage(userDto.FullQualifyName);
                var entity = await ContractFromTransactionMapper.DtoToEntity(contractDto, user, _errorLoggerService);
                if (wfe != null)
                {
                    entity.contract.CurrentStageId = wfe.Id;
                }
                // contractdto.ServiceExplanation.IsForExecution == true;

                if (contractDto.TransactionId != null)
                {
                    var exec = await _unitOfWork.TransactionExecutionRequestRepository.Get(contractDto.TransactionId);
                    if (exec == null)
                        return ("درخواست برگزاری معامله یافت نشد.", false);

                    if (exec.IsFinalApprove != true)
                        return ("درخواست برگزاری معامله هنوز نهایی (FinalApproved) نشده است.", false);
                    //if (execExplanations != null && execExplanations.Any())
                    //{
                    //    var mapped = new List<ServiceExplanation>();
                    //    long order = 1;
                    //    foreach (var x in execExplanations)
                    //    {
                    //        var se = new ServiceExplanation
                    //        {
                    //            // <-- use original id from execution explanation instead of new Guid
                    //            ID = x.ID,

                    //            // map the rest
                    //            Title = x.Title,
                    //            UnitOfMeasurementID = x.UnitOfMeasurementID,
                    //            ExplanationType = x.ExplanationType,
                    //            Order = order++,
                    //            IsForExecutionRequest = true,

                    //            // very important: link to the new contract id
                    //            ContractID = exec.Id,

                    //            // if ServiceExplanation has CurrencyID non-nullable, set default or map
                    //            // CurrencyID = defaultCurrencyId, // یا هر مقداری که لازمه

                    //            // map any other fields that exist in exec explanation and service explanation
                    //            UnitAmount = x.UnitAmount,
                    //            TotalAmount = x.TotalAmount,
                    //            ExplanationStartingDate = x.ExplanationStartingDate,
                    //            ExplanationEndingDate = x.ExplanationEndingDate,
                    //            // ...
                    //        };

                    //        mapped.Add(se);
                    //    }

                    //    // سپس mapped را به ریپو/سرویس ServiceExplanation اضافه کن
                    //}

                    var execExplanations = exec.ExecutionRequestServiceExplanations;
                    if (execExplanations != null && execExplanations.Any())
                    {
                        var mapped = new List<ServiceExplanation>();
                        long order = 1;
                        foreach (var x in execExplanations)
                        {
                            var se = new ServiceExplanation
                            {
                                ID = Guid.NewGuid(),
                                Title = x.Title,
                                UnitOfMeasurementID = x.UnitOfMeasurementID,
                                ExplanationType = x.ExplanationType,
                                Order = order++,
                                IsForExecutionRequest = true
                            };
                            mapped.Add(se);
                        }
                        var ins = await _serviceExplanationService.AddServiceExplanation(mapped);
                        if (!ins.isSuccess)
                            return ("ثبت شرح خدماتِ منتقل‌شده از برگزاری معامله با خطا مواجه شد.", false);
                    }
                }
                var insertContract = await _unitOfWork.ContractRepository.Add(entity.contract);

                (string message, bool isSuccess) insertServiceExplanations;
                if (insertContract.isSuccess == true)
                {
                    var insertContractTimeProfile = await _contractTimeProfileService.AddProfile(entity.contractTimeProfile);
                    if (insertContractTimeProfile)
                    {
                        var insertContractFinancialDetail = await _contractFinancialDetailsService.AddFinancialDetaile(entity.contractFinancialDetails);
                        if (insertContractFinancialDetail)
                        {
                            await _unitOfWork.Save();
                            publicEntityId = entity.contract.ID;
                            await _wfeContractService.InsertDefaultApprovers(user.FullQualifyName, entity.contract.ID);
                            await _unitOfWork.Save();
                            if (entity.contractGuarantees != null)
                            {
                                await _contractGuaranteeService.AddGuarantee(entity.contractGuarantees);
                            }
                            if (contractDto.descriptionDtos != null)
                            {
                                await _descriptionService.Add(contractDto.descriptionDtos, false, entity.contract.ID);
                            }
                            if (contractDto.attachDtos != null)
                            {
                                await _attachService.Add(contractDto.attachDtos, false, entity.contract.ID);
                            }
                            await _historyService.AddHistory(user.ID, "افزودن  قرارداد", entity.contract.ID, user.FullName);
                            await _unitOfWork.Save();

                            return ("قرارداد با موفقیت ثبت شد", true);
                        }
                        return ("ثبت مشخصات مالی قرارداد با خطا مواجه شد.", false);
                    }
                    return ("ثبت مشخصات زمانی قرارداد با خطا مواجه شد.", false);
                }
                return ("ثبت  قرارداد با خطا مواجه شد.", false);
            }

            catch (Exception ex)
            {
                await DeleteExceptinalContract(publicEntityId);
                await _errorLoggerService.SaveError(ex);
                return ("در اتصال به دیتابیس با مشکل مواجه شدیم.", false);
            }
        }

        public async Task<List<ContractDto>> GetAllWithIsForTransaction()
        {
            //var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
            try
            {
                var models = await _unitOfWork.ContractRepository.GetWithIsFromExecution();
                var dtos = await ContractAutoMapperProfile.EntitesToDtos(models, _errorLoggerService);
                return dtos;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}




