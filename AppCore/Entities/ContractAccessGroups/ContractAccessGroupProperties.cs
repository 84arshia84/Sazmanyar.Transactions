using AppCore.Entities.InvoiceAccessGroups.InvoiceAccessGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.ContractAccessGroups
{
    /// <summary>
    /// دسترسی های هر کاربر 
    /// </summary>
    public class ContractAccessGroupProperties
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// دسترسی ثبت نوع معامله
        /// </summary>
        public bool ContractTypeSave { get; set; }
        /// <summary>
        /// دسترسی مشاهده نوع معامله
        /// </summary>
        public bool ContractTypeView { get; set; }
        /// <summary>
        /// دسترسی ویرایش نوع معامله
        /// </summary>
        public bool ContractTypeEdit { get; set; }
        /// <summary>
        /// دسترسی حذف نوع معامله
        /// </summary>
        public bool ContractTypeDelete { get; set; }
        /// <summary>
        /// دسترسی ثبت نقش سازمان
        /// </summary>
        public bool RoleOfOrganizationSave { get; set; }
        /// <summary>
        /// دسترسی مشاهده نقش سازمان
        /// </summary>
        public bool RoleOfOrganizationView { get; set; }
        /// <summary>
        /// دسترسی ویرایش نقش سازمان
        /// </summary>
        public bool RoleOfOrganizationEdit { get; set; }
        /// <summary>
        /// دسترسی حذف نقش سازمان
        /// </summary>
        public bool RoleOfOrganizationDelete { get; set; }
        /// <summary>
        /// دسترسی ثبت واحد سازمانی
        /// </summary>
        public bool OrganizationUnitSave { get; set; }
        /// <summary>
        /// دسترسی مشاهده واحد سازمانی
        /// </summary>
        public bool OrganizationUnitView { get; set; }
        /// <summary>
        /// دسترسی ویرایش واحد سازمانی
        /// </summary>
        public bool OrganizationUnitEdit { get; set; }
        /// <summary>
        /// دسترسی حذف واحد سازمانی
        /// </summary>
        public bool OrganizationUnitDelete { get; set; }

        #endregion
        #region relation
        public Guid ContractAccessGroupId { get; set; }
        public ContractAccessGroup ContractAccessGroup { get; set; }
        #endregion

    }
}
