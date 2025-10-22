using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using AppCore.Enums;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.FactorDtos.FactorNettingProcessItem;
using ApplicationService.DtoModels.InvoiceDtos.NettingProcessDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;

namespace ApplicationService.Calculators.InvoiceInformationsCalculators
{
    internal static class NettingProcessItemCalculator 
    {
        public static decimal CalculateAmountByPercentage(decimal percentage)
        {
            return 0;
        }
        public static List<RequestAmountGetDto> CalculateRequestedPriceSummation(List<ServiceExplanationFinancial> AllSEFs)
        {
            try
            {
                var SumOfExpalantionsByCurrency = AllSEFs
                                  .GroupBy(x => x.CurrencyId)
                                  .Select(g => new RequestAmountGetDto
                                  {
                                      Currency = g.Key,
                                      RequestAmount = g.Sum(x => x.RequestedPrice)
                                  })
                                  .ToList();
                //decimal result = 0;
                //result = AllSEFs.Sum(SEF => SEF.RequestedPrice);
                return SumOfExpalantionsByCurrency;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static List<ApprovedAmountGetDto> CalculateApprovedPriceSummation(List<ServiceExplanationFinancial> AllSEFs)
        {
            try
            {
                var SumOfExpalantionsByCurrency = AllSEFs
                                  .GroupBy(x => x.CurrencyId)
                                  .Select(g => new ApprovedAmountGetDto
                                  {
                                      Currency = g.Key,
                                      ApproveAmount = g.Sum(x => x.ApprovedPrice)
                                  })
                                  .ToList();
                //decimal result = 0;
                //result = AllSEFs.Sum(SEF => SEF.ApprovedPrice);
                return SumOfExpalantionsByCurrency;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static decimal CalculateNetPrice(decimal invoiceApprovedPriceSummation, List<GetAllNettingProcessItemsDto> allNPItems,Guid currencyId)
        {
            try
            {
                decimal result = 0;
                decimal deductedPercentage = allNPItems.Where(npi => npi.IsDeduction).Where(npi=>npi.NettingProcessType!=1 && npi.NettingProcessType!=2 &&npi.CurrencyId== currencyId).Sum(npi => npi.Percentage);
                decimal addedPercentage = allNPItems.Where(npi => !npi.IsDeduction && npi.CurrencyId == currencyId).Sum(npi => npi.Percentage);
                decimal deductedPrice = (deductedPercentage * invoiceApprovedPriceSummation) / 100;
                decimal addedPrice = (addedPercentage * invoiceApprovedPriceSummation) / 100;
                result = invoiceApprovedPriceSummation - deductedPrice + addedPrice;
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        
    }
}
