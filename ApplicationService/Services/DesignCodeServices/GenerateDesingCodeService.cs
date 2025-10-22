using ApplicationService.DtoModels.DesignCodeDtos.FieldObjectDtos;
using ApplicationService.ServicesContract.DesignCode;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.DesignCodeServices
{
    public static class GenerateDesingCodeService 
    {
        
        static List<FieldObjectDto> staticMin = new List<FieldObjectDto>();
        static List<FieldObjectDto> globalCounterData = new List<FieldObjectDto>();
        /// <summary>
        /// ساخت الگوی کد قرارداد
        /// </summary>
        /// <param name="desingCode"></param>
        /// <param name="lastCounter"></param>
        /// <param name="contractType"></param>
        /// <param name="roleOforganization"></param>
        /// <param name="organizationUnit"></param>
        /// <returns></returns>
        public static string GenerateContractDesingCodeAsync(string desingCode, int lastCounter,string contractType, string roleOforganization,string organizationUnit)
        {
            try
            {
                string code = @"order[%keyValue%]1[%&&%]field[%keyValue%]amounts[%keyValueValue%]year[%&%]xxxx[%obj%]
order[%keyValue%]2[%&&%]field[%keyValue%]amounts[%keyValueValue%]month[%&%]xx[%obj%]
order[%keyValue%]3[%&&%]field[%keyValue%]seprator[%keyValueValue%]*[%&%][%obj%]
order[%keyValue%]4[%&&%]field[%keyValue%]text[%keyValueValue%]title[%&%][%obj%]
order[%keyValue%]5[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]5[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]6[%&&%]field[%keyValue%]text[%keyValueValue%]title2[%&%][%obj%]
order[%keyValue%]7[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]8[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]
order[%keyValue%]9[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]0026[%then%]max[%&%][%then%]toNext[%&%]false[%obj%]
order[%keyValue%]10[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]11[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]";

                // REMOVE BOTH \r and \n to match JS behavior
                code = code.Replace("\r", "").Replace("\n", "");

                var objs = code.Split(new[] { "[%obj%]" }, StringSplitOptions.None);

                var allKeys = new List<FieldObjectDto>();

                foreach (var raw in objs)
                {
                    var objStr = (raw ?? "").Trim();
                    if (string.IsNullOrEmpty(objStr)) continue;

                    var parts = objStr.Split(new[] { "[%&&%]" }, StringSplitOptions.None);
                    var obj = new FieldObjectDto();

                    foreach (var rawPart in parts)
                    {
                        var part = (rawPart ?? "").Trim();
                        if (part.Length == 0) continue;

                        var kv = part.Split(new[] { "[%keyValue%]" }, StringSplitOptions.None);
                        var key = (kv[0] ?? "").Trim();
                        var value = kv.Length > 1 ? (kv[1] ?? "").Trim() : "";

                        if (key == "field")
                        {
                            var fsplit = value.Split(new[] { "[%keyValueValue%]" }, StringSplitOptions.None);
                            var fieldValue = (fsplit[0] ?? "").Trim();
                            obj.Field = fieldValue;

                            var fieldShortKey = fsplit.Length > 1 ? (fsplit[1] ?? "").Trim() : "";

                            if (fieldValue == "amounts")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.ShortKey = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                                obj.Type = ss.Length > 1 ? (ss[1] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "seprator" || fieldValue == "text")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.Value = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "counter")
                            {
                                var partsCounter = fieldShortKey.Split(new[] { "[%then%]" }, StringSplitOptions.None);

                                string takeAfterAmp(string s)
                                {
                                    var p = (s ?? "").Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                    return p.Length > 1 ? (p[1] ?? "").Trim() : "";
                                }

                                obj.Min = partsCounter.Length > 0 ? takeAfterAmp(partsCounter[0]) : "";
                                obj.Max = partsCounter.Length > 1 ? takeAfterAmp(partsCounter[1]) : "";
                                obj.ToNext = partsCounter.Length > 2 ? takeAfterAmp(partsCounter[2]) : "";
                            }
                        }
                        else if (key == "order")
                        {
                            obj.Order = value; // now correctly parsed (no leading \r)
                        }
                    }

                    allKeys.Add(obj);
                }

                var counters = allKeys.Where(x => x.Field == "counter").ToList();

                // seed staticMin and a copy for wrap logic
                staticMin = counters.Select(c => new FieldObjectDto { Order = c.Order, Min = c.Min }).ToList();
                globalCounterData = counters.Select(c => new FieldObjectDto
                {
                    Order = c.Order,
                    Min = c.Min,
                    Max = c.Max,
                    ToNext = c.ToNext
                }).ToList();

                var calculatedValues = CalculateCounter(allKeys).OrderBy(x=>x.Order).ToList();
                return "test";
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ساخت الگوی کد صورت وضعیت
        /// </summary>
        /// <param name="desingCode"></param>
        /// <param name="lastCounter"></param>
        /// <param name="contractType"></param>
        /// <param name="roleOforganization"></param>
        /// <param name="organizationUnit"></param>
        /// <param name="InvoiceType"></param>
        /// <returns></returns>
        public static string GenerateInvoiceDesingCodeAsync(string desingCode, int lastCounter, string contractType, string roleOforganization, string organizationUnit, string InvoiceType)
        {
            try
            {
                string code = @"order[%keyValue%]1[%&&%]field[%keyValue%]amounts[%keyValueValue%]year[%&%]xxxx[%obj%]
order[%keyValue%]2[%&&%]field[%keyValue%]amounts[%keyValueValue%]month[%&%]xx[%obj%]
order[%keyValue%]3[%&&%]field[%keyValue%]seprator[%keyValueValue%]*[%&%][%obj%]
order[%keyValue%]4[%&&%]field[%keyValue%]text[%keyValueValue%]title[%&%][%obj%]
order[%keyValue%]5[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]5[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]6[%&&%]field[%keyValue%]text[%keyValueValue%]title2[%&%][%obj%]
order[%keyValue%]7[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]8[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]
order[%keyValue%]9[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]0026[%then%]max[%&%][%then%]toNext[%&%]false[%obj%]
order[%keyValue%]10[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]11[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]";

                // REMOVE BOTH \r and \n to match JS behavior
                code = code.Replace("\r", "").Replace("\n", "");

                var objs = code.Split(new[] { "[%obj%]" }, StringSplitOptions.None);

                var allKeys = new List<FieldObjectDto>();

                foreach (var raw in objs)
                {
                    var objStr = (raw ?? "").Trim();
                    if (string.IsNullOrEmpty(objStr)) continue;

                    var parts = objStr.Split(new[] { "[%&&%]" }, StringSplitOptions.None);
                    var obj = new FieldObjectDto();

                    foreach (var rawPart in parts)
                    {
                        var part = (rawPart ?? "").Trim();
                        if (part.Length == 0) continue;

                        var kv = part.Split(new[] { "[%keyValue%]" }, StringSplitOptions.None);
                        var key = (kv[0] ?? "").Trim();
                        var value = kv.Length > 1 ? (kv[1] ?? "").Trim() : "";

                        if (key == "field")
                        {
                            var fsplit = value.Split(new[] { "[%keyValueValue%]" }, StringSplitOptions.None);
                            var fieldValue = (fsplit[0] ?? "").Trim();
                            obj.Field = fieldValue;

                            var fieldShortKey = fsplit.Length > 1 ? (fsplit[1] ?? "").Trim() : "";

                            if (fieldValue == "amounts")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.ShortKey = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                                obj.Type = ss.Length > 1 ? (ss[1] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "seprator" || fieldValue == "text")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.Value = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "counter")
                            {
                                var partsCounter = fieldShortKey.Split(new[] { "[%then%]" }, StringSplitOptions.None);

                                string takeAfterAmp(string s)
                                {
                                    var p = (s ?? "").Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                    return p.Length > 1 ? (p[1] ?? "").Trim() : "";
                                }

                                obj.Min = partsCounter.Length > 0 ? takeAfterAmp(partsCounter[0]) : "";
                                obj.Max = partsCounter.Length > 1 ? takeAfterAmp(partsCounter[1]) : "";
                                obj.ToNext = partsCounter.Length > 2 ? takeAfterAmp(partsCounter[2]) : "";
                            }
                        }
                        else if (key == "order")
                        {
                            obj.Order = value; // now correctly parsed (no leading \r)
                        }
                    }

                    allKeys.Add(obj);
                }

                var counters = allKeys.Where(x => x.Field == "counter").ToList();

                // seed staticMin and a copy for wrap logic
                staticMin = counters.Select(c => new FieldObjectDto { Order = c.Order, Min = c.Min }).ToList();
                globalCounterData = counters.Select(c => new FieldObjectDto
                {
                    Order = c.Order,
                    Min = c.Min,
                    Max = c.Max,
                    ToNext = c.ToNext
                }).ToList();

                var calculatedValues = CalculateCounter(allKeys).OrderBy(x => x.Order).ToList();
                return "test";
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ساخت الگوی کد فاکتور
        /// </summary>
        /// <param name="desingCode"></param>
        /// <param name="lastCounter"></param>
        /// <param name="FactorType"></param>
        /// <param name="roleOforganization"></param>
        /// <param name="organizationUnit"></param>
        /// <returns></returns>
        public static string GenerateFactorDesingCodeAsync(string desingCode, int lastCounter, string FactorType, string roleOforganization, string organizationUnit)
        {
            try
            {
                string code = @"order[%keyValue%]1[%&&%]field[%keyValue%]amounts[%keyValueValue%]year[%&%]xxxx[%obj%]
order[%keyValue%]2[%&&%]field[%keyValue%]amounts[%keyValueValue%]month[%&%]xx[%obj%]
order[%keyValue%]3[%&&%]field[%keyValue%]seprator[%keyValueValue%]*[%&%][%obj%]
order[%keyValue%]4[%&&%]field[%keyValue%]text[%keyValueValue%]title[%&%][%obj%]
order[%keyValue%]5[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]5[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]6[%&&%]field[%keyValue%]text[%keyValueValue%]title2[%&%][%obj%]
order[%keyValue%]7[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]8[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]
order[%keyValue%]9[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]0026[%then%]max[%&%][%then%]toNext[%&%]false[%obj%]
order[%keyValue%]10[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]11[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]";

                // REMOVE BOTH \r and \n to match JS behavior
                code = code.Replace("\r", "").Replace("\n", "");

                var objs = code.Split(new[] { "[%obj%]" }, StringSplitOptions.None);

                var allKeys = new List<FieldObjectDto>();

                foreach (var raw in objs)
                {
                    var objStr = (raw ?? "").Trim();
                    if (string.IsNullOrEmpty(objStr)) continue;

                    var parts = objStr.Split(new[] { "[%&&%]" }, StringSplitOptions.None);
                    var obj = new FieldObjectDto();

                    foreach (var rawPart in parts)
                    {
                        var part = (rawPart ?? "").Trim();
                        if (part.Length == 0) continue;

                        var kv = part.Split(new[] { "[%keyValue%]" }, StringSplitOptions.None);
                        var key = (kv[0] ?? "").Trim();
                        var value = kv.Length > 1 ? (kv[1] ?? "").Trim() : "";

                        if (key == "field")
                        {
                            var fsplit = value.Split(new[] { "[%keyValueValue%]" }, StringSplitOptions.None);
                            var fieldValue = (fsplit[0] ?? "").Trim();
                            obj.Field = fieldValue;

                            var fieldShortKey = fsplit.Length > 1 ? (fsplit[1] ?? "").Trim() : "";

                            if (fieldValue == "amounts")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.ShortKey = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                                obj.Type = ss.Length > 1 ? (ss[1] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "seprator" || fieldValue == "text")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.Value = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "counter")
                            {
                                var partsCounter = fieldShortKey.Split(new[] { "[%then%]" }, StringSplitOptions.None);

                                string takeAfterAmp(string s)
                                {
                                    var p = (s ?? "").Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                    return p.Length > 1 ? (p[1] ?? "").Trim() : "";
                                }

                                obj.Min = partsCounter.Length > 0 ? takeAfterAmp(partsCounter[0]) : "";
                                obj.Max = partsCounter.Length > 1 ? takeAfterAmp(partsCounter[1]) : "";
                                obj.ToNext = partsCounter.Length > 2 ? takeAfterAmp(partsCounter[2]) : "";
                            }
                        }
                        else if (key == "order")
                        {
                            obj.Order = value; // now correctly parsed (no leading \r)
                        }
                    }

                    allKeys.Add(obj);
                }

                var counters = allKeys.Where(x => x.Field == "counter").ToList();

                // seed staticMin and a copy for wrap logic
                staticMin = counters.Select(c => new FieldObjectDto { Order = c.Order, Min = c.Min }).ToList();
                globalCounterData = counters.Select(c => new FieldObjectDto
                {
                    Order = c.Order,
                    Min = c.Min,
                    Max = c.Max,
                    ToNext = c.ToNext
                }).ToList();

                var calculatedValues = CalculateCounter(allKeys).OrderBy(x => x.Order).ToList();
                return "test";
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ساخت الگوی کد درخواست برگزاری
        /// </summary>
        /// <param name="desingCode"></param>
        /// <param name="lastCounter"></param>
        /// <param name="contractType"></param>
        /// <param name="organizationUnit"></param>
        /// <returns></returns>
        public static string GenerateTransactionDesingCodeAsync(string desingCode, int lastCounter, string contractType, string organizationUnit)
        {
            try
            {
                string code = @"order[%keyValue%]1[%&&%]field[%keyValue%]amounts[%keyValueValue%]year[%&%]xxxx[%obj%]
order[%keyValue%]2[%&&%]field[%keyValue%]amounts[%keyValueValue%]month[%&%]xx[%obj%]
order[%keyValue%]3[%&&%]field[%keyValue%]seprator[%keyValueValue%]*[%&%][%obj%]
order[%keyValue%]4[%&&%]field[%keyValue%]text[%keyValueValue%]title[%&%][%obj%]
order[%keyValue%]5[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]5[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]6[%&&%]field[%keyValue%]text[%keyValueValue%]title2[%&%][%obj%]
order[%keyValue%]7[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]8[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]
order[%keyValue%]9[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]0026[%then%]max[%&%][%then%]toNext[%&%]false[%obj%]
order[%keyValue%]10[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]00[%then%]max[%&%]2[%then%]toNext[%&%]true[%obj%]
order[%keyValue%]11[%&&%]field[%keyValue%]counter[%keyValueValue%]min[%&%]000[%then%]max[%&%]2[%then%]toNext[%&%]false[%obj%]";

                // REMOVE BOTH \r and \n to match JS behavior
                code = code.Replace("\r", "").Replace("\n", "");

                var objs = code.Split(new[] { "[%obj%]" }, StringSplitOptions.None);

                var allKeys = new List<FieldObjectDto>();

                foreach (var raw in objs)
                {
                    var objStr = (raw ?? "").Trim();
                    if (string.IsNullOrEmpty(objStr)) continue;

                    var parts = objStr.Split(new[] { "[%&&%]" }, StringSplitOptions.None);
                    var obj = new FieldObjectDto();

                    foreach (var rawPart in parts)
                    {
                        var part = (rawPart ?? "").Trim();
                        if (part.Length == 0) continue;

                        var kv = part.Split(new[] { "[%keyValue%]" }, StringSplitOptions.None);
                        var key = (kv[0] ?? "").Trim();
                        var value = kv.Length > 1 ? (kv[1] ?? "").Trim() : "";

                        if (key == "field")
                        {
                            var fsplit = value.Split(new[] { "[%keyValueValue%]" }, StringSplitOptions.None);
                            var fieldValue = (fsplit[0] ?? "").Trim();
                            obj.Field = fieldValue;

                            var fieldShortKey = fsplit.Length > 1 ? (fsplit[1] ?? "").Trim() : "";

                            if (fieldValue == "amounts")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.ShortKey = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                                obj.Type = ss.Length > 1 ? (ss[1] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "seprator" || fieldValue == "text")
                            {
                                var ss = fieldShortKey.Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                obj.Value = ss.Length > 0 ? (ss[0] ?? "").Trim() : "";
                            }
                            else if (fieldValue == "counter")
                            {
                                var partsCounter = fieldShortKey.Split(new[] { "[%then%]" }, StringSplitOptions.None);

                                string takeAfterAmp(string s)
                                {
                                    var p = (s ?? "").Split(new[] { "[%&%]" }, StringSplitOptions.None);
                                    return p.Length > 1 ? (p[1] ?? "").Trim() : "";
                                }

                                obj.Min = partsCounter.Length > 0 ? takeAfterAmp(partsCounter[0]) : "";
                                obj.Max = partsCounter.Length > 1 ? takeAfterAmp(partsCounter[1]) : "";
                                obj.ToNext = partsCounter.Length > 2 ? takeAfterAmp(partsCounter[2]) : "";
                            }
                        }
                        else if (key == "order")
                        {
                            obj.Order = value; // now correctly parsed (no leading \r)
                        }
                    }

                    allKeys.Add(obj);
                }

                var counters = allKeys.Where(x => x.Field == "counter").ToList();

                // seed staticMin and a copy for wrap logic
                staticMin = counters.Select(c => new FieldObjectDto { Order = c.Order, Min = c.Min }).ToList();
                globalCounterData = counters.Select(c => new FieldObjectDto
                {
                    Order = c.Order,
                    Min = c.Min,
                    Max = c.Max,
                    ToNext = c.ToNext
                }).ToList();

                var calculatedValues = CalculateCounter(allKeys).OrderBy(x => x.Order).ToList();
                return "test";
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static List<FieldObjectDto> CalculateCounter(List<FieldObjectDto> allKeys)
        {
            var counters = allKeys.Where(x => x.Field == "counter").ToList();
            int lastCount = 1000000;
            for (int i = 0; i < counters.Count; i++)
            {
                if (counters[i].ToNext == "true")
                {
                    var orders = new List<string> { counters[i].Order };
                    int j = i + 1;
                    for (; j < counters.Count; j++)
                    {
                        orders.Add(counters[j].Order);
                        if (counters[j].ToNext == "false")
                        {
                            i = j + 1; // mirrors your JS
                            break;
                        }
                    }
                    LinierCounterFast(lastCount, counters, orders);
                }

                if (i <= counters.Count - 1 && counters[i].ToNext == "false")
                {
                    int min = int.Parse(counters[i].Min);
                    int max;
                    bool hasMax = int.TryParse(counters[i].Max, out max);

                    if (!hasMax)
                    {
                        int num = 1;
                        while (num <= lastCount) { num++; min++; }
                        counters[i].Min = min.ToString();
                    }
                    else
                    {
                        int forPlus = 1;
                        while (forPlus <= lastCount)
                        {
                            if (forPlus > max)
                            {
                                min = int.Parse(globalCounterData[i].Min);
                            }
                            else
                            {
                                min++;
                            }
                            forPlus++;
                        }
                        counters[i].Min = min.ToString();
                    }
                }
            }
            SumWithZero(counters);
            return allKeys.Select(k => new FieldObjectDto
            {
                Order = k.Order,
                Field = k.Field,
                Min = k.Min,
                Max = k.Max,
                ToNext = k.ToNext,
                ShortKey = k.ShortKey,
                Type = k.Type,
                Value = k.Value
            }).ToList();
           
        }
        public static void LinierCounter(int lastCount, List<FieldObjectDto> data, List<string> orders)
        {
            orders.Reverse();

            int index = 0;
            var first = data.First(x => x.Order == orders[index]);
            int min = int.Parse(first.Min);
            int max = int.Parse(first.Max);

            while (lastCount > 0)
            {
                if (min < max)
                {
                    min++;
                    data.First(x => x.Order == orders[index]).Min = min.ToString();
                }
                else
                {
                    min = int.Parse(staticMin.First(x => x.Order == orders[index]).Min);
                    data.First(x => x.Order == orders[index]).Min = min.ToString();
                    GoToNext(index, data, orders);
                }
                lastCount--;
            }
        }
        public static void LinierCounterFast(int lastCount, List<FieldObjectDto> data, List<string> orders)
        {
            orders.Reverse();
            // Think of orders as "digits" in a mixed-radix counter.
            int index = 0;
            while (lastCount > 0 && index < orders.Count)
            {
                var obj = data.First(x => x.Order == orders[index]);
                int min = int.Parse(obj.Min);
                int max = string.IsNullOrEmpty(obj.Max) ? int.MaxValue : int.Parse(obj.Max);

                int available = max - min;

                if (available >= lastCount)
                {
                    // We can satisfy all increments in one go
                    obj.Min = (min + lastCount).ToString();
                    lastCount = 0;
                }
                else
                {
                    // Jump to max and reset, then carry leftover
                    obj.Min = staticMin.First(x => x.Order == obj.Order).Min;
                    lastCount -= (available + 1);
                    index++; // cascade to next counter
                }
            }
        }
        public static void GoToNext(int index, List<FieldObjectDto> data, List<string> orders)
        {
            index++;
            if (index > orders.Count - 1) return;

            var cur = data.First(x => x.Order == orders[index]);
            int min = int.Parse(cur.Min);
            string maxStr = cur.Max;

            if (!string.IsNullOrEmpty(maxStr))
            {
                int max = int.Parse(maxStr);
                if (min >= max)
                {
                    cur.Min = staticMin.First(x => x.Order == orders[index]).Min;
                    GoToNext(index, data, orders);
                }
                else
                {
                    min++;
                    cur.Min = min.ToString();
                }
            }
            else
            {
                min++;
                cur.Min = min.ToString();
            }
        }
        public static void SumWithZero(List<FieldObjectDto> data)
        {
            foreach (var s in staticMin)
            {
                int staticLength = s.Min?.Length ?? 0;

                var obj = data.First(x => x.Order == s.Order);
                string current = obj.Min ?? "";
                int numLength = current.Length;

                if (staticLength >= numLength)
                {
                    int count = staticLength - numLength;
                    if (count > 0)
                    {
                        string zeroes = new string('0', count);
                        obj.Min = zeroes + current;
                    }
                }
            }
        }

    }
}
