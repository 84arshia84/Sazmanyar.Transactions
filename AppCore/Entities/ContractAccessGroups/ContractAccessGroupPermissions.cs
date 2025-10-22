using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// دسترسی به بخش های سامانه
    /// </summary>
    public class ContractAccessGroupPermissions
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// تب درخواست برگزاری معامله
        /// </summary>
        public bool TransactionExecutionRequest { get; set; }
        /// <summary>
        /// دسترسی به تب معاملات
        /// </summary>
        public bool Contract { get; set; }
        /// <summary>
        /// دسترسی به تب الحاقیه
        /// </summary>
        public bool ContractAddendum { get; set; }
        /// <summary>
        /// دسترسی به تب گروه دسترسی معاملات
        /// </summary>
        public bool AccessGroupSettings { get; set; }
        /// <summary>
        /// دسترسی به تنظیمات پایه معاملات
        /// </summary>
        public bool BaseSettings { get; set; }
        /// <summary>
        /// دسترسی به تب تنظیمات فهرست بها
        /// </summary>
        public bool PriceListSettings { get; set; }
        /// <summary>
        /// دسترسی به بخش فلو معاملات
        /// </summary>
        public bool WorkFlow { get; set; }
        #endregion
        #region relation
        public Guid ContractAccessGroupId { get; set; }
        public ContractAccessGroup ContractAccessGroup { get; set; }
        #endregion
    }
}
