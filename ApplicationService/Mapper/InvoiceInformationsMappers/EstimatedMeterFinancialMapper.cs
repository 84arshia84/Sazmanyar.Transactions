using AppCore.Entities.ContractsInformation.ContractEstimatedmeters;
using AppCore.Entities.ContractsInformation.ServiceExplanations;
using AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials;
using AppCore.Entities.InvoiceInformations.ServiceExplanationFinancials;
using ApplicationService.DtoModels.InvoiceDtos.EstimatedMeterFinancialsDtos;
using ApplicationService.DtoModels.InvoiceDtos.ServiceExplanationFinancialsDtos;
using ApplicationService.ServicesContract.ExceptionHandling;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ApplicationService.Mapper.InvoiceInformationsMappers
{
    internal static class EstimatedMeterFinancialMapper
    {
        public static List<GetAllEMFDto> EntitiesToGetAllCFSDtos(List<ContractEstimatedmeter> contractEstimatedMeterEntities, List<EstimatedMeterFinancial> estimatedMeterFinancialEntities,
            IErrorLoggerService errorLoggerService)
        {
            try
            {
                var dtos = new List<GetAllEMFDto>();
                var i = 1;
                foreach (EstimatedMeterFinancial entity in estimatedMeterFinancialEntities)
                {
                    try
                    {
                        var dto = new GetAllEMFDto();
                        dto.Key = entity.Id;
                        dto.Row = i;
                        var releventEM = contractEstimatedMeterEntities.Where(cem => cem.Id == entity.ContractEstimatedmeterId).FirstOrDefault();
                        dto.EstimatedMeterType = string.Empty;
                        dto.YearId = releventEM.YearId;
                        dto.Year = releventEM.Year;
                        dto.FieldId = releventEM.FieldId;
                        dto.Field = releventEM.Field;
                        dto.RowNumber = string.Empty; //Develope
                        dto.ServiceExplanationTitle = releventEM.ServiceExplanation.Title;
                        dto.ProjectId = releventEM.ServiceExplanation.ProjectID;
                        dto.ProjectName = releventEM.ServiceExplanation.ProjectName;
                        dto.ProposalId = releventEM.ServiceExplanation.ProposalID;
                        dto.ProposalName = releventEM.ServiceExplanation.ProposalName;
                        dto.CurrencyId = releventEM.ServiceExplanation.CurrencyID;
                        dto.CurrencyTitle = releventEM.ServiceExplanation.Currency.Title;
                        dto.UnitPrice = releventEM.ServiceExplanation.UnitAmount;
                        dto.Unit = string.Empty; //Develope
                        dto.Amount = releventEM.Amount;
                        dto.RawPrice = 0; //Develope
                        dto.CoefficientTitle = releventEM.CoefficientTitle;
                        dto.SumOfCoefficients = releventEM.SumOfCoefficients;
                        dto.RowPrice = releventEM.RowPrice;
                        dto.Description = releventEM.Description;
                        dto.VolumeSummation = 0; //Develope
                        dto.PercentSummation = 0; //Develope
                        dto.ProjectCenterPercent = 0; //Develope
                        dto.RequestedVolume = entity.RequestedVolume;
                        dto.RequestedPercent = entity.RequestedPercent;
                        dto.RequestedPrice = entity.RequestedPrice;
                        dto.ApprovedVolume = entity.ApprovedVolume;
                        dto.ApprovedPercent = entity.ApprovedPercent;
                        dto.ApprovedPrice = entity.ApprovedPrice;
                        dto.IsFinancial = true;
                        contractEstimatedMeterEntities.Remove(releventEM);
                        dtos.Add(dto);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        errorLoggerService.SaveError(ex);
                        continue;
                    }
                }
                foreach (ContractEstimatedmeter entity in contractEstimatedMeterEntities)
                {
                    try
                    {
                        var dto = new GetAllEMFDto();
                        dto.Key = entity.Id;
                        dto.Row = i;
                        dto.EstimatedMeterType = string.Empty;
                        dto.YearId = entity.YearId;
                        dto.Year = entity.Year;
                        dto.FieldId = entity.FieldId;
                        dto.Field = entity.Field;
                        dto.RowNumber = string.Empty; //Develope
                        dto.ServiceExplanationTitle = entity.ServiceExplanation.Title;
                        dto.ProjectId = entity.ServiceExplanation.ProjectID;
                        dto.ProjectName = entity.ServiceExplanation.ProjectName;
                        dto.ProposalId = entity.ServiceExplanation.ProposalID;
                        dto.ProposalName = entity.ServiceExplanation.ProposalName;
                        dto.CurrencyId = entity.ServiceExplanation.CurrencyID;
                        dto.CurrencyTitle = entity.ServiceExplanation.Currency.Title;
                        dto.UnitPrice = entity.ServiceExplanation.UnitAmount;
                        dto.Unit = string.Empty; //Develope
                        dto.Amount = entity.Amount;
                        dto.RawPrice = 0; //Develope
                        dto.CoefficientTitle = entity.CoefficientTitle;
                        dto.SumOfCoefficients = entity.SumOfCoefficients;
                        dto.RowPrice = entity.RowPrice;
                        dto.Description = entity.Description;
                        dto.VolumeSummation = 0; //Develope
                        dto.PercentSummation = 0; //Develope
                        dto.ProjectCenterPercent = 0; //Develope
                        dto.RequestedVolume = 0;
                        dto.RequestedPercent = 0;
                        dto.RequestedPrice = 0;
                        dto.ApprovedVolume = 0;
                        dto.ApprovedPercent = 0;
                        dto.ApprovedPrice = 0;
                        dto.IsFinancial = false;
                        contractEstimatedMeterEntities.Remove(entity);
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
            catch (Exception ex)
            {
                errorLoggerService.SaveError(ex);
                return new List<GetAllEMFDto>();
            }
        }
        public static EstimatedMeterFinancial DtoToEntity(CUDEMRequestedFinancialDto cUDEMRequestedFinancialDto, CalculatedEMRequestedFinancialDto calculatedEMRequestedFinancialDto)
        {
            var entity = new EstimatedMeterFinancial();
            if (cUDEMRequestedFinancialDto.Key == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            else
            {
                entity.Id = cUDEMRequestedFinancialDto.Key;
            }
            entity.ContractEstimatedmeterId = cUDEMRequestedFinancialDto.ContractEstimatedMeterId;
            entity.InvoiceBaseInformationId = cUDEMRequestedFinancialDto.InvoiceBaseInformationId;
            entity.RequestedVolume = calculatedEMRequestedFinancialDto.CalculatedRequestedVolume;
            entity.RequestedPercent = calculatedEMRequestedFinancialDto.CalculatedRequestedPercent;
            entity.RequestedPrice = calculatedEMRequestedFinancialDto.CalculatedRequestedPrice;
            entity.ApprovedVolume = (calculatedEMRequestedFinancialDto.CalculatedRequestedVolume != null) ? 0 : null;
            entity.ApprovedPercent = 0;
            entity.ApprovedPrice = 0;
            return entity;
        }
    }
}
