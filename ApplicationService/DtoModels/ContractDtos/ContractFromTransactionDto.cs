using ApplicationService.DtoModels.PublicEntitiesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DtoModels.ContractDtos
{


        public class ContractFromTransactionDto
    {
            #region contract
            public Guid TransactionId { get; set; }
            public Guid contractTimeProfileKey { get; set; }
            public Guid contractFinancialDetailsKey { get; set; }
            public string contractTitle { get; set; }
            public string contractNumber { get; set; }
            public string? contractExpertName { get; set; }
            public Guid? contractExpertKey { get; set; }
            public Guid? consultantID { get; set; }
            public string? consultantName { get; set; }
            public bool requirementToCloseTheAccount { get; set; }
            public List<ContractCoefficientDto>? contractCoefficientDtos { get; set; }
            public List<ContractGuaranteeDto>? contractGuaranteeDtos { get; set; }
            public List<DescriptionDto>? descriptionDtos { get; set; }
            public List<AttachDto>? attachDtos { get; set; }
            public ContractCheckListValueDto? contractCheckListValueDto { get; set; }
            public Guid contractTypeID { get; set; }
            public Guid transActionTypeID { get; set; }
            public Guid roleOFOrganizationID { get; set; }
            public Guid corespondentID { get; set; }
            public Guid organizationUnitID { get; set; }
            public long? row { get; set; }
            public bool? corespondentRealOrLegal { get; set; }
            public DateTime? contractInsertDate { get; set; }
            public bool? IsfinalApproved { get; set; }
            public bool? hasAddendum { get; set; }
            public Guid? StatusId { get; set; }
            #endregion

            #region contractTimeProfile




            public Guid? creditSourceID { get; set; }
            public DateTime? ContractDateOfNotification { get; set; }
            public DateTime? ContractExchangeDate { get; set; }
            public DateTime? ContractStartDate { get; set; }
            public DateTime? ContractEndDate { get; set; }
            public string? ContractPeriod { get; set; }
            public Guid? BasisForStartingProjectID { get; set; }
            #endregion

            #region contractFinancialDetaile
            public decimal contractInsurance_Percent { get; set; }
            public decimal contractValue_Added_Percent { get; set; }
            public decimal contractTax_Percent { get; set; }
            public decimal? percentageOfChanges { get; set; }
            public decimal goodJob_Percent { get; set; }
            public string? amount_of_timeExtension { get; set; }
            public string? basis_of_Receipt { get; set; }
            #endregion

        }
    }
