using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.NettingProcesses;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.Entities.SettingEntities.Currencies;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.Calculators.InvoiceInformationsCalculators;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class NettingProcessItemService : INettingProcessItemService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IPrePaymentDepreciationService _prePaymentDepreciationService;
        private IAccountService _onAccountDepreciationService;
        public NettingProcessItemService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService, IPrePaymentDepreciationService prePaymentDepreciationService,
            IAccountService onAccountDepreciationService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _prePaymentDepreciationService = prePaymentDepreciationService;
            _onAccountDepreciationService = onAccountDepreciationService;
        }
        public async Task<(string message, bool isSuccess)> AddNettingProcessItem(AddNettingProcessItemDto addNettingProcessItemDto, LoginUserDto userDto)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (addNettingProcessItemDto.Percentage < 0 || addNettingProcessItemDto.Percentage > 100)
                {
                    return ("درصد کسورات باید بین 0 تا 100 باشد", false);
                }

                // بررسی اینکه مجموع پیش‌پرداخت از مجموع کسورات بیشتر نباشد
                var existingItems = await GetAllNettingProcessItems(addNettingProcessItemDto.InvoiceBaseInformationId);
                var totalDeductions = existingItems.Where(x => x.IsDeduction && x.CurrencyId == addNettingProcessItemDto.CurrencyId).Sum(x => x.Percentage);
                var totalPrepayments = existingItems.Where(x => x.Title.Contains("پیش پرداخت") && x.CurrencyId == addNettingProcessItemDto.CurrencyId).Sum(x => x.Percentage);

                if (addNettingProcessItemDto.IsDeduction && addNettingProcessItemDto.Title.Contains("پیش پرداخت"))
                {
                    totalPrepayments += addNettingProcessItemDto.Percentage;
                }
                else if (addNettingProcessItemDto.IsDeduction)
                {
                    totalDeductions += addNettingProcessItemDto.Percentage;
                }

                if (totalPrepayments > totalDeductions)
                {
                    return ("مجموع پیش‌پرداخت نمی‌تواند از مجموع کسورات بیشتر باشد", false);
                }

                // بررسی محدودیت پیش‌پرداخت بر اساس شرح خدمات قرارداد
                if (addNettingProcessItemDto.IsDeduction && addNettingProcessItemDto.Title.Contains("پیش پرداخت"))
                {
                    var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(addNettingProcessItemDto.InvoiceBaseInformationId);
                    if (invoice?.ContractId != null)
                    {
                        var serviceExplanations = await _unitOfWork.ServiceExplanationRepository.GetAll(invoice.ContractId);
                        var maxPrepaymentPercentage = serviceExplanations.Sum(se => se.PrepaymentPercentage);

                        if (addNettingProcessItemDto.Percentage > maxPrepaymentPercentage)
                        {
                            return ($"درصد پیش‌پرداخت نمی‌تواند از {maxPrepaymentPercentage}% (حداکثر مجاز در قرارداد) بیشتر باشد", false);
                        }
                    }
                }

                // بررسی محدودیت علی‌الحساب - معمولاً علی‌الحساب محدودیت خاصی ندارد اما می‌تواند بر اساس قوانین کسب‌وکار تنظیم شود
                if (addNettingProcessItemDto.IsDeduction && addNettingProcessItemDto.Title.Contains("علی الحساب"))
                {
                    // در اینجا می‌توان محدودیت خاصی برای علی‌الحساب تعریف کرد
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                var entity = NettingProcessItemMapper.DtoToEntity(addNettingProcessItemDto, userDto, _errorLoggerService);
                await _unitOfWork.NettingProcessItemRepository.Add(entity);
                return ("ثبت  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ثبت با خطا مواجه شد.", false);
            }
        }
        public async Task AddDefaultNettingProcess(CUDSERequestedFinancialDto cUDSERequestedFinancialDto, ServiceExplanationDto serviceExplanation, LoginUserDto userDto)
        {
            try
            {
                var releventInvoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(cUDSERequestedFinancialDto.InvoiceBaseInformationId);

                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                var contractFinancial = releventInvoice.Contract.ContractFinancialDetails;
                if (contractFinancial.ContractInsurance_Percent < 0 || contractFinancial.ContractInsurance_Percent > 100 ||
                    contractFinancial.ContractValue_Added_Percent < 0 || contractFinancial.ContractValue_Added_Percent > 100 ||
                    contractFinancial.ContractTax_Percent < 0 || contractFinancial.ContractTax_Percent > 100 ||
                    contractFinancial.GoodJob_Percent < 0 || contractFinancial.GoodJob_Percent > 100)
                {
                    return; // درصدها خارج از محدوده مجاز هستند، عملیات متوقف می‌شود
                }

                var DefaultNettingCount = 1;
                var allNPItems = await GetAllNettingProcessItems(cUDSERequestedFinancialDto.InvoiceBaseInformationId);
                if (allNPItems.Count == 0 || allNPItems.All(x => x.CurrencyId != serviceExplanation.CurrencyID))
                {
                    while (DefaultNettingCount <= 6)
                    {
                        var NettedValue = new AddNettingProcessItemDto();
                        NettedValue.Key = Guid.Empty;
                        NettedValue.InvoiceBaseInformationId = cUDSERequestedFinancialDto.InvoiceBaseInformationId;
                        NettedValue.Amount = 0;
                        switch (DefaultNettingCount)
                        {
                            case 1:
                                NettedValue.Title = "پیش پرداخت";
                                NettedValue.Percentage = 0;
                                NettedValue.IsDeduction = true;
                                break;
                            case 2:
                                NettedValue.Title = "علی الحساب";
                                NettedValue.Percentage = 0;
                                NettedValue.IsDeduction = true;
                                break;
                            case 3:
                                NettedValue.Title = "بیمه";
                                NettedValue.Percentage = releventInvoice.Contract.ContractFinancialDetails.ContractInsurance_Percent;
                                NettedValue.IsDeduction = true;
                                break;
                            case 4:
                                NettedValue.Title = "ارزش افزوده";
                                NettedValue.Percentage = releventInvoice.Contract.ContractFinancialDetails.ContractValue_Added_Percent;
                                NettedValue.IsDeduction = false;
                                break;
                            case 5:
                                NettedValue.Title = "مالیات";
                                NettedValue.Percentage = releventInvoice.Contract.ContractFinancialDetails.ContractTax_Percent;
                                NettedValue.IsDeduction = true;
                                break;
                            case 6:
                                NettedValue.Title = "حسن انجام کار";
                                NettedValue.Percentage = releventInvoice.Contract.ContractFinancialDetails.GoodJob_Percent;
                                NettedValue.IsDeduction = true;
                                break;
                        }
                        NettedValue.IsEditable = false;
                        NettedValue.NettingProcessType = DefaultNettingCount;
                        NettedValue.CurrencyId = serviceExplanation.CurrencyID;
                        await AddNettingProcessItem(NettedValue, userDto);
                        DefaultNettingCount++;

                    }
                }
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
        public async Task<(string message, bool isSuccess)> DeleteNettingProcessItem(Guid nettingProcessItemId, LoginUserDto userDto)
        {
            try
            {
                await _unitOfWork.NettingProcessItemRepository.Delete(nettingProcessItemId);
                return ("حذف  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("حذف با خطا مواجه شد.", false);
            }
        }
        public async Task<List<GetAllNettingProcessItemsDto>> GetAllNettingProcessItems(Guid invoiceBaseInformationId)
        {
            try
            {
                var entities = await _unitOfWork.NettingProcessItemRepository.GetAll(invoiceBaseInformationId);
                var results = NettingProcessItemMapper.EntitiesToDtos(entities, _errorLoggerService);
                foreach (var item in results)
                {
                    if (item.NettingProcessType == (int)NettingProcessTypesEnum.prePayment)
                    {
                        item.Amount = await _prePaymentDepreciationService.CalculatingNetProccessForPrepayment(invoiceBaseInformationId, (Guid)item.CurrencyId);
                    }
                    if (item.NettingProcessType == (int)NettingProcessTypesEnum.onAccount)
                    {
                        item.Amount = await _onAccountDepreciationService.CalculatingNetProccessForOnAccount(invoiceBaseInformationId, (Guid)item.CurrencyId);
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<GetAllNettingProcessItemsDto>();
            }
        }


        public async Task<(string message, bool isSuccess)> UpdateNettingProcessItem(UpdateNettingProcessItemDto dto, LoginUserDto userDto)
        {
            try
            {
                // اعمال محدودیت منطقی روی درصد کسورات (حداکثر 100٪ و حداقل 0%)
                if (dto.Percentage < 0 || dto.Percentage > 100)
                {
                    return ("درصد کسورات باید بین 0 تا 100 باشد", false);
                }

                var model = await _unitOfWork.NettingProcessItemRepository.Get(dto.Key);
                if (model == null || model.Id == Guid.Empty)
                    return ("آیتم موردنظر یافت نشد.", false);

                // بررسی اینکه مجموع پیش‌پرداخت از مجموع کسورات بیشتر نباشد
                var existingItems = await GetAllNettingProcessItems(model.InvoiceBaseInformationId);
                var totalDeductions = existingItems.Where(x => x.IsDeduction && x.CurrencyId == dto.CurrencyId && x.Key != dto.Key).Sum(x => x.Percentage);
                var totalPrepayments = existingItems.Where(x => x.Title.Contains("پیش پرداخت") && x.CurrencyId == dto.CurrencyId && x.Key != dto.Key).Sum(x => x.Percentage);

                if (model.IsDeduction && model.Title.Contains("پیش پرداخت"))
                {
                    totalPrepayments += dto.Percentage;
                }
                else if (model.IsDeduction)
                {
                    totalDeductions += dto.Percentage;
                }

                if (totalPrepayments > totalDeductions)
                {
                    return ("مجموع پیش‌پرداخت نمی‌تواند از مجموع کسورات بیشتر باشد", false);
                }

                // بررسی محدودیت پیش‌پرداخت بر اساس شرح خدمات قرارداد
                if (model.IsDeduction && model.Title.Contains("پیش پرداخت"))
                {
                    var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(model.InvoiceBaseInformationId);
                    if (invoice?.ContractId != null)
                    {
                        var serviceExplanations = await _unitOfWork.ServiceExplanationRepository.GetAll(invoice.ContractId);
                        var maxPrepaymentPercentage = serviceExplanations.Sum(se => se.PrepaymentPercentage);

                        if (dto.Percentage > maxPrepaymentPercentage)
                        {
                            return ($"درصد پیش‌پرداخت نمی‌تواند از {maxPrepaymentPercentage}% (حداکثر مجاز در قرارداد) بیشتر باشد", false);
                        }
                    }
                }

                // بررسی محدودیت علی‌الحساب - معمولاً علی‌الحساب محدودیت خاصی ندارد اما می‌تواند بر اساس قوانین کسب‌وکار تنظیم شود
                if (model.IsDeduction && model.Title.Contains("علی الحساب"))
                {
                    // در اینجا می‌توان محدودیت خاصی برای علی‌الحساب تعریف کرد
                    // فعلاً محدودیت 100% اعمال می‌شود که قبلاً چک شده
                }

                // 2) اعمال تغییرات کاربر
                model.Title = dto.Title;
                model.Percentage = dto.Percentage;
                model.Amount = dto.Amount;
                model.CurrencyId = dto.CurrencyId;

                // 3) مسیر ویژه‌ی پیش‌پرداخت
                if (model.NettingProcessTypes == NettingProcessTypesEnum.prePayment)
                {
                    var invoiceId = dto.InvoiceId ?? model.InvoiceBaseInformationId;
                    var ok = await _prePaymentDepreciationService
                        .TheImpactOfTheUserAmountOnTheRemainingDepreciation(invoiceId, model.Amount);
                    if (!ok)
                        return ("مبلغ وارد شده از سقف مبالغ استهلاک پیش‌پرداخت بیشتر است.", false);

                }
                // 4) مسیر ویژه‌ی علی‌الحساب
                else if (model.NettingProcessTypes == NettingProcessTypesEnum.onAccount)
                {
                    var invoiceId = dto.InvoiceId ?? model.InvoiceBaseInformationId;
                    var ok = await _onAccountDepreciationService
                        .TheImpactOfTheUserAmountOnTheRemainingDepreciation(invoiceId, model.Amount);
                    if (!ok)
                        return ("مبلغ وارد شده از سقف مبالغ علی‌الحساب بیشتر است.", false);
                }

                await _unitOfWork.Save();
                return ("ویرایش با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا مواجه شد.", false);
            }
        }

        public async Task<List<RequestAmountGetDto>> GetInvoiceRequestedPriceSummation(Guid invoiceBaseInformationId)
        {
            try
            {
                var AllSEFinancials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceBaseInformationId);
                var result = NettingProcessItemCalculator.CalculateRequestedPriceSummation(AllSEFinancials);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<RequestAmountGetDto>();
            }
        }
        public async Task<List<ApprovedAmountGetDto>> GetInvoiceApprovedPriceSummation(Guid invoiceBaseInformationId)
        {
            try
            {
                var AllSEFinancials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceBaseInformationId);
                var result = NettingProcessItemCalculator.CalculateApprovedPriceSummation(AllSEFinancials);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<ApprovedAmountGetDto>();
            }
        }
        public async Task<List<RequestAmountGetDto>> GetContractInvoicesRequestedPriceSummation(Guid invoiceBaseInformationId)
        {
            try
            {
                var AllSEFinancials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllBeforThisInvoiceBaseInformationId(invoiceBaseInformationId);
                var result = NettingProcessItemCalculator.CalculateRequestedPriceSummation(AllSEFinancials);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<RequestAmountGetDto>();
            }
        }
        public async Task<List<ApprovedAmountGetDto>> GetContractInvoicesApprovedPriceSummation(Guid invoiceBaseInformationId)
        {
            try
            {
                var AllSEFinancials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllBeforThisInvoiceBaseInformationId(invoiceBaseInformationId);
                var result = NettingProcessItemCalculator.CalculateApprovedPriceSummation(AllSEFinancials);
                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<ApprovedAmountGetDto>();
            }
        }
        public async Task<List<NettedAmountGetDto>> GetInvoiceNetPrice(Guid invoiceBaseInformationId)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(invoiceBaseInformationId);
                var serviceExpalanationFinancial = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceBaseInformationId);
                var allNPItems = await GetAllNettingProcessItems(invoiceBaseInformationId);
                var invoiceApprovedPriceSummation = await GetInvoiceApprovedPriceSummation(invoiceBaseInformationId);
                if (invoice.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid() ||
                   invoice.InvoiceTypeId == InvoiceTypesEnum.onAccount.GetGuid())
                {

                    return invoiceApprovedPriceSummation
                                  .Select(g => new NettedAmountGetDto
                                  {
                                      Currency = g.Currency,
                                      NettedAmount = g.ApproveAmount
                                  })
                                  .ToList();
                }
                var AllNettedAmount = new List<NettedAmountGetDto>();
                foreach (var item in invoiceApprovedPriceSummation)
                {
                    var NettedAmount = new NettedAmountGetDto();
                    NettedAmount.Currency = item.Currency;
                    /// این جا کسورات و اضافات به جز پیش پرداخت و علی‌الحساب اعمال می شود
                    NettedAmount.NettedAmount = NettingProcessItemCalculator.CalculateNetPrice(item.ApproveAmount, allNPItems, (Guid)item.Currency);
                    if(invoice.InvoiceTypeId != InvoiceTypesEnum.adjustment.GetGuid())
                        NettedAmount.NettedAmount -= await _prePaymentDepreciationService.CalculatingNetProccessForPrepayment(invoiceBaseInformationId, (Guid)item.Currency);
                        NettedAmount.NettedAmount -= await _onAccountDepreciationService.CalculatingNetProccessForOnAccount(invoiceBaseInformationId, (Guid)item.Currency);
                    AllNettedAmount.Add(NettedAmount);

                }
                return AllNettedAmount;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<NettedAmountGetDto>();
            }
        }
        public async Task<List<NettedAmountGetDto>> GetNetPriceUnitlThisInvoice(Guid invoiceBaseInformationId)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(invoiceBaseInformationId);
                var allNPItems = await GetAllNettingProcessItems(invoiceBaseInformationId);
                var invoiceApprovedPriceSummation = await GetContractInvoicesApprovedPriceSummation(invoiceBaseInformationId);
                if (invoice.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid() ||
                   invoice.InvoiceTypeId == InvoiceTypesEnum.onAccount.GetGuid())
                {
                    return invoiceApprovedPriceSummation.Select(g => new NettedAmountGetDto
                    {
                        Currency = g.Currency,
                        NettedAmount = g.ApproveAmount
                    }).ToList();
                }
                var AllNettedAmount = new List<NettedAmountGetDto>();
                foreach (var item in invoiceApprovedPriceSummation)
                {
                    var NettedAmount = new NettedAmountGetDto();
                    NettedAmount.Currency = item.Currency;
                    NettedAmount.NettedAmount = NettingProcessItemCalculator.CalculateNetPrice(item.ApproveAmount, allNPItems, (Guid)item.Currency);
                    AllNettedAmount.Add(NettedAmount);
                }
                return AllNettedAmount;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<NettedAmountGetDto>();
            }
        }

    }
}
