using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using InfrStructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Repository.PriceListsRepository
{
    internal static class PriceListCrudValidation
    {
        /// <summary>
        /// بررسی کن ، آیا این سال و یا این رشته در دیتابیس فهرست بها وجود دارد؟
        /// </summary>
        /// <param name="priceList"></param>
        /// <param name="_appDbContext"></param>
        /// <returns></returns>
        public static List<PriceList> CheckExists(List<PriceList> priceList, AppDbContext _appDbContext)
        {
            try
            {
                var listThatShouldAddComplete = new List<PriceList>();
                foreach (var item in priceList)
                {
                    var checkYear = _appDbContext.PriceLists.Where(x => x.Year.Contains(item.Year)).FirstOrDefault();
                    if (checkYear != null)
                    {
                        foreach (var fields in item.PriceListFields)
                        {
                            fields.PriceListID = checkYear.ID;
                            var checkField = _appDbContext.PriceListFields.Where(x => x.FieldTitle.Contains(fields.FieldTitle) && x.PriceListID == checkYear.ID).FirstOrDefault();
                            if (checkField != null)
                            {
                                continue;
                            }
                            _appDbContext.PriceListFields.Add(fields);
                        }
                        continue;
                    }
                    listThatShouldAddComplete.Add(item);
                }
                return listThatShouldAddComplete;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// بررسی کن ببین از این مورد در دیتابیس وجود دارد
        /// اگر وجود داشت اجازه ثبت نده
        /// </summary>
        /// <param name="activitycenterTitle"></param>
        /// <returns>bool</returns>
        public static bool CheckDuplicate(AppDbContext _appDbContext, PriceList priceList)
        {
            try
            {
                var model = _appDbContext.PriceLists.Where(x => x.Year == priceList.Year && x.ID != priceList.ID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        public static bool CheckDuplicate(AppDbContext _appDbContext, PriceListField field)
        {
            try
            {
                var model = _appDbContext.PriceListFields.Where(x => x.FieldTitle == field.FieldTitle && x.ID != field.ID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        public static bool CheckDuplicate(AppDbContext _appDbContext, PriceListClause clause)
        {
            try
            {
                var model = _appDbContext.PriceListClauses.Where(x => x.ClauseTitle == clause.ClauseTitle && x.ID != clause.ID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        public static bool CheckDuplicate(AppDbContext _appDbContext, PriceListExplanation explenation)
        {
            try
            {
                var model = _appDbContext.PriceListExplanations.Where(x => x.Explanation == explenation.Explanation && x.ID != explenation.ID).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }

        }
        /// <summary>
        /// بررسی کن ببین از این مورد در قراردادی استفاده شده 
        /// اگر شده اجازه حذف نده
        /// </summary>
        /// <param name="priceListId"></param>
        /// <returns></returns>
        public static bool CheckIsItUsed(AppDbContext _appDbContext, Guid priceListId)
        {
            try
            {
                var model = _appDbContext.ContractEstimatedmeters.Where(x => x.YearId == priceListId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool CheckIsItUsedField(AppDbContext _appDbContext, Guid fieldId)
        {
            try
            {
                var model = _appDbContext.ContractEstimatedmeters.Where(x => x.FieldId == fieldId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool CheckIsItUsedClaus(AppDbContext _appDbContext, Guid clausId)
        {
            try
            {
                var model = _appDbContext.ContractEstimatedmeters.Where(x => x.ClauseId == clausId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static bool CheckIsItUsedExplenation(AppDbContext _appDbContext, Guid explenationId)
        {
            try
            {
                var model = _appDbContext.ContractEstimatedmeters.Where(x => x.ExplenationId == explenationId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
