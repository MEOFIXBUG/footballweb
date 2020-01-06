﻿using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infras.Common
{
    public static class CommonUltil
    {
        public class DatatableReq
        {
            public int Id { get; set; }
            public int page { get; set; }
            public int pages { get; set; }
            public int perpage { get; set; }
            public int total { get; set; }
            public string cField { get; set; }
            public string sort { get; set; }
            public string search { get; set; }
        }
        public static DatatableReq GetDatatableReq(HttpRequestBase Request)
        {
            int page = 0;
            if (!string.IsNullOrEmpty(Request.Params["pagination[page]"]))
            {
                int.TryParse(Request.Params["pagination[page]"], out page);
            }

            int pages = 0;
            if (!string.IsNullOrEmpty(Request.Params["pagination[pages]"]))
            {
                int.TryParse(Request.Params["pagination[pages]"], out pages);
            }

            int perpage = 0;
            if (!string.IsNullOrEmpty(Request.Params["pagination[perpage]"]))
            {
                int.TryParse(Request.Params["pagination[perpage]"], out perpage);
            }

            int total = 0;
            if (!string.IsNullOrEmpty(Request.Params["pagination[total]"]))
            {
                int.TryParse(Request.Params["pagination[total]"], out total);
            }

            DatatableReq datatableReq = new DatatableReq();
            datatableReq.page = page;
            datatableReq.pages = pages;
            datatableReq.perpage = perpage;
            datatableReq.total = total;
            datatableReq.cField = Request.Params["sort[field]"] == null ? string.Empty : Request.Params["sort[field]"];
            datatableReq.sort = Request.Params["sort[sort]"] == null ? string.Empty : Request.Params["sort[sort]"];
            datatableReq.search = Request.Params["query[generalSearch]"] == null ? string.Empty : Request.Params["query[generalSearch]"];

            return datatableReq;
        }
        public static IEnumerable<T> ConvertSheetToObjects<T>(this ExcelWorksheet worksheet) where T : new()
        {
            //DateTime Conversion
            var convertDateTime = new Func<double, DateTime>(excelDate =>
            {
                if (excelDate < 1)
                    throw new ArgumentException("Excel dates cannot be smaller than 0.");

                var dateOfReference = new DateTime(1900, 1, 1);

                if (excelDate > 60d)
                    excelDate = excelDate - 2;
                else
                    excelDate = excelDate - 1;
                return dateOfReference.AddDays(excelDate);
            });

            //Get the properties of T
            var tprops = (new T())
                .GetType()
                .GetProperties()
                .ToList();

            //Cells only contains references to cells with actual data
            var groups = worksheet.Cells
                .GroupBy(cell => cell.Start.Row)
                .ToList();

            //Assume the second row represents column data types (big assumption!)
            var types = groups
                .Skip(1)
                .First()
                .Select(rcell => rcell.Value.GetType())
                .ToList();

            //Assume first row has the column names
            var colnames = groups
                .First()
                .Select((hcell, idx) => new { Name = hcell.Value.ToString(), index = idx })
                .Where(o => tprops.Select(p => p.Name).Contains(o.Name))
                .ToList();

            //Everything after the header is data
            var rowvalues = groups
                .Skip(1) //Exclude header
                .Select(cg => cg.Select(c => c.Value).ToList());


            //Create the collection container
            var collection = rowvalues
                .Select(row =>
                {
                    var tnew = new T();
                    colnames.ForEach(colname =>
                    {
                        //This is the real wrinkle to using reflection - Excel stores all numbers as double including int
                        var val = row[colname.index];
                        var type = types[colname.index];
                        var prop = tprops.First(p => p.Name == colname.Name);
                        //If it is numeric it is a double since that is how excel stores all numbers
                        if (type == typeof(double))
                        {
                            //Unbox it
                            var unboxedVal = (double)val;
                            //Debug.WriteLine(unboxedVal);
                            //FAR FROM A COMPLETE LIST!!!
                            if (prop.PropertyType == typeof(Int32))
                                prop.SetValue(tnew, (int)unboxedVal);
                            else if (prop.PropertyType == typeof(double))
                                prop.SetValue(tnew, unboxedVal);
                            else if (prop.PropertyType == typeof(DateTime))
                                prop.SetValue(tnew, convertDateTime(unboxedVal));
                            else
                                throw new NotImplementedException(String.Format("Type '{0}' not implemented yet!", prop.PropertyType.Name));
                        }
                        else
                        {
                            //Its a string
                            prop.SetValue(tnew, val);
                        }
                    });

                    return tnew;
                });


            //Send it back
            return collection;
        }

    }
}
