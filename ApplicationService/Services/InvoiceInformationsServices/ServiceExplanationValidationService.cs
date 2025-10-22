using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal static class ServiceExplanationValidationService
    {
        /// <summary>
        /// این تابع در صد وارد شده برای صورت وضعیت از نوع پیش پرداخت
        /// با درصد پیش پرداخت شرح خدمت را بررسی می کند
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="serviceExplanationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<bool> CheckPrePaymentPercent(Guid invoiceId, Guid serviceExplanationId, decimal request, IUnitOfWork _unitOfWork, IErrorLoggerService _errorLoggerService)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(invoiceId);
                var serviceExplenation = await _unitOfWork.ServiceExplanationRepository.Get(serviceExplanationId);
                if (invoice != null)
                {
                    if (invoice.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid())
                    {
                        return request > serviceExplenation.PrepaymentPercentage ? false : true;
                    }
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
        /// <summary>
        /// بررسی مقدار درخواستی کاربر با مجموع مقادیر قبلی
        /// </summary>
        /// <param name="serviceExplanationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<bool> CheckMaxPercentThatFinancialHasAccess(Guid invoiceId, Guid serviceExplanationId, decimal request, IUnitOfWork _unitOfWork, IErrorLoggerService _errorLoggerService)
        {
            try
            {
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(invoiceId);
                var allInvoices = await _unitOfWork.InvoiceBaseInformationRepository.GetAllByContractId(invoice.ContractId);
                var serviceExplanationFinancial = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByServiceExplanationId(serviceExplanationId);
                if(invoice.InvoiceTypeId == InvoiceTypesEnum.adjustment.GetGuid())
                {
                    return true;
                }
                if (serviceExplanationFinancial.Count > 0)
                {
                    if (invoice.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid())
                    {
                        var serviceExplenation = await _unitOfWork.ServiceExplanationRepository.Get(serviceExplanationId);
                        if (serviceExplenation != null)
                        {
                            //var prePaymentInvoice = 
                            //var SumPrePaymentPriorAmounts = serviceExplenation.PrepaymentPercentage - serviceExplanationFinancial.Where(x=> allInvoices.Contains(x.InvoiceBaseInformationId))
                            //    .Sum(x => x.RequestedPercent);
                            //var sumPrePaymentPriorAmounts = serviceExplenation.PrepaymentPercentage
                            //                                - serviceExplanationFinancial
                            //                                .Where(x => allInvoices.Contains(x.InvoiceBaseInformationId)
                            //                                && x.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid())
                            //                                .Sum(x => x.RequestedPercent);
                            var SumPrePaymentPriorAmounts = serviceExplenation.PrepaymentPercentage
                                - serviceExplanationFinancial
                                  .Where(x => allInvoices
                                       .Any(inv => inv.Id == x.InvoiceBaseInformationId
                                            && inv.InvoiceTypeId == InvoiceTypesEnum.prepayment.GetGuid()
                                            && inv.Id!= invoiceId))
                                  .Sum(x => x.RequestedPercent);
                            if (SumPrePaymentPriorAmounts < request)
                            {
                                return false;
                            }
                            return true;
                        }
                        return false;
                    }
                    //var m =serviceExplanationFinancial
                    //              .Where(x => allInvoices
                    //                   .Any(inv => inv.Id == x.InvoiceBaseInformationId
                    //                        && (inv.InvoiceTypeId != InvoiceTypesEnum.prepayment.GetGuid() && inv.InvoiceTypeId != InvoiceTypesEnum.onAccount.GetGuid())
                    //                        && inv.Id != invoiceId))
                    //              .Sum(x => x.RequestedPercent);
                    var SumPriorAmounts = 100 
                                - serviceExplanationFinancial
                                  .Where(x => allInvoices
                                       .Any(inv => inv.Id == x.InvoiceBaseInformationId
                                            && (inv.InvoiceTypeId != InvoiceTypesEnum.prepayment.GetGuid() && inv.InvoiceTypeId != InvoiceTypesEnum.onAccount.GetGuid())
                                            && inv.Id != invoiceId))
                                  .Sum(x => x.RequestedPercent);



                    //serviceExplanationFinancial.Sum(x => x.RequestedPercent);
                    if (SumPriorAmounts < request)
                    {
                        return false;
                    }
                    return true;
                }
                return true;

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return false;
            }
        }
    }
}
