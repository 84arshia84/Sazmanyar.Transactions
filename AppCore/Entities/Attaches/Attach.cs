using AppCore.Entities.ContractsInformation.Contratcs;
using AppCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Attaches
{
    /// <summary>
    /// پیوست ها
    /// </summary>
    public class Attach
    {
        #region properties
        public Guid Id { get; set; }
        /// <summary>
        /// شخصی که پیوست را بارگزاری کرده
        /// </summary>
        public string? UserUploader {  get; set; }
        public string? UserUploaderName { get; set; }
        /// <summary>
        /// نام فایل
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// تایپ فایل
        /// </summary>
        public string FileExtention { get; set; }
        /// <summary>
        /// تاریخ بارگزاری
        /// </summary>
        public DateTime UploadeDate { get; set; }
        /// <summary>
        /// آیدی که بعد از آپلود فایل به ما بر میگرداند
        /// </summary>
        public Guid? SharePointId { get; set; }
        #endregion
        #region relation 
        /// <summary>
        /// پیوست متعطلق به کدام قسمت قرارداد هست 
        /// می تونه تنظیم معامله باشه 
        /// می تونه صورت وضعیت باشه 
        /// و...
        /// </summary>
        public Guid SectionId { get; set; }
        #endregion
    }
}
