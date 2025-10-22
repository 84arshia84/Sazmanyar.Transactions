using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCore.Entities.ContractAccessGroups;

namespace AppCore.Entities.FactorAccessGroup
{
    /// <summary>
    /// دسترسی به بخش های سامانه
    /// </summary>
    public class FactorAccessGroupPermissions
    {
        public Guid Id { get; set; }
        public bool Factor {  get; set; }   
      
        /// <summary>
        /// دسترسی به تب گروه دسترسی معاملات
        /// </summary>
        public bool AccessGroupSettings { get; set; }
        /// <summary>
        /// دسترسی به تنظیمات پایه معاملات
        /// </summary>
        public bool BaseSettings { get; set; }
        /// <summary>
        /// دسترسی به بخش فلو معاملات
        /// </summary>
        public bool WorkFlow { get; set; }

        public Guid FactorAccessGroupId { get; set; }
        public FactorAccessGroup FactorAccessGroup { get; set; }
    }
}



