using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using ApplicationService.DtoModels.UserDtos;
using AppCore.Entities.ContractsInformation.ContractFinancialDetailes;
using AppCore.Entities.ContractsInformation.ContratTimeProfiles;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using AppCore.Entities.SettingEntities.CorespondentLegals;
using AppCore.Entities.SettingEntities.CorespondentReals;
using ApplicationService.Mapper.ContractInformationMapper;
using ApplicationService.ServicesContract.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationService.DtoModels.WFEDto;
using AppCore.Entities.InvoiceInformations.InvoiceAmounts;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceAmountDtos;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    internal static class InvoiceBaseInformationMapper
    {
        public static InvoiceBaseInformation DtoToEntity(InvoiceBaseInformationInsertDto dto, LoginUserDto userDto, GetFirstStageDto? getFirstStageDto, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var InvoiceBaseInformation = new InvoiceBaseInformation();
                if (dto.Key == Guid.Empty)
                {
                    InvoiceBaseInformation.Id = Guid.NewGuid();
                    InvoiceBaseInformation.InsertDate = DateTime.Now;
                    // set current stage id
                    InvoiceBaseInformation.CurrentStageId = getFirstStageDto.Id;
                    InvoiceBaseInformation.LastActionTitle = $"ثبت شده توسط یوزر {userDto.FullQualifyName}";
                    InvoiceBaseInformation.CurrentStateTitle = $"در انتظار ارسال یوزر {userDto.FullQualifyName}";

                }
                else
                {
                    InvoiceBaseInformation.Id = dto.Key;
                }
                InvoiceBaseInformation.ContractId = dto.ContractId;
                InvoiceBaseInformation.InvoiceNumber = null;
                InvoiceBaseInformation.InvoiceTitle = dto.InvoiceTitle;
                InvoiceBaseInformation.LeadingToDate = dto.LeadingToDate;
                InvoiceBaseInformation.SendDate = dto.SendDate;
                InvoiceBaseInformation.Description = dto.Description;
                InvoiceBaseInformation.InvoiceCode = dto.InvoiceCode;
                InvoiceBaseInformation.InvoiceTypeId = dto.InvoiceTypeId;
                InvoiceBaseInformation.InsertBy = userDto.ID;
                InvoiceBaseInformation.IsDeleted = false;
                InvoiceBaseInformation.DeleteBy = Guid.Empty;
                InvoiceBaseInformation.DeleteDate = null;
                InvoiceBaseInformation.AccountId = dto.AccountId;
                return InvoiceBaseInformation;
            }
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return (new InvoiceBaseInformation());
            }
        }
        public static InvoiceBaseInformationGetDto EntityToGetDto(InvoiceBaseInformation entity, CorespondentLegal? corespondentLegal,
            CorespondentReal? corespondentReal, List<InvoiceAmount> invoiceAmount, IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dto = new InvoiceBaseInformationGetDto();
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
                if (invoiceAmount != null && invoiceAmount.Count>0)
                {
                    foreach(var amount in invoiceAmount)
                    {
                        var NewAmount = new InvoiceAmountGetDto();
                        if (amount.NettingAmount != 0)
                        {
                            NewAmount.Amount = amount.NettingAmount;
                            NewAmount.CurrencyTitle = amount.Currency !=null? amount.Currency.Title : " ";
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
                return new InvoiceBaseInformationGetDto();
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
        public static List<InvoiceBaseInformationGetAllDto> EntitiesToGetAllDtos(List<InvoiceBaseInformation> entities, List<CorespondentLegal> LegalCorespondents,
            List<CorespondentReal> RealCorespondents, List<InvoiceAmount> invoiceAmounts, IErrorLoggerService errorLoggerService)
        {

            var dtos = new List<InvoiceBaseInformationGetAllDto>();
            var i = 1;
            foreach (InvoiceBaseInformation entity in entities)
            {
                try
                {
                    var dto = new InvoiceBaseInformationGetAllDto();
                    dto.Key = entity.Id;
                    dto.InvoiceTitle = entity.InvoiceTitle;
                    dto.InvoiceCode = entity.InvoiceCode;
                    dto.ContractTitle = entity.Contract.ContractTitle;
                    dto.LeadingToDate = entity.LeadingToDate;
                    dto.SendDate = entity.SendDate;
                    dto.CurrentStateTitle = entity.CurrentStateTitle;
                    dto.LastActionTitle = entity.LastActionTitle;
                    dto.IsfinalApproved = entity.IsFinalApproved;
                    dto.invoiceType = entity.InvoiceTypeId;
                    if (entity.Contract.CorespondentRealOrLegal)
                    {
                        //حقیقی
                        var RealCorespondent = RealCorespondents.Where(RC => RC.ID == entity.Contract.CorespondentID).FirstOrDefault();
                        if (RealCorespondent != null)
                        {
                            dto.Corespondent = RealCorespondent.Name + " " + RealCorespondent.Family;
                        }
                    }
                    else
                    {
                        //حقوقی
                        var LegalCorespondent = LegalCorespondents.Where(LC => LC.ID == entity.Contract.CorespondentID).FirstOrDefault();
                        if (LegalCorespondent != null)
                        {
                            dto.Corespondent = LegalCorespondent.CompanyName;
                        }
                    }
                    var lastAmount = invoiceAmounts.Where(x => x.InvocieBaseInformationId == dto.Key).ToList();
                    dto.LastInvoiceAmount = new List<InvoiceAmountGetDto>();
                    if (lastAmount != null && lastAmount.Count>0)
                    {
                        foreach (var amount in lastAmount)
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
                    //باید توسعه داده شود. فعلا خالی پر می شود
                    dto.InvoicePrice = "";
                    dto.InvoiceNumber = entity.InvoiceNumber;
                    dto.contractNumber = entity.Contract.ContractNumber;
                    dto.LastContractAmount = "";
                    dto.CurrentSituation = "";
                    dto.LastAction = "";
                    dto.contractExpertKey = entity.Contract.ContractExpertID;
                    dto.consultantID = entity.Contract.ConsultantID;
                    dto.contractTypeID = entity.Contract.ContractTypeID;
                    dto.transActionTypeID = entity.Contract.TransActionTypeID;
                    dto.roleOFOrganizationID = entity.Contract.RoleOFOrganizationID;
                    dto.corespondentID = entity.Contract.CorespondentID;
                    dto.organizationUnitID = entity.Contract.OrganizationUnitID;
                    dto.creditSourceID = entity.Contract.CreditSourceID;
                    dto.ContractDateOfNotification = entity.Contract.ContratTimeProfile.ContractDateOfNotification;
                    dto.ContractExchangeDate = entity.Contract.ContratTimeProfile.ContractExchangeDate;
                    dto.ContractStartDate = entity.Contract.ContratTimeProfile.ContractStartDate;
                    dto.ContractEndDate = entity.Contract.ContratTimeProfile.ContractEndDate;
                    dto.BasisForStartingProjectID = entity.Contract.ContratTimeProfile.BasisForStartingProjectID;
                    dto.Row = i;
                    dtos.Add(dto);
                    i++;
                }
                catch (Exception ex)
                {
                    errorLoggerService.SaveError(ex);
                    continue;
                }
            }
            return dtos;
        }

    }
}

