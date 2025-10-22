using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using AppCore.UnitOfWork;
using ApplicationService.DtoModels.AccountsDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceDataForTemplateDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.ServicesContract.ContractInformation;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.InvoiceInformations;
using ApplicationService.ServicesContract.OrganizationInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.InvoiceInformationsServices
{
    public class InvoiceTemplateService : IInvoiceTemplateService
    {
        private IUnitOfWork _unitOfWork;
        private IErrorLoggerService _errorLoggerService;
        private IServiceExplanationFinancialService _serviceExplanationFinancial;
        private IOrganizationInformationService _organizationInformationService;
        public InvoiceTemplateService(IUnitOfWork unitOfWork, IErrorLoggerService errorLoggerService,
            IServiceExplanationFinancialService serviceExplanationFinancial, IOrganizationInformationService organizationInformationService)
        {
            _unitOfWork = unitOfWork;
            _errorLoggerService = errorLoggerService;
            _serviceExplanationFinancial = serviceExplanationFinancial;
            _organizationInformationService = organizationInformationService;
        }
        public async Task<InvoiceGetDataForTemplateDto> GetTemplateData(Guid InvoiceId)
        {
            try
            {
                var FullData = new InvoiceGetDataForTemplateDto();
                var invoice = await _unitOfWork.InvoiceBaseInformationRepository.Get(InvoiceId);
                var contract = await _unitOfWork.ContractRepository.Get(invoice.ContractId);
                var addendum = await _unitOfWork.ContractAddendumRepository.GetLastAddendumOfContract(contract.ID);
                var serviceExplanation = await _serviceExplanationFinancial.GetAllCFS(new GetAllCSFIdsDto { ContractId = contract.ID, InvoiceBaseInformationId = InvoiceId }, false);
                var Province = await _unitOfWork.LocationRepository.GetAllProvince();
                var County = await _unitOfWork.LocationRepository.GetAllCounties();
                var Cities = await _unitOfWork.LocationRepository.GetAllCities();
                var UnitOfMesurment = await _unitOfWork.UnitOfMeasurementRepository.GetAll();
                var OrganizationInformation = await _organizationInformationService.Get();
                var Accounts = await _organizationInformationService.GetAccounts();
                AccountDto? Account = null;
                if (invoice.AccountId != null)
                {
                    Account = Accounts.FirstOrDefault(x => x.Id == invoice.AccountId);
                }
                CorespondentReal? corespondentReal = null;
                CorespondentLegal? corespondentLegal = null;
                if (contract.CorespondentRealOrLegal)
                {
                    corespondentReal = await _unitOfWork.CorespondentRealRepository.Get(contract.CorespondentID);
                }
                else
                {
                    corespondentLegal = await _unitOfWork.CorespondentLegalRepository.Get(contract.CorespondentID);
                }
                FullData.InvoiceTitle = invoice.InvoiceTitle;
                FullData.InvoiceCode = invoice.InvoiceCode;
                FullData.InvoiceTypeDocumentId = invoice.InvoiceType.OfficeOnlineDocumentId ?? Guid.Empty;
                FullData.ContractTitle = contract.ContractTitle;
                FullData.ContractNumber = contract.ContractNumber;
                FullData.InvoiceInsertDate = invoice.InsertDate.ToString();

                if (OrganizationInformation != null)
                {
                    FullData.SellerInformationName = OrganizationInformation.CompanyName;
                    FullData.SellerNationalId = OrganizationInformation.NationalID;
                    FullData.SellerRegistrationId = OrganizationInformation.RegistrationNumber ?? "";
                    if (OrganizationInformation.Province != null)
                    {
                        FullData.SellerProvince = Province.FirstOrDefault(x => x.Id == OrganizationInformation.Province).Name ?? "";
                    }
                    else
                    {
                        FullData.SellerProvince = "";
                    }
                    if (OrganizationInformation.County != null)
                    {
                        FullData.SellerCounty = County.FirstOrDefault(x => x.Id == OrganizationInformation.County).Name ?? "";
                    }
                    else
                    {
                        FullData.SellerCounty = "";
                    }
                    if (OrganizationInformation.City != null)
                    {
                        FullData.SellerCity = Cities.FirstOrDefault(x => x.Id == OrganizationInformation.City).Name ?? "";
                    }
                    else
                    {
                        FullData.SellerCity = "";
                    }
                    FullData.SellerZipCode = OrganizationInformation.PostalCode ?? "";
                    FullData.SellerAddress = OrganizationInformation.Address ?? "";
                    FullData.SellerPhoneNumber = OrganizationInformation.PhoneNumber ?? "";
                }
                else
                {
                    FullData.SellerInformationName = "";
                    FullData.SellerNationalId = "";
                    FullData.SellerRegistrationId = "";
                    FullData.SellerProvince = "";
                    FullData.SellerCounty = "";
                    FullData.SellerCity = "";
                    FullData.SellerZipCode = "";
                    FullData.SellerAddress = "";
                    FullData.SellerPhoneNumber = "";
                }
                if (corespondentReal != null)
                {
                    FullData.BuyerInformationName = corespondentReal.Name;
                    FullData.BuyerNationalId = corespondentReal.NationalCode ?? "";
                    FullData.BuyerRegistrationId = "";
                    if (corespondentReal.ProvincId != null)
                    {
                        FullData.BuyerProvince = Province.FirstOrDefault(x => x.Id == corespondentReal.ProvincId).Name ?? "";
                    }
                    else
                    {
                        FullData.BuyerProvince = "";
                    }
                    if (corespondentReal.CountyId != null)
                    {
                        FullData.BuyerCounty = County.FirstOrDefault(x => x.Id == corespondentReal.CountyId).Name ?? "";
                    }
                    else
                    {
                        FullData.BuyerCounty = "";
                    }
                    if (corespondentReal.CityId != null)
                    {
                        FullData.BuyerCity = Cities.FirstOrDefault(x => x.Id == corespondentReal.CityId).Name ?? "";
                    }
                    else
                    {
                        FullData.BuyerCity = "";
                    }
                    FullData.BuyerZipCode = corespondentReal.PostalCode ?? "";
                    FullData.BuyerAddress = corespondentReal.Address;
                    FullData.BuyerEconomicCode = "";
                    FullData.BuyerPhoneNumber = corespondentReal.PhoneNumber ?? "";
                }
                else
                {
                    FullData.BuyerInformationName = corespondentLegal.CompanyName ?? "";
                    FullData.BuyerNationalId = corespondentLegal.NationalID ?? "";
                    FullData.BuyerRegistrationId = corespondentLegal.RegistrationNumber ?? "";
                    if (corespondentLegal.ProvincId != null)
                    {
                        FullData.BuyerProvince = Province.FirstOrDefault(x => x.Id == corespondentLegal.ProvincId).Name;
                    }
                    else
                    {
                        FullData.BuyerProvince = "";
                    }
                    if (corespondentLegal.CountyId != null)
                    {
                        FullData.BuyerCounty = County.FirstOrDefault(x => x.Id == corespondentLegal.CountyId).Name;
                    }
                    else
                    {
                        FullData.BuyerCounty = "";
                    }
                    if (corespondentLegal.CityId != null)
                    {
                        FullData.BuyerCity = Cities.FirstOrDefault(x => x.Id == corespondentLegal.CityId).Name;
                    }
                    else
                    {
                        FullData.BuyerCity = "";
                    }
                    FullData.BuyerZipCode = corespondentLegal.PostalCode ?? "";
                    FullData.BuyerAddress = corespondentLegal.Address ?? "";
                    FullData.BuyerEconomicCode = corespondentLegal.EconomicCode ?? "";
                    FullData.BuyerPhoneNumber = corespondentLegal.PhoneNumber ?? "";
                }
                FullData.InvoiceServiceExplanations = new List<InvoiceServiceExplanationTemplateDto>();
                for (int i = 0; i < serviceExplanation.Count; i++)
                {
                    var invoiceServiceExplanation = new InvoiceServiceExplanationTemplateDto();
                    invoiceServiceExplanation.Title = serviceExplanation[i].ServiceExplanationTitle;
                    invoiceServiceExplanation.Count = serviceExplanation[i].UnitOfMeasurementId != null ?
                        serviceExplanation[i].UnitOfMeasurementId != Guid.Empty ?
                        serviceExplanation[i].RequestedVolume.ToString() :
                        serviceExplanation[i].RequestedPercent.ToString() :
                        serviceExplanation[i].RequestedPercent.ToString();


                    // serviceExplanation[i].RequestedVolume != 0 ? serviceExplanation[i].RequestedVolume.ToString() : serviceExplanation[i].RequestedPercent.ToString();
                    invoiceServiceExplanation.UnitOfMesurment = serviceExplanation[i].UnitOfMeasurementId != null ?
                        serviceExplanation[i].UnitOfMeasurementId != Guid.Empty ?
                        UnitOfMesurment.FirstOrDefault(x => x.ID ==
                    serviceExplanation[i].UnitOfMeasurementId).Title : "درصد" : "درصد";

                    invoiceServiceExplanation.UnitAmount = serviceExplanation[i].TotalAmount.ToString();
                    invoiceServiceExplanation.TotalAmount = serviceExplanation[i].RequestedPrice.ToString();
                    if (serviceExplanation[i].UnitOfMeasurementId != null)
                    {
                        if (serviceExplanation[i].UnitOfMeasurementId != Guid.Empty)
                        {
                            invoiceServiceExplanation.DiscountAmount = ((serviceExplanation[i].DiscountAmount * serviceExplanation[i].ApprovedVolume) / serviceExplanation[i].ProgramVolume).ToString();
                        }
                        else
                        {
                            invoiceServiceExplanation.DiscountAmount = ((serviceExplanation[i].DiscountAmount * serviceExplanation[i].ApprovedPercent) / 100).ToString();
                        }
                    }
                    else
                    {
                        invoiceServiceExplanation.DiscountAmount = ((serviceExplanation[i].DiscountAmount * serviceExplanation[i].ApprovedPercent) / 100).ToString();
                    }
                    //invoiceServiceExplanation.DiscountAmount = (serviceExplanation[i].TotalAmount - serviceExplanation[i].UnitAmount).ToString();
                    invoiceServiceExplanation.TotalAmountWithDiscountAmount = (serviceExplanation[i].RequestedPrice - Convert.ToDecimal(invoiceServiceExplanation.DiscountAmount)).ToString();
                    invoiceServiceExplanation.Tax = ((Convert.ToDecimal(invoiceServiceExplanation.TotalAmountWithDiscountAmount) * contract.ContractFinancialDetails.ContractValue_Added_Percent) / 100).ToString();
                    invoiceServiceExplanation.SumOf_TotalAmountWithDiscount_And_Tax = (Convert.ToDecimal(invoiceServiceExplanation.TotalAmountWithDiscountAmount) + Convert.ToDecimal(invoiceServiceExplanation.Tax)).ToString();
                    FullData.InvoiceServiceExplanations.Add(invoiceServiceExplanation);
                }
                FullData.TotalAmounts = new InvoiceServiceExplanationTemplateTotalAmountsDto();
                FullData.TotalAmounts.SumTotalAmount = serviceExplanation.Sum(x => x.RequestedPrice).ToString();
                FullData.TotalAmounts.SumDiscountAmount = FullData.InvoiceServiceExplanations.Sum(x => Convert.ToDecimal(x.DiscountAmount)).ToString();
                FullData.TotalAmounts.SumTotalAmountWithDiscountAmount = FullData.InvoiceServiceExplanations.Sum(x => Convert.ToDecimal(x.TotalAmountWithDiscountAmount)).ToString(); ;
                FullData.TotalAmounts.SumTax = FullData.InvoiceServiceExplanations.Sum(x=> ((Convert.ToDecimal(x.TotalAmountWithDiscountAmount) * contract.ContractFinancialDetails.ContractValue_Added_Percent) / 100)).ToString();
                FullData.TotalAmounts.SumOf_SumOf_TotalAmountWithDiscount_And_Tax = FullData.InvoiceServiceExplanations.Sum(x => Convert.ToDecimal(x.SumOf_TotalAmountWithDiscount_And_Tax)).ToString();
                FullData.TotalAmounts.TotalAmountInLetters = "";
                FullData.AcountNumber = Account != null ? Account.AccountNumber : "";
                FullData.BankName = Account != null ? Account.Bank : "";
                FullData.BranchCodeAndName = Account != null ? Account.Branch : "";
                FullData.ShabaNumber = Account != null ? Account.Sheba : "";
                return FullData;
            }
            catch (Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return null;
            }
        }
    }
}
