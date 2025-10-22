using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.InvoiceInformations.EstimatedMeterFinancials
{
    public class EstimatedMeterFinancial
    {
        #region properties
        public Guid Id { get; set; }
        public Guid ContractEstimatedmeterId { get; set; }
        public decimal? RequestedVolume { get; set; }
        public decimal RequestedPercent { get; set; }
        public decimal RequestedPrice { get; set; }
        public decimal? ApprovedVolume { get; set; }
        public decimal ApprovedPercent { get; set; }
        public decimal ApprovedPrice { get; set; }
        #endregion
        #region relations
        public Guid InvoiceBaseInformationId { get; set; }
        public InvoiceBaseInformation InvoiceBaseInformation { get; set; }
        #endregion
    }
}
