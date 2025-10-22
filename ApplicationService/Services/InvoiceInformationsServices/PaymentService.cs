using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.UnitOfWork;
using ApplicationService.Calculators.InvoiceInformationsCalculators;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.DtoModels.InvoiceDtos.PaymentDtos;
using ApplicationService.DtoModels.UserDtos;
using ApplicationService.Mapper.InvoiceInformationsMappers;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    public class PaymentService : IPaymentService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        public PaymentService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
        }
        public async Task<(string message, bool isSuccess)> Add(AddPaymentDto addPaymentDto, LoginUserDto userDto)
        {
            try
            {
                var entity = PaymentMapper.DtoToEntity(addPaymentDto, userDto, _errorLoggerService);
                await _unitOfWork.PaymentRepository.Add(entity);
                return ("ثبت  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ثبت با خطا مواجه شد.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> Delete(Guid paymentId, LoginUserDto userDto)
        {
            try
            {
                await _unitOfWork.PaymentRepository.Delete(paymentId);
                return ("حذف  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("حذف با خطا مواجه شد.", false);
            }
        }
        public async Task<(string message, bool isSuccess)> Update(UpdatePaymentDto updatePaymentDto, LoginUserDto userDto)
        {
            try
            {
                var model = await _unitOfWork.PaymentRepository.Get(updatePaymentDto.Key);
                if (model != null)
                {
                    model.CurrencyId = updatePaymentDto.CurrencyId;
                    model.ExchangeRate = updatePaymentDto.ExchangeRate;
                    model.PaymentAmount = updatePaymentDto.PaymentAmount;
                    model.HowToPayId = updatePaymentDto.HowToPayId;
                    model.PaymentDate = updatePaymentDto.PaymentDate;
                }
                await _unitOfWork.Save();
                return ("ویرایش  با موفقیت انجام شد.", true);
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return ("ویرایش با خطا مواجه شد.", false);
            }
        }
        public async Task<List<GetAllPaymentDto>> GetAll(Guid invoiceBaseInformationId)
        {
            try
            {
                var entities = await _unitOfWork.PaymentRepository.GetAll(invoiceBaseInformationId);
                var results = PaymentMapper.EntitiesToDtos(entities, _errorLoggerService);
                return results;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<GetAllPaymentDto>();
            }
        }
        public async Task<List<NettedAmountGetDto>> GetInvoicePaidPrice(Guid invoiceBaseInformationId)
        {
            try
            {
                var allPayments = await _unitOfWork.PaymentRepository.GetAll(invoiceBaseInformationId);
                var Result = allPayments
                    .GroupBy(x => x.CurrencyId)
                    .Select(g => new NettedAmountGetDto
                    {
                        Currency = g.Key,
                        NettedAmount = PaymentCalculator.CalculatePaymentPrice(g.ToList())
                    }).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<NettedAmountGetDto>();
            }
        }
        public async Task<List<NettedAmountGetDto>> GetContractPaidPrice(Guid contractId)
        {
            try
            {
                var AllInvoices = await _unitOfWork.InvoiceBaseInformationRepository.GetAllByContractId(contractId);
                var allResults = new List<NettedAmountGetDto>();
                foreach (var invoice in AllInvoices)
                {
                    var invoiceResults = await GetInvoicePaidPrice(invoice.Id);
                    allResults.AddRange(invoiceResults);
                }
                var result = allResults
                .GroupBy(x => x.Currency)
                .Select(g => new NettedAmountGetDto
                {
                   Currency = g.Key,
                  NettedAmount = g.Sum(x => x.NettedAmount)
                })
                .ToList();

                return result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<NettedAmountGetDto>();
            }
        }
        public async Task<List<NettedAmountGetDto>> GetAllBeforThisInvoiceBaseInformationId(Guid invoiceBaseInformationId)
        {
            try
            {
                var allPayments = await _unitOfWork.PaymentRepository.GetAllBeforThisInvoiceBaseInformationId(invoiceBaseInformationId);
                var Result = allPayments
                    .GroupBy(x => x.CurrencyId)
                    .Select(g => new NettedAmountGetDto
                    {
                        Currency = g.Key,
                        NettedAmount = PaymentCalculator.CalculatePaymentPrice(g.ToList())
                    }).ToList();
                return Result;
            }
            catch (Exception ex)
            {
                await _errorLoggerService.SaveError(ex);
                return new List<NettedAmountGetDto>();
            }
        }
    }
}
