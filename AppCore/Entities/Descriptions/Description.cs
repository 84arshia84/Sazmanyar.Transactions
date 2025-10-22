using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.Descriptions
{
    public class Description
    {
        /// <summary>
        /// توضیحات
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// اسم و فامیل نویسنده
        /// </summary>
        public string AuthorName { get; set; }
        public string AuthorFullQualifyName { get; set; }
        /// <summary>
        /// زمان نوشتن متن
        /// </summary>
        public string WriteTime { get; set; }
        /// <summary>
        /// زمان ثبت
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// متن
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// توضیحات متعطلق به کدام قسمت قرارداد هست 
        /// می تونه تنظیم معامله باشه 
        /// می تونه صورت وضعیت باشه 
        /// و...
        /// </summary>
        public Guid SectionId { get; set; }
    }
}
