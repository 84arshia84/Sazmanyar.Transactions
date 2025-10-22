using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.FactorAccessGroup
{
    public class FactorAccessGroupProperties
    {
        public Guid Id { get; set; }
     
        public bool FactorTypeSave { get; set; }
        /// <summary>
        /// دسترسی مشاهده نوع فاکتور
        /// </summary>
        public bool FactorTypeView { get; set; }
        /// <summary>
        /// دسترسی ویرایش نوع فاکتور
        /// </summary>
        public bool FactorTypeEdit { get; set; }
        /// <summary>
        /// دسترسی حذف نوع فاکتور
        /// </summary>
        public bool FactorTypeDelete { get; set; }
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

        public Guid FactorAccessGroupId { get; set; }
        public FactorAccessGroup FactorAccessGroup { get; set; }
    }
}
