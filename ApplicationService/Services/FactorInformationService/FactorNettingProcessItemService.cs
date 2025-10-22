using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.FactorInformation.FactorNettingProcessItems;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorFinancialDetaile;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.FactorDtos.FactorServiceExplanation;
using ApplicationService.Mapper.FactorMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.FactorInformation;
using ApplicationService.ServicesContract.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.FactorInformationService
{
    internal class FactorNettingProcessItemService : IFactorNettingProcessItemService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        private IUserService _userService;
        private IFactorServiceExplanationService _factorServiceExplanationService;
        public FactorNettingProcessItemService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IConfiguration configuration,
           IUserService userService, IFactorServiceExplanationService factorServiceExplanationService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
            _userService = userService;
            _factorServiceExplanationService = factorServiceExplanationService;
        }
        public async Task<(string message, bool isSuccess)> Add(FactorNettingProcessItemAddDto nettingProcessItem, string userName)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (nettingProcessItem.Percentage < 0 || nettingProcessItem.Percentage > 100)
                {
                    return ("درصد کسورات باید بین 0 تا 100 باشد", false);
                }

                // بررسی اینکه مجموع پیش‌پرداخت از مجموع کسورات بیشتر نباشد
                var existingItems = await _unitOfWork.FactorNettingProcessItemRepository.GetAll(nettingProcessItem.FactorId);
                var totalDeductions = existingItems.Where(x => x.IsDeduction && x.CurrencyId == nettingProcessItem.CurrencyId).Sum(x => x.Percentage);
                var totalPrepayments = existingItems.Where(x => x.Title.Contains("پیش پرداخت") && x.CurrencyId == nettingProcessItem.CurrencyId).Sum(x => x.Percentage);

                if (nettingProcessItem.IsDeduction && nettingProcessItem.Title.Contains("پیش پرداخت"))
                {
                    totalPrepayments += nettingProcessItem.Percentage;
                }
                else if (nettingProcessItem.IsDeduction)
                {
                    totalDeductions += nettingProcessItem.Percentage;
                }

                if (totalPrepayments > totalDeductions)
                {
                    return ("مجموع پیش‌پرداخت نمی‌تواند از مجموع کسورات بیشتر باشد", false);
                }

                // بررسی محدودیت پیش‌پرداخت بر اساس شرح خدمات قرارداد
                if (nettingProcessItem.IsDeduction && nettingProcessItem.Title.Contains("پیش پرداخت"))
                {
                    // برای فاکتورها، محدودیت پیش‌پرداخت بر اساس قوانین کسب‌وکار تعریف می‌شود
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                // بررسی محدودیت علی‌الحساب - معمولاً علی‌الحساب محدودیت خاصی ندارد اما می‌تواند بر اساس قوانین کسب‌وکار تنظیم شود
                if (nettingProcessItem.IsDeduction && nettingProcessItem.Title.Contains("علی الحساب"))
                {
                    // در اینجا می‌توان محدودیت خاصی برای علی‌الحساب تعریف کرد
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var result = await _unitOfWork.FactorNettingProcessItemRepository.Add(
                    FactorNettingProcessItemAutoMapperProfile.DtoToEntityAdd(nettingProcessItem, user.ID, _errorLoggerService));
                if (result)
                {
                    await _unitOfWork.Save();
                    return ("مقدار مورد نظر با موفقیت افزوده شد", true);
                }
                return ("خطا در افزودن", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در ذخیره سازی مقدار وارد شده", false);
            }
        }

        public async Task<bool> AddDefaultFactorData(FactorFinancialDetaileAddDto factorFinancialDetaile, List<FactorServiceExplanationAddDto> serviceExplanation, Guid factorId, Guid userid)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (factorFinancialDetaile.FactorInsurance_Percent < 0 || factorFinancialDetaile.FactorInsurance_Percent > 100 ||
                    factorFinancialDetaile.FactorValue_Added_Percent < 0 || factorFinancialDetaile.FactorValue_Added_Percent > 100 ||
                    factorFinancialDetaile.FactorTax_Percent < 0 || factorFinancialDetaile.FactorTax_Percent > 100 ||
                    factorFinancialDetaile.GoodJob_Percent < 0 || factorFinancialDetaile.GoodJob_Percent > 100)
                {
                    return false; // درصدها خارج از محدوده مجاز هستند
                }

                //var FactorServiceExplenationsTotalAmount = serviceExplanation.Sum(s => s.TotalAmount);
                var FactorServiceExplenationsTotalAmount = serviceExplanation
                                                 .GroupBy(x => x.CurrencyID)
                                                 .Select(g => new
                                                 {
                                                     CurrencyID = g.Key,
                                                     TotalAmountSum = g.Sum(x => x.TotalAmount ?? 0)
                                                 })
                                                 .ToList();
                for (int i = 0; i < FactorServiceExplenationsTotalAmount.Count; i++)
                {
                    var Insurance = new FactorNettingProcessItem();
                    Insurance.Id = Guid.NewGuid();
                    Insurance.FactorId = factorId;
                    Insurance.Title = "بیمه";
                    Insurance.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount[i].TotalAmountSum : 0) * (factorFinancialDetaile.FactorInsurance_Percent / 100));
                    Insurance.Percentage = factorFinancialDetaile.FactorInsurance_Percent;
                    Insurance.IsDeduction = true;
                    Insurance.IsEditable = true;
                    Insurance.IsDeleted = false;
                    Insurance.InsertDate = DateTime.Now;
                    Insurance.InsertBy = userid;
                    Insurance.NettingProcessTypes = NettingProcessTypesEnum.insurance;
                    Insurance.CurrencyId = FactorServiceExplenationsTotalAmount[i].CurrencyID;

                    await _unitOfWork.FactorNettingProcessItemRepository.Add(Insurance);

                    var AddedValue = new FactorNettingProcessItem();
                    AddedValue.Id = Guid.NewGuid();
                    AddedValue.FactorId = factorId;
                    AddedValue.Title = "ارزش افزوده";
                    AddedValue.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount[i].TotalAmountSum : 0) * (factorFinancialDetaile.FactorValue_Added_Percent / 100));
                    AddedValue.Percentage = factorFinancialDetaile.FactorValue_Added_Percent;
                    AddedValue.IsDeduction = false;
                    AddedValue.IsEditable = true;
                    AddedValue.IsDeleted = false;
                    AddedValue.InsertDate = DateTime.Now;
                    AddedValue.InsertBy = userid;
                    AddedValue.NettingProcessTypes = NettingProcessTypesEnum.addedValue;
                    AddedValue.CurrencyId = FactorServiceExplenationsTotalAmount[i].CurrencyID;

                    await _unitOfWork.FactorNettingProcessItemRepository.Add(AddedValue);

                    var Tax = new FactorNettingProcessItem();
                    Tax.Id = Guid.NewGuid();
                    Tax.FactorId = factorId;
                    Tax.Title = "مالیات";
                    Tax.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount[i].TotalAmountSum : 0) * (factorFinancialDetaile.FactorTax_Percent / 100));
                    Tax.Percentage = factorFinancialDetaile.FactorTax_Percent;
                    Tax.IsDeduction = true;
                    Tax.IsEditable = true;
                    Tax.IsDeleted = false;
                    Tax.InsertDate = DateTime.Now;
                    Tax.InsertBy = userid;
                    Tax.NettingProcessTypes = NettingProcessTypesEnum.Tax;
                    Tax.CurrencyId = FactorServiceExplenationsTotalAmount[i].CurrencyID;

                    await _unitOfWork.FactorNettingProcessItemRepository.Add(Tax);

                    var GoodJob = new FactorNettingProcessItem();
                    GoodJob.Id = Guid.NewGuid();
                    GoodJob.FactorId = factorId;
                    GoodJob.Title = "حسن انجام کار";
                    GoodJob.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount[i].TotalAmountSum : 0) * (factorFinancialDetaile.GoodJob_Percent / 100));
                    GoodJob.Percentage = factorFinancialDetaile.GoodJob_Percent;
                    GoodJob.IsDeduction = true;
                    GoodJob.IsEditable = true;
                    GoodJob.IsDeleted = false;
                    GoodJob.InsertDate = DateTime.Now;
                    GoodJob.InsertBy = userid;
                    GoodJob.NettingProcessTypes = NettingProcessTypesEnum.GoodJob;
                    GoodJob.CurrencyId = FactorServiceExplenationsTotalAmount[i].CurrencyID;

                    await _unitOfWork.FactorNettingProcessItemRepository.Add(GoodJob);
                }


                return true;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<(string message, bool isSuccess)> Delete(Guid nettingProcessItemId, string userName)
        {
            try
            {
                var user = await _userService.GetByFullQualifyName(userName, _configuration["ConnectionStrings:DbConnection"]);
                var result = await _unitOfWork.FactorNettingProcessItemRepository.Delete(nettingProcessItemId, user.ID);
                if (result)
                {
                    await _unitOfWork.Save();
                    return ("با موفقیت حذف شد", true);
                }
                return ("خطا در حذف", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در حذف مقدار مورد نظر", false);
            }
        }

        public async Task<FactorNettingProcessItemGetDto> Get(Guid nettingProcessItemId)
        {
            try
            {
                return FactorNettingProcessItemAutoMapperProfile.EntityToDto(await _unitOfWork.FactorNettingProcessItemRepository.Get(nettingProcessItemId), _errorLoggerService);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new FactorNettingProcessItemGetDto();
            }
        }

        public async Task<(List<FactorNettingProcessItemGetDto> factorNettingProcessesItems, List<NettedAmountGet> netted)> GetAll(Guid factorId)
        {
            try
            {
                var AllNettedAmount = new List<NettedAmountGet>();
                var models = await _unitOfWork.FactorNettingProcessItemRepository.GetAll(factorId);
                var serviceExplanations = await _factorServiceExplanationService.GetAll(factorId);
                var SumOfExpalantionsByCurrency = serviceExplanations
                                              .GroupBy(x => x.CurrencyID)
                                              .Select(g => new
                                              {
                                                  CurrencyID = g.Key,
                                                  TotalAmountSum = g.Sum(x => x.TotalAmount ?? 0)
                                              })
                                              .ToList();
                foreach (var amount in SumOfExpalantionsByCurrency)
                {
                    var NettedAmount = new NettedAmountGet();
                    NettedAmount.NettedAmount = FactorCalculationProcess.CalculatedNettedAmount(models, amount.TotalAmountSum, amount.CurrencyID);
                    NettedAmount.Currency = amount.CurrencyID;
                    AllNettedAmount.Add(NettedAmount);
                }
                return (FactorNettingProcessItemAutoMapperProfile.EntitiesToDtos(models, _errorLoggerService), AllNettedAmount);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return (new List<FactorNettingProcessItemGetDto>(), new List<NettedAmountGet>());
            }
        }

        public async Task<(string message, bool isSuccess)> Update(FactorNettingProcessItemUpdateDto nettingProcessItem)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (nettingProcessItem.Percentage < 0 || nettingProcessItem.Percentage > 100)
                {
                    return ("درصد کسورات باید بین 0 تا 100 باشد", false);
                }

                var lastData = await _unitOfWork.FactorNettingProcessItemRepository.Get(nettingProcessItem.Id);

                // بررسی اینکه مجموع پیش‌پرداخت از مجموع کسورات بیشتر نباشد
                var existingItems = await _unitOfWork.FactorNettingProcessItemRepository.GetAll(lastData.FactorId);
                var totalDeductions = existingItems.Where(x => x.IsDeduction && x.CurrencyId == lastData.CurrencyId && x.Id != nettingProcessItem.Id).Sum(x => x.Percentage);
                var totalPrepayments = existingItems.Where(x => x.Title.Contains("پیش پرداخت") && x.CurrencyId == lastData.CurrencyId && x.Id != nettingProcessItem.Id).Sum(x => x.Percentage);

                if (lastData.IsDeduction && lastData.Title.Contains("پیش پرداخت"))
                {
                    totalPrepayments += nettingProcessItem.Percentage;
                }
                else if (lastData.IsDeduction)
                {
                    totalDeductions += nettingProcessItem.Percentage;
                }

                if (totalPrepayments > totalDeductions)
                {
                    return ("مجموع پیش‌پرداخت نمی‌تواند از مجموع کسورات بیشتر باشد", false);
                }

                // بررسی محدودیت پیش‌پرداخت بر اساس شرح خدمات قرارداد
                if (lastData.IsDeduction && lastData.Title.Contains("پیش پرداخت"))
                {
                    // برای فاکتورها، محدودیت پیش‌پرداخت بر اساس قوانین کسب‌وکار تعریف می‌شود
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                // بررسی محدودیت علی‌الحساب - معمولاً علی‌الحساب محدودیت خاصی ندارد اما می‌تواند بر اساس قوانین کسب‌وکار تنظیم شود
                if (lastData.IsDeduction && lastData.Title.Contains("علی الحساب"))
                {
                    // در اینجا می‌توان محدودیت خاصی برای علی‌الحساب تعریف کرد
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                var result = await _unitOfWork.FactorNettingProcessItemRepository.Update(
                    FactorNettingProcessItemAutoMapperProfile.DtoToEntityUpdate(nettingProcessItem, lastData, _errorLoggerService));
                if (result)
                {
                    await _unitOfWork.Save();
                    return ("بروزرسانی موفقیت آمیز بود", true);
                }
                return ("خطا در بروزرسانی", false);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return ("خطا در بروزرسانی", false);
            }
        }

        public async Task UpdateDefaultFactorData(FactorFinancialDetaileUpdateDto factorFinancialDetaile, Guid factorId, List<FactorServiceExplanationUpdateDto> serviceExplanation)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (factorFinancialDetaile.FactorInsurance_Percent < 0 || factorFinancialDetaile.FactorInsurance_Percent > 100 ||
                    factorFinancialDetaile.FactorValue_Added_Percent < 0 || factorFinancialDetaile.FactorValue_Added_Percent > 100 ||
                    factorFinancialDetaile.FactorTax_Percent < 0 || factorFinancialDetaile.FactorTax_Percent > 100 ||
                    factorFinancialDetaile.GoodJob_Percent < 0 || factorFinancialDetaile.GoodJob_Percent > 100)
                {
                    return; // درصدها خارج از محدوده مجاز هستند، عملیات متوقف می‌شود
                }

                var nettings = await _unitOfWork.FactorNettingProcessItemRepository.GetAll(factorId);
                var defaultNettings = nettings.Where(x => x.NettingProcessTypes != 0);
                var FactorServiceExplenationsTotalAmount = serviceExplanation.Sum(s => s.TotalAmount);
                foreach (var item in defaultNettings)
                {
                    try
                    {
                        var entity = new FactorNettingProcessItem();
                        entity.Id = item.Id;
                        entity.FactorId = item.FactorId;
                        entity.IsDeduction = item.IsDeduction;
                        entity.InsertDate = item.InsertDate;
                        entity.Title = item.Title;
                        entity.InsertBy = item.InsertBy;
                        entity.IsEditable = item.IsEditable;
                        entity.NettingProcessTypes = item.NettingProcessTypes;
                        switch (item.NettingProcessTypes)
                        {
                            case NettingProcessTypesEnum.insurance:
                                entity.Percentage = factorFinancialDetaile.FactorInsurance_Percent;
                                entity.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount : 0) * (factorFinancialDetaile.FactorInsurance_Percent / 100));
                                break;
                            case NettingProcessTypesEnum.addedValue:
                                entity.Percentage = factorFinancialDetaile.FactorValue_Added_Percent;
                                entity.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount : 0) * (factorFinancialDetaile.FactorValue_Added_Percent / 100));
                                break;
                            case NettingProcessTypesEnum.Tax:
                                entity.Percentage = factorFinancialDetaile.FactorTax_Percent;
                                entity.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount : 0) * (factorFinancialDetaile.FactorTax_Percent / 100));
                                break;
                            case NettingProcessTypesEnum.GoodJob:
                                entity.Percentage = factorFinancialDetaile.GoodJob_Percent;
                                entity.Amount = ((FactorServiceExplenationsTotalAmount != null ? (decimal)FactorServiceExplenationsTotalAmount : 0) * (factorFinancialDetaile.GoodJob_Percent / 100));
                                break;
                        }
                        await _unitOfWork.FactorNettingProcessItemRepository.Update(entity);
                    }
                    catch (Exception ex)
                    {
                        _errorLoggerService.SaveError(ex);
                        continue;
                    }
                }
                //await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
    }
}
