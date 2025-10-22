using AppCore.Entities.ContractsInformation.ContractAddendums;
using AppCore.Entities.ContractsInformation.TransactionExecutionRequests;
using AppCore.Entities.SettingEntities.AddendumTypes;
using AppCore.Entities.SettingEntities.ContractTypes;
using AppCore.Entities.SettingEntities.LookUpTables;
using AppCore.Entities.SettingEntities.TransActionTypes;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.CheckLists
{
    /// <summary>
    /// چک لیست
    /// </summary>
    public class CheckList
    {
        #region properties 
        public Guid Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// ضروری بودن
        /// </summary>
        public bool? IsRequired {  get; set; } 
        /// <summary>
        /// نوع چک لیست
        /// </summary>
        public string Type {  get; set; }
        /// <summary>
        /// ترتیب
        /// </summary>
        public int? Order {  get; set; }
        #endregion
        #region relation
        public Guid ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }

        public Guid? LookUpTableId { get; set; }
        public LookUpTable? LookUpTable { get; set; }
        /// <summary>
        /// برای افزودن چک لیست روی درخواست برگزاری و تغییرات معامله
        /// </summary>
       
        //public List<SystemParts>? SystemParts { get; set; }
        /// <summary>
        /// رابطه با جدول
        /// </summary>



        #endregion
    }
}
