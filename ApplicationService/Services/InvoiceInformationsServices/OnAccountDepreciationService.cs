using AppCore.Entities.InvoiceInformations.OnAccountDepreciations;
using AppCore.Entities.InvoiceInformations.PrePaymentDepreciations;
using AppCore.Enums;
using AppCore.UnitOfWork;
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
    internal class OnAccountDepreciationService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IConfiguration _configuration;
        public OnAccountDepreciationService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService,
            IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _configuration = configuration;
        }
        public async Task<bool> Add(OnAccountDepreciation onAccountDepreciation)
        {
            try
            {
                return await _unitOfWork.OnAccountDepreciationRepository.Add(onAccountDepreciation);
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
        public async Task<decimal> CalculatingNetProccessForOnAccount(Guid invoiceBaseInformationId, Guid currencyId)
        {
            try
            {
                decimal resultForShow = 0;
                var depreciations = await _unitOfWork.OnAccountDepreciationRepository.GetAllByInvoiceId(invoiceBaseInformationId);
                depreciations = depreciations.Where(d => d.CurrencyId == currencyId).ToList();
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
                    if (ids.Any())
                    {
                        for (int i = 0; i < ids.Count; i++)
                        {
                            await CalculatingSugestedOnAccount(ids[i]);
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
        /// هسته ی اصلی محاسبه مقادیر پیشنهادی علی الحساب برای هر شرح خدمت
        /// در این تابع چند مقدار گرفته می شود 
        /// </summary>
        /// <param name="ServiceExplenationFinancialId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task CalculatingSugestedOnAccount(Guid ServiceExplenationFinancialId)
        {
            try
            {
                /// 1- شرح خدمت 
                /// 2- شرح خدمتی که توی صورت وضعیت مقدار دارد
                /// 3- خود صورت وضعیت
                /// 4- لیستی از علی الحساب های استهلاک شده قبل از این استهلاک
                /// 5- تمام صورت وضعیت های از نوع استهلاک که این شرح خدمت توی آن ها بوده ، مقدار تایید شده ی این شرح خدمت
                /// نحوه محاسبات
                var calculatedValue = new OnAccountDepreciation();
                var model = await _unitOfWork.OnAccountDepreciationRepository.GetByFinancialId(ServiceExplenationFinancialId);
                var financialData = await _unitOfWork.ServiceExplanationFinancialRepository.Get(ServiceExplenationFinancialId);
                // var serviceExplenationData = await _unitOfWork.ServiceExplanationRepository.Get(financialData.ServiceExplanationId);
                var invoiceData = await _unitOfWork.InvoiceBaseInformationRepository.Get(financialData.InvoiceBaseInformationId);
                if (!(invoiceData.InvoiceTypeId == InvoiceTypesEnum.temporary.GetGuid() ||
                      invoiceData.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid())
                   )
                {
                    return;
                }
                var DepreciationBefor = invoiceData.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid() ? await _unitOfWork.OnAccountDepreciationRepository.GetAllForContract(invoiceData.ContractId)
                    : await _unitOfWork.OnAccountDepreciationRepository.GetAllByServiceExplenationId(financialData.ServiceExplanationId);
                if (DepreciationBefor.Count > 0)
                {
                    DepreciationBefor = DepreciationBefor.Where(x => x.DepreciationDate < (model != null ? model.DepreciationDate : DateTime.Now)).ToList();
                }

                var allOnAccountFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    financialData.ServiceExplanationId, InvoiceTypesEnum.onAccount.GetGuid(), invoiceData.InsertDate);

                if (allOnAccountFinancialBefor.Count == 0)
                {
                    return;
                }

                var sumApprovedBefor = allOnAccountFinancialBefor.Sum(x => x.ApprovedPrice);

                var sumDepreciantionBefor = DepreciationBefor.Count > 0 ? DepreciationBefor.Sum(x => x.ApprovedDepreciationAmount) : 0;

                /// <summary>
                /// Calculates the suggested depreciation amount using the formula:
                /// ((sum of previously approved amounts - sum of all previously approved depreciation amounts) * current approved price) / total amount 
                /// </summary>
                /// <remarks>
                /// تمام مبالغ تایید شده این شرح خدمت تا کنون  منهای مبالغ استهلاک شده قبل ضرب در مبلغ تایید شده الان تقسیم بر مبلغ کل شرح خدمت
                /// منهای تمام مبالغ استهلاک شده تایید شده تا کنون
                /// </remarks>
                calculatedValue.SuggestedDepreciationAmount = sumApprovedBefor - sumDepreciantionBefor;//* financialData.ApprovedPrice) ;// / (decimal)serviceExplenationData.TotalAmount);

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
                calculatedValue.CurrencyId = financialData.CurrencyId != null ? (Guid)financialData.CurrencyId : Guid.Empty;
                calculatedValue.IsDeleted = false;
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

        public Task<OnAccountDepreciation> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OnAccountDepreciation>> GetAllByFinancialId(Guid financialId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OnAccountDepreciation>> GetAllByInvoiceId(Guid invoiceId)
        {
            try
            {
                return await _unitOfWork.OnAccountDepreciationRepository.GetAllByInvoiceId(invoiceId);
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
                var depreciantions = invoice.InvoiceTypeId == InvoiceTypesEnum.definite.GetGuid() ? await _unitOfWork.OnAccountDepreciationRepository.GetAllForContract(invoice.ContractId)
                    : await GetAllByInvoiceId(invoiceBaseInformationId);
                var financials = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceBaseInformationId);
                var allOnAcountFinancialBefor = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceType(
                    depreciantions.Select(x => x.ServiceExplenationId).ToList(), InvoiceTypesEnum.onAccount.GetGuid(), invoice.InsertDate);
                //var SumOfFinacials = financials.Sum(x => x.ApprovedPrice);
                var SumOfAllOnAccount = allOnAcountFinancialBefor.Sum(x => x.ApprovedPrice);
                /// سقف استهلاک
                decimal DepreciationCeiling = SumOfAllOnAccount - depreciantions.Sum(x => x.ApprovedDepreciationAmount);
                if (value > DepreciationCeiling)
                {
                    return false;
                }
                var sumOfDepreciation = depreciantions.Sum(x => x.ApprovedDepreciationAmount);
                decimal ratio = value / sumOfDepreciation;

                foreach (var item in depreciantions)
                {
                    var financial = financials.FirstOrDefault(x => x.Id == item.ServiceExplenationFinancialId);
                    if (financial != null)
                    {
                        //  decimal ratio = (value * financial.ApprovedPrice) / SumOfFinacials;
                        //    if (value > item.ApprovedDepreciationAmount)
                        //  {
                        item.ApprovedDepreciationAmount = item.ApprovedDepreciationAmount * ratio;
                        await Update(item);
                        //     continue;
                        //   }
                        //  item.ApprovedDepreciationAmount -= ratio;
                        //  await Update(item);
                        //  continue;
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

        public async Task<bool> Update(OnAccountDepreciation onAccountDepreciation)
        {
            try
            {
                return await _unitOfWork.OnAccountDepreciationRepository.Update(onAccountDepreciation);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
