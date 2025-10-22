using AppCore.Entities.PriceListEntities.PriceListClauses;
using AppCore.Entities.PriceListEntities.PriceListExplanations;
using AppCore.Entities.PriceListEntities.PriceListFields;
using AppCore.Entities.PriceListEntities.PriceLists;
using AppCore.UnitOfWork;
using ApplicationService.Services.ExceptionHandlingService;
using ApplicationService.ServicesContract.ExceptionHandling;
using ApplicationService.ServicesContract.PriceLists;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.PriceListServices
{
    internal static class PriceListExcelService
    {

        public async static Task<List<PriceList>> ImportExcel(Stream file, IErrorLoggerService _errorLoggerService)
        {
            try
            {
                var missionComplete = new List<PriceList>();
                PriceList priceList = new PriceList();
                PriceListField priceListField = new PriceListField();
                PriceListClause priceListClause = new PriceListClause();
                PriceListExplanation priceListExplanation = new PriceListExplanation();
                string Year = "";
                string Field = "";
                string Clause = "";
                bool YearChanged = false;
                bool FieldChanged = false;
                bool ClauseChanged = false;

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var columnName = new List<string>();
                var Datas = new List<string>();
                object model = new List<object>();
                using (var reader = ExcelReaderFactory.CreateReader(file))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    // Read data from each sheet
                    foreach (DataTable table in result.Tables)
                    {

                        foreach (DataRow row in table.Rows)
                        {

                            foreach (DataColumn col in table.Columns)
                            {
                                switch (col.ColumnName.ToLower())
                                {
                                    case "year":
                                        if (row[col].ToString() != Year)
                                        {
                                            Year = row[col].ToString();
                                            priceList = new PriceList();
                                            priceList.ID = Guid.NewGuid();
                                            priceList.Year = Year;
                                            priceList.PriceListFields = new List<PriceListField>();
                                            YearChanged = true;
                                        }
                                        break;
                                    case "field":
                                        if (row[col].ToString() != Field)
                                        {
                                            Field = row[col].ToString();
                                            priceListField = new PriceListField();
                                            priceListField.ID = Guid.NewGuid();
                                            priceListField.FieldTitle = Field;
                                            priceListField.PriceListID = priceList.ID;
                                            priceListField.PriceListClauses = new List<PriceListClause>();
                                            FieldChanged = true;
                                        }
                                        break;
                                    case "clause":
                                        if (row[col].ToString() != Clause)
                                        {
                                            Clause = row[col].ToString();
                                            priceListClause = new PriceListClause();
                                            priceListClause.ID = Guid.NewGuid();
                                            priceListClause.ClauseTitle = Clause;
                                            priceListClause.PriceListFieldID = priceListField.ID;
                                            priceListClause.PriceListExplanations = new List<PriceListExplanation>();
                                            ClauseChanged = true;
                                        }
                                        priceListExplanation = new PriceListExplanation();
                                        priceListExplanation.ID = Guid.NewGuid();
                                        priceListExplanation.PriceListClauseID = priceListClause.ID;
                                        priceListExplanation.IsStar = false;
                                        break;
                                    case "rownumber":
                                        priceListExplanation.RowNumber = row[col].ToString();
                                        break;
                                    case "explanation":
                                        priceListExplanation.Explanation = row[col].ToString();
                                        break;
                                    case "unit":
                                        priceListExplanation.Unit = row[col].ToString();
                                        break;
                                    case "unitamount":
                                        priceListExplanation.UnitPrice = row[col].ToString();
                                        break;
                                }

                            }
                            priceListClause.PriceListExplanations.Add(priceListExplanation);
                            if (ClauseChanged)
                            {
                                priceListField.PriceListClauses.Add(priceListClause);
                            }
                            if (FieldChanged)
                            {
                                priceList.PriceListFields.Add(priceListField);
                            }
                            if (YearChanged)
                            {
                                missionComplete.Add(priceList);
                            }
                            FieldChanged = false;
                            ClauseChanged = false;
                            YearChanged = false;
                        }
                    }

                }
                return missionComplete;
            }
            catch(Exception ex)
            {
                _errorLoggerService.SaveError(ex);
                return new List<PriceList>();
            }
        }
    }
}
