using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Enums
{
    public enum InvoiceTypesEnum
    {

        /// <summary>
        /// موقت
        /// </summary>
        [Guid("C4BE10DB-C477-4D86-B38A-5709D4A64828")]
        temporary = 1,
        /// <summary>
        /// تعدیل
        /// </summary>
        [Guid("17DF3CC8-DD59-4757-8C33-7C1C34C04271")]
        adjustment = 2,
        /// <summary>
        /// تعلیق
        /// </summary>
        [Guid("FF760CDC-FEE9-4D9F-A735-83835F92EEFB")]
        suspension = 3,
        /// <summary>
        /// پیش پرداخت
        /// </summary>
        [Guid("1149AF0B-EFB0-4D79-94DF-996555D3FF56")]
        prepayment = 4,
        /// <summary>
        /// علی الحساب
        /// </summary>
        [Guid("63CB0F99-72E4-4439-9F69-AA58C2877E94")]
        onAccount = 5,
        /// <summary>
        /// قطعی
        /// </summary>
        [Guid("DBB3CF0D-5B6E-406B-A033-B6448EB9FBE4")]
        definite = 6,
        /// <summary>
        /// توقف
        /// </summary>
        [Guid("A074B5BD-D8F4-42B2-BD17-EC8C092BCC0A")]
        pause = 7
    }
    
    public class GuidAttribute : Attribute
    {
        public Guid Value { get; }

        public GuidAttribute(string guid)
        {
            Value = Guid.Parse(guid);
        }
    }
    public static class EnumExtensions
    {
        public static Guid GetGuid(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo?.GetCustomAttribute<GuidAttribute>();
            return attribute?.Value ?? throw new InvalidOperationException($"GuidAttribute not found on {value.GetType().Name}.{value}");
        }
    }
}
