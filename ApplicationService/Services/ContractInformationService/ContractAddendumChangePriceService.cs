using AppCore.UnitOfWork;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ContractInformationService
{
    internal static class ContractAddendumChangePriceService
    {
        /// <summary>
        /// بررسی تغییرات مبلغ در الحاقیه جدید در ثبت
        /// که با الحاقیه قبلی مقایسه می شود
        /// </summary>
        /// <param name="contractId"></param>
        /// <param name="addendumId"></param>
        /// <param name="lastAddendumId"></param>
        /// <param name="serviceExplanationService"></param>
        /// <param name="errorLoggerService"></param>
        /// <returns></returns>
        public static async Task<string> CalculateAddendumAltersInAdd(Guid contractId,Guid addendumId,Guid lastAddendumId, IServiceExplanationService serviceExplanationService,IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService)
        {
			try
			{
                string CalculatedValue = string.Empty;
                var Currencies = await unitOfWork.CurrencyRepository.GetAll();
                var ThisAddendumServiceExplanation = await serviceExplanationService.GetAllServiceExplenationForAddendum(contractId,addendumId);
                var LastAddendumServiceExplanation = new List<ServiceExplanationDto>();
                if (lastAddendumId != Guid.Empty)
                {
                    LastAddendumServiceExplanation = await serviceExplanationService.GetAllServiceExplenationForAddendum(contractId, lastAddendumId);
                }
                else
                {
                    LastAddendumServiceExplanation = await serviceExplanationService.GetAllServiceExplanations(contractId);
                }
                var ThisAddendumSumByCurrency = ThisAddendumServiceExplanation
                                                  .GroupBy(x => x.CurrencyID)
                                                  .Select(g => new
                                                  {
                                                    CurrencyID = g.Key,
                                                    TotalAmountSum = g.Sum(x => x.TotalAmount ?? 0)
                                                  })
                                                  .ToList();
                var LastAddendumSumByCurrency = LastAddendumServiceExplanation
                                                 .GroupBy(x => x.CurrencyID)
                                                 .Select(g => new
                                                 {
                                                     CurrencyID = g.Key,
                                                     TotalAmountSum = g.Sum(x => x.TotalAmount ?? 0)
                                                 })
                                                 .ToList();
                var differences = (from currency in Currencies
                                   join thisAdd in ThisAddendumSumByCurrency
                                       on currency.ID equals thisAdd.CurrencyID into thisAddGroup
                                   from thisAdd in thisAddGroup.DefaultIfEmpty()
                                   join lastAdd in LastAddendumSumByCurrency
                                       on currency.ID equals lastAdd.CurrencyID into lastAddGroup
                                   from lastAdd in lastAddGroup.DefaultIfEmpty()
                                   let diff = (thisAdd?.TotalAmountSum ?? 0) - (lastAdd?.TotalAmountSum ?? 0)
                                   where diff != 0
                                   select $"{currency.Title} : {(diff >= 0 ? "افزایشی" : "")}{diff:N3}"
                  ).ToList();

                // Final string
                CalculatedValue = string.Join(", ", differences);
                if (CalculatedValue.Contains("-"))
                {
                    CalculatedValue.Replace("-"," کاهشی ");
                }
                return CalculatedValue;

			}
			catch (Exception ex)
			{
                errorLoggerService.SaveError(ex);
                return string.Empty;
			}
        }

    }
}
