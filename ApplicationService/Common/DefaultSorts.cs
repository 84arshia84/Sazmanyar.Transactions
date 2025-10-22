using AppCore.Entities.InvoiceInformations.InvoiceBaseInformations;
using ApplicationService.DtoModels.ContractDtos;
using ApplicationService.DtoModels.FactorDtos.Factor;
using ApplicationService.DtoModels.InvoiceDtos.InvoiceBaseInformationsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Common
{
    public static class DefaultSorts
    {
        public static IQueryable<ContractDto> OrderDefault(this IQueryable<ContractDto> q) =>
            q.OrderByDescending(x => x.contractInsertDate).ThenBy(x=>x.contractNumber);
        public static IQueryable<FactorGetDto> OrderDefault(this IQueryable<FactorGetDto> q) =>
            q.OrderByDescending(x => x.FactorNumber).ThenBy(x => x.Id);

        public static IQueryable<InvoiceBaseInformationGetAllDto> OrderDefault(this IQueryable<InvoiceBaseInformationGetAllDto> q)
        {
            return q
                .OrderByDescending(x => x.SendDate)
                .ThenBy(x => x.InvoiceNumber ?? int.MaxValue)
                .ThenBy(x => x.InvoiceCode);
        }

        public static IQueryable<T> OrderDefault<T>(this IQueryable<T> q)
        {
            var t = typeof(T);
            var dateProp = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .FirstOrDefault(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            var idProp = t.GetProperty("Id") ?? t.GetProperty("ID");
            if (dateProp != null)
            {
                return idProp != null
                    ? q.OrderByDescending(e => (DateTime?)dateProp.GetValue(e, null) ?? DateTime.MinValue)
                        .ThenBy(e => idProp.GetValue(e, null))
                    : q.OrderByDescending(e => (DateTime?)dateProp.GetValue(e, null) ?? DateTime.MinValue);
            }

            return q.OrderBy(e => idProp!.GetValue(e, null));
        }
    }
}
