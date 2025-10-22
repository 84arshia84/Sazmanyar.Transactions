using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class PrePaymentDepreciationService : IPrePaymentDepreciationService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        public PrePaymentDepreciationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
        }
        public async Task<bool> Add(PrePaymentDepreciation paymentDepreciation)
        {
            try
            {
                return await _unitOfWork.PaymentDepreciationRepository.Add(paymentDepreciation);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
        /// <summary>
        /// نمایش مقدار پیش نهاد شده برای این صورت وضعیت
        /// </summary>
        /// <param name="invoiceBaseInformationId"></param>
        /// <returns></returns>
        public async Task<decimal> CalculatingNetProccessForPrepayment(Guid invoiceBaseInformationId, Guid currencyId)
        {
            try
            {
                decimal resultForShow = 0;
                var depreciations = await _unitOfWork.PaymentDepreciationRepository.GetAllByInvoiceId(invoiceBaseInformationId);
                depreciations = depreciations.Where(x => x.CurrencyId == currencyId).ToList();
                resultForShow = depreciations.Sum(x => x.ApprovedDepreciationAmount);
                return resultForShow;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return 0;
            }
        }
        public async Task migrateData()
        {
            try
            {
                var connectionString = _configuration["ConnectionStrings:DbConnection"];
                var ids = await _unitOfWork.PaymentDepreciationRepository.guids(connectionString);
                if (ids != null)
                {
                    if(ids.Any())
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            await CalculatingSugestedPrepayment(ids[i]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// هسته ی اصلی محاسبه مقادیر پیشنهادی پیش پرداخت برای هر شرح خدمت
        /// در این تابع چند مقدار گرفته می شود 
        /// </summary>
        /// <param name="ServiceExplenationFinancialId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CalculatingSugestedPrepayment(Guid ServiceExplenationFinancialId)
        {
            try
            {
                /// 1- شرح خدمت 
                /// 2- شرح خدمتی که توی صورت وضعیت مقدار دارد
                /// 3- خود صورت وضعیت
                /// 4- لیستی از پیش پرداخت های استهلاک شده قبل از این استهلاک


                var calculatedValue = new PrePaymentDepreciation();
                var model = await _unitOfWork.PaymentDepreciationRepository.GetByFinancialId(ServiceExplenationFinancialId);
                var financialData = await _unitOfWork.ServiceExplanationFinancialRepository.Get(ServiceExplenationFinancialId);
                var serviceExplenationData = await _unitOfWork.ServiceExplanationRepository.Get(financialData.ServiceExplanationId);
                var invoiceData = await _unitOfWork.InvoiceBaseInformationRepository.Get(financialData.InvoiceBaseInformationId);
                if (!(invoiceData.InvoiceTypeId == InvoiceTypesEnum.temporary.GetGuid() ||
                      invoiceData.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid())
                   )
                {
                    return;
                }
                var DepreciationBefor = invoiceData.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid() ? await _unitOfWork.PaymentDepreciationRepository.GetAllForContract(invoiceData.ContractId)
                    : await _unitOfWork.PaymentDepreciationRepository.GetAllByServiceExplenationId(financialData.ServiceExplanationId);
                if (DepreciationBefor.Count > 0)
                {
                    DepreciationBefor = DepreciationBefor.Where(x => x.DepreciationDate < (model != null ? model.DepreciationDate : DateTime.Now)).ToList();
                }
                /// 5- تمام صورت وضعیت های از نوع پیش پرداخت که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allPrepaymentFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.prepayment.GetGuid(), invoiceData.InsertDate);

                /// 6- تمام صورت وضعیت های از نوع موقت که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allTemporaryFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.temporary.GetGuid(), invoiceData.InsertDate);

                /// 7- تمام صورت وضعیت های از نوع تعدیل که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allAdjustmentFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.adjustment.GetGuid(), invoiceData.InsertDate);

                /// 8- تمام صورت وضعیت های از نوع تعلیق که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allSuspensionFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.suspension.GetGuid(), invoiceData.InsertDate);

                /// 9- تمام صورت وضعیت های از نوع قطعی که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allDefiniteFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.definite.GetGuid(), invoiceData.InsertDate);

                /// 10- تمام صورت وضعیت های از نوع توقف که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                var allPauseFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.pause.GetGuid(), invoiceData.InsertDate);

                if (allPrepaymentFinancialBefor.Count == 0)
                {
                    return;
                }
                /// نحوه محاسبات
                var sumApprovedBefor = allPrepaymentFinancialBefor.Sum(x => x.ApprovedPrice);

                var sumDepreciantionBefor = DepreciationBefor.Count > 0 ? DepreciationBefor.Sum(x => x.ApprovedDepreciationAmount) : 0;

                /// 11- جمع تمامی مبالغ مستهلک شده این شرح خدمت تاکنون به جز از نوع عالی‌الحساب و پیش پرداخت
                var sumApprovedBeforFinancial =
                    allTemporaryFinancialBefor.Count > 0 ? allTemporaryFinancialBefor.Sum(x => x.ApprovedPrice) : 0 +
                    allAdjustmentFinancialBefor.Count > 0 ? allAdjustmentFinancialBefor.Sum(x => x.ApprovedPrice) : 0 +
                    allSuspensionFinancialBefor.Count > 0 ? allSuspensionFinancialBefor.Sum(x => x.ApprovedPrice) : 0 +
                    allDefiniteFinancialBefor.Count > 0 ? allDefiniteFinancialBefor.Sum(x => x.ApprovedPrice) : 0 +
                    allPauseFinancialBefor.Count > 0 ? allPauseFinancialBefor.Sum(x => x.ApprovedPrice) : 0
                    ;

                /// <summary>
                /// Calculates the suggested depreciation amount using the formula:
                /// ((sum of previously approved amounts - sum of all previously approved depreciation amounts) * current approved price) / total amount - sum of all previously approved depreciation amounts of this serviceExplenation 
                /// </summary>
                /// <remarks>
                /// تمام مبالغ تایید شده این شرح خدمت تا کنون در صورت وضعیت پیش پرداخت  منهای مبالغ استهلاک شده قبل ضرب در مبلغ تایید شده الان تقسیم بر مبلغ کل شرح خدمت منهای تمام مبلغ مستهلک شده از قبل این شرح خدمت
                /// </remarks>
                if (invoiceData.InvoiceTypeId == InvoiceTypesEnum.temporary.GetGuid())
                {
                    calculatedValue.SuggestedDepreciationAmount = ((sumApprovedBefor - sumDepreciantionBefor) * financialData.ApprovedPrice) / ((decimal)serviceExplenationData.TotalAmount - sumApprovedBeforFinancial);
                }
                if (invoiceData.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid())
                {
                    calculatedValue.SuggestedDepreciationAmount = (sumApprovedBefor - sumDepreciantionBefor);
                }
                /// <summary>
                /// Sets the approved depreciation amount equal to the suggested amount
                /// </summary>
                /// <remarks>
                /// نکته : تا زمانی که کاربر در تب خالص سازی مبلغ پبشنهادی پیش پرداخت را تغییر ندهد ، مبلغ پیشنهادی با مبلغ تاییدی کاربر یکی می شود
                /// </remarks>
                calculatedValue.ApprovedDepreciationAmount = calculatedValue.SuggestedDepreciationAmount;

                calculatedValue.Id = model != null ? model.Id : Guid.NewGuid();
                calculatedValue.ServiceExplenationId = financialData.ServiceExplanationId;
                calculatedValue.ServiceExplenationFinancialId = financialData.Id;
                calculatedValue.InvoiceId = financialData.InvoiceBaseInformationId;
                calculatedValue.DepreciationDate = model != null ? model.DepreciationDate : DateTime.Now;
                calculatedValue.IsDeleted = false;
                calculatedValue.CurrencyId = serviceExplenationData.CurrencyID;
                if (model == null)
                {
                    await Add(calculatedValue);
                    await _unitOfWork.Save();
                    return;
                }
                await Update(calculatedValue);
                await _unitOfWork.Save();
                return;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public Task<PrePaymentDepreciation> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PrePaymentDepreciation>> GetAllByFinancialId(Guid financialId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PrePaymentDepreciation>> GetAllByInvoiceId(Guid invoiceId)
        {
            try
            {
                return await _unitOfWork.PaymentDepreciationRepository.GetAllByInvoiceId(invoiceId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
        /// <summary>
        /// تاثیر مبلغ در خواستی کاربر بر باقی موارد استهلاک شده این صورت وضعیت
        /// </summary>
        /// <param name="invoiceBaseInformationId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> TheImpactOfTheUserAmountOnTheRemainingDepreciation(Guid invoiceBaseInformationId, decimal value)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(invoiceBaseInformationId);
                var depreciantions = invoice.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid() ? await _unitOfWork.PaymentDepreciationRepository.GetAllForContract(invoice.ContractId)
                    : await GetAllByInvoiceId(invoiceBaseInformationId);
                var financials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceBaseInformationId);
                var allPrepaymentFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    depreciantions.Select(x => x.ServiceExplenationId).ToList(), InvoiceTypesEnum.prepayment.GetGuid(), invoice.InsertDate);
                //var SumOfFinacials = financials.Sum(x => x.ApprovedPrice);
                var SumOfAllPrePyament = allPrepaymentFinancialBefor.Sum(x => x.ApprovedPrice);
                /// سقف استهلاک
                decimal DepreciationCeiling = SumOfAllPrePyament - depreciantions.Sum(x => x.ApprovedDepreciationAmount);
                if (value > DepreciationCeiling)
                {
                    return false;
                }
                /// در این جا میایم مقداره وارد شده توسط کاربر را روی مبالغ تایید شده تمامی شرح خدمات تقسیم می کنیم تا یک نرخی به دست بیاد
                /// بعد میام توی تمامی مبلغ تایید جهت استهلاک ضرب می کنم تا همه آن ها جمعشان برابر مبلغ وارد شده توسط کاربر بشود
                var sumOfDepreciation = depreciantions.Sum(x => x.ApprovedDepreciationAmount);
                decimal ratio = value / sumOfDepreciation;
                foreach (var item in depreciantions)
                {
                    var financial = financials.FirstOrDefault(x => x.Id == item.ServiceExplenationFinancialId);
                    if (financial != null)
                    {
                        item.ApprovedDepreciationAmount = item.ApprovedDepreciationAmount * ratio;
                        await Update(item);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }

        public async Task<bool> Update(PrePaymentDepreciation prePaymentDepreciation)
        {
            try
            {
                return await _unitOfWork.PaymentDepreciationRepository.Update(prePaymentDepreciation);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
