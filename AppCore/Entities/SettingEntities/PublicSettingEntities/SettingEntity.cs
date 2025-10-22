using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Entities.SettingEntities.PublicSettingEntities.SettingEntity
{
    public abstract class SettingEntity
    {
        public Guid ID { get; set; }
        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// حذف
        /// </summary>
        public bool IsDeleted { get; set; }
        public int Order {  get; set; }
    }
}
