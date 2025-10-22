using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAmountDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    public  class InvoiceBaseInformationFullAutoMapper
    {
        public static InvoiceBaseInformationFullGetDto EntityToGetDto(InvoiceBaseInformation entity, CorespondentLegal? corespondentLegal,
            CorespondentReal? corespondentReal,
            List<InvoiceAmount> invoiceAmount, 
            decimal paymentAmount,
            decimal nettingProcessItemAmount,
            string lastContractAmount,
            IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new InvoiceBaseInformationFullGetDto();
                dto.Key = entity.Id;
                dto.InvoiceTitle = entity.InvoiceTitle;
                dto.SendDate = entity.SendDate;
                dto.LeadingToDate = entity.LeadingToDate;
                dto.InvoiceNumber = entity.InvoiceNumber;
                dto.Description = entity.Description;
                dto.InvoiceCode = entity.InvoiceCode;
                dto.InvoiceTypeId = entity.InvoiceTypeId;
                dto.InvoiceTypeTitle = entity.InvoiceType.Title;
                dto.ContractId = entity.Contract.ID;
                dto.ContractTitle = entity.Contract.ContractTitle;
                dto.ContractNumber = entity.Contract.ContractNumber;
                dto.PaymentAmount = paymentAmount;
                dto.NettingProcessItemsAmount = nettingProcessItemAmount;
                dto.LastContractAmount = lastContractAmount;
                //اتصال به کردیت سورس اجباریه ولی عباس هنوز توسعه نداده. هر وقت توسعه داده باید ترنری رو برداریم
                dto.CreditSourceID = (entity.Contract.CreditSource != null) ? entity.Contract.CreditSource.ID : Guid.Empty;
                dto.CreditSourceTitle = (entity.Contract.CreditSource != null) ? entity.Contract.CreditSource.Title : "";
                dto.CurrentStateTitle = entity.CurrentStateTitle;
                dto.LastActionTitle = entity.LastActionTitle;
                if (corespondentReal != null)
                {
                    //حقیقی
                    dto.Corespondent = corespondentReal.Name + " " + corespondentReal.Family;
                    //dto.BirthCertificateNumber = corespondentReal.BirthCertificateNumber;
                    dto.BirthCertificateNumber = "";
                    dto.RegistrationNumber = "";
                    dto.NationalCode = corespondentReal.NationalCode;
                    dto.NationalId = "";
                    dto.Address = corespondentReal.Address;
                    dto.BankAcountNumber = corespondentReal.BankAcountNumber;
                    dto.ShabaNumber = corespondentReal.ShabaNumber;
                    dto.BankName = corespondentReal.BankName;
                    dto.BranchCodeAndName = corespondentReal.BranchCodeAndName;
                    dto.IsLegal = false;
                }
                else
                {
                    //حقوقی
                    dto.Corespondent = corespondentLegal.CompanyName;
                    dto.BirthCertificateNumber = "";
                    dto.RegistrationNumber = corespondentLegal.RegistrationNumber;
                    dto.NationalId = corespondentLegal.NationalID;
                    dto.NationalCode = "";
                    dto.Address = corespondentLegal.Address;
                    dto.BankAcountNumber = corespondentLegal.BankAcountNumber;
                    dto.ShabaNumber = corespondentLegal.ShabaNumber;
                    dto.BankName = corespondentLegal.BankName;
                    dto.BranchCodeAndName = corespondentLegal.BranchCodeAndName;
                    dto.IsLegal = true;
                }
                dto.LastInvoiceAmount = new List<InvoiceAmountGetDto>();
                if (invoiceAmount != null && invoiceAmount.Count > 0)
                {
                    foreach (var amount in invoiceAmount)
                    {
                        var NewAmount = new InvoiceAmountGetDto();
                        if (amount.NettingAmount != 0)
                        {
                            NewAmount.Amount = amount.NettingAmount;
                            NewAmount.CurrencyTitle = amount.Currency != null ? amount.Currency.Title : " ";
                            dto.LastInvoiceAmount.Add(NewAmount);
                        }
                        else if (amount.ApprovedAmount != 0)
                        {
                            NewAmount.Amount = amount.ApprovedAmount;
                            NewAmount.CurrencyTitle = amount.Currency != null ? amount.Currency.Title : " ";
                            dto.LastInvoiceAmount.Add(NewAmount);
                        }
                        else
                        {
                            NewAmount.Amount = amount.RequestedAmount;
                            NewAmount.CurrencyTitle = amount.Currency != null ? amount.Currency.Title : " ";
                            dto.LastInvoiceAmount.Add(NewAmount);
                        }
                    }


                }
                dto.AccountId = entity.AccountId;
                dto.OrganizationUnitId = entity.Contract.OrganizationUnitID;
                //اتصال به ارگانیزیشن اجباریه ولی عباس هنوز توسعه نداده. هر وقت توسعه داده باید ترنری رو برداریم
                dto.OrganizationUnitTitle = (entity.Contract.Organizationalunit != null) ? entity.Contract.Organizationalunit.Title : "";
                dto.ConsultantName = (entity.Contract.ConsultantName != null) ? entity.Contract.ConsultantName : "";
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new InvoiceBaseInformationFullGetDto();
            }
        }
        public static GetContractCorespondentInformationsDto CorespondentEntityToDto(CorespondentLegal? corespondentLegal, CorespondentReal? corespondentReal, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new GetContractCorespondentInformationsDto();
                if (corespondentReal != null)
                {
                    //حقیقی
                    dto.Corespondent = corespondentReal.Name + " " + corespondentReal.Family;
                    //dto.BirthCertificateNumber = corespondentReal.BirthCertificateNumber;
                    dto.BirthCertificateNumber = "";
                    dto.RegistrationNumber = "";
                    dto.NationalCode = corespondentReal.NationalCode;
                    dto.NationalId = "";
                    dto.Address = corespondentReal.Address;
                    dto.BankAcountNumber = corespondentReal.BankAcountNumber;
                    dto.ShabaNumber = corespondentReal.ShabaNumber;
                    dto.BankName = corespondentReal.BankName;
                    dto.BranchCodeAndName = corespondentReal.BranchCodeAndName;
                    dto.IsLegal = false;
                }
                else
                {
                    //حقوقی
                    dto.Corespondent = corespondentLegal.CompanyName;
                    dto.BirthCertificateNumber = "";
                    dto.RegistrationNumber = corespondentLegal.RegistrationNumber;
                    dto.NationalId = corespondentLegal.NationalID;
                    dto.NationalCode = "";
                    dto.Address = corespondentLegal.Address;
                    dto.BankAcountNumber = corespondentLegal.BankAcountNumber;
                    dto.ShabaNumber = corespondentLegal.ShabaNumber;
                    dto.BankName = corespondentLegal.BankName;
                    dto.BranchCodeAndName = corespondentLegal.BranchCodeAndName;
                    dto.IsLegal = true;
                }
                return dto;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new GetContractCorespondentInformationsDto();
            }
        }
    }
}
