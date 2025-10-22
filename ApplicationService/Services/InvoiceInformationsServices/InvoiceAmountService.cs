using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.Users;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    internal class InvoiceAmountService : IInvoiceAmountService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public InvoiceAmountService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task AddInvoiceAmount(Guid invoiceId)
        {
            try
            {
                var checkExisted = await GetInvoiceAmount(invoiceId);
                if (checkExisted != null && checkExisted.Count>0)
                {
                    await UpdateRequestedAmount(invoiceId, false);
                    return;
                }
             
                var serviceExplenation = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceId);
                if (serviceExplenation.Count > 0)
                {
                    var SumOfExpalantionsByCurrency = serviceExplenation
                                  .GroupBy(x => x.CurrencyId)
                                  .Select(g => new
                                  {
                                      Currency = g.Key,
                                      Amount = g.Sum(x => x.RequestedPrice)
                                  })
                                  .ToList();
                    foreach (var item in SumOfExpalantionsByCurrency)
                    {
                        var model = new InvoiceAmount();
                        model.InvocieBaseInformationId = invoiceId;
                        model.CurrencyId = item.Currency;
                        model.RequestedAmount = item.Amount;
                        model.ApprovedAmount = 0;
                        model.NettingAmount = 0;
                        await _unitOfWork.InvoiceAmountRepository.AddInvoiceAmount(model);
                    }
                   
                    await _unitOfWork.Save();
                }
                
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public async Task<List<InvoiceAmount>> GetInvoiceAmount(Guid invoiceId)
        {
            try
            {
                return await _unitOfWork.InvoiceAmountRepository.GetAllInvoiceAmount(invoiceId);
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }

        public async Task UpdateApprovedAmount(Guid invoiceId, bool rejected)
        {
            try
            {
                var serviceExplenation = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceId);
                if (serviceExplenation.Count > 0 && !rejected)
                {
                    var SumOfExpalantionsByCurrency = serviceExplenation
                                  .GroupBy(x => x.CurrencyId)
                                  .Select(g => new
                                  {
                                      Currency = g.Key,
                                      Amount = g.Sum(x => x.ApprovedPrice)
                                  })
                                  .ToList();
                    foreach (var item in SumOfExpalantionsByCurrency)
                    {
                        await _unitOfWork.InvoiceAmountRepository.UpdateApprovedAmount(item.Amount, invoiceId,(Guid)item.Currency);
                    }
                    await _unitOfWork.Save();
                    return;
                }
                foreach (var item in serviceExplenation)
                {
                    await _unitOfWork.InvoiceAmountRepository.UpdateApprovedAmount(0, invoiceId, (Guid)item.CurrencyId);
                }
                await _unitOfWork.Save();


            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public async Task UpdateNettingAmount(List<NettedAmountGetDto> nettedValues, Guid invoiceId)
        {
            try
            {
                foreach (var nettedValue in nettedValues)
                {
                    await _unitOfWork.InvoiceAmountRepository.UpdateNettingAmount(nettedValue.NettedAmount, invoiceId, (Guid)nettedValue.Currency);
                }
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }

        public async Task UpdateRequestedAmount(Guid invoiceId, bool rejected)
        {
            try
            {
                var serviceExplenation = await _unitOfWork.ServiceExplanationFinancialRepository.GetAllByInvoiceBaseInformationId(invoiceId);
                if (serviceExplenation.Count > 0 && !rejected)
                {
                    await _unitOfWork.InvoiceAmountRepository.DeleteAmounts(invoiceId);
                    await _unitOfWork.Save();
                    var SumOfExpalantionsByCurrency = serviceExplenation
                                  .GroupBy(x => x.CurrencyId)
                                  .Select(g => new
                                  {
                                      Currency = g.Key,
                                      Amount = g.Sum(x => x.RequestedPrice)
                                  })
                                  .ToList();
                    foreach (var item in SumOfExpalantionsByCurrency)
                    {
                        var model = new InvoiceAmount();
                        model.InvocieBaseInformationId = invoiceId;
                        model.CurrencyId = item.Currency;
                        model.RequestedAmount = item.Amount;
                        model.ApprovedAmount = 0;
                        model.NettingAmount = 0;
                        await _unitOfWork.InvoiceAmountRepository.AddInvoiceAmount(model);
                    }
                    //foreach (var item in SumOfExpalantionsByCurrency)
                    //{
                    //    await _unitOfWork.InvoiceAmountRepository.UpdateRequestedAmount(item.Amount, invoiceId, (Guid)item.Currency);
                    //}
                    await _unitOfWork.Save();
                    return;
                }
                foreach (var item in serviceExplenation)
                {
                    await _unitOfWork.InvoiceAmountRepository.UpdateRequestedAmount(0, invoiceId, (Guid)item.CurrencyId);
                }
                await _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
            }
        }
    }
}
