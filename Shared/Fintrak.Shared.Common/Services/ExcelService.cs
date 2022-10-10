using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web;
using System.Runtime.InteropServices;

namespace Fintrak.Shared.Common.Services
{
    public class ExcelService
    {
        static private int rowsPerSheet = 30000;
        static private Row headerRow = new Row();
        static private string fileName = "data.xlsx";

        public ExcelService()
        {
            string filePath = HttpContext.Current.Server.MapPath("~") + fileName;
            //Delete the file if it exists. 
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public ExcelService(string path)
        {
            if (Directory.Exists(path)) Directory.Delete(path, true);
            Directory.CreateDirectory(path);
        }



        public string Export<T>(List<T> tempArticles, string path)
        {
            var accountNo = path.Split('\\')[path.Split('\\').Count() - 1];
            if (accountNo != "") path = path.Replace(accountNo, "");
            if (Directory.Exists(path) && accountNo == "") Directory.Delete(path, true);
            Directory.CreateDirectory(path);

            if (tempArticles.Count > 0)
            {
                var workbook = new HSSFWorkbook();

                //Create new Excel Sheet
                var sheet = workbook.CreateSheet("sheet 1");

                //Create a header row
                var headerRow = sheet.CreateRow(0);

                int i = 0;
                foreach (var prop in tempArticles.FirstOrDefault().GetType().GetProperties())
                {
                    //(Optional) set the width of the columns
                    sheet.SetColumnWidth(i, 20 * 256);


                    headerRow.CreateCell(i).SetCellValue(prop.Name);

                    i++;
                }

                //(Optional) freeze the header row so it is not scrolled
                sheet.CreateFreezePane(1, 1, 1, 1);

                int rowNumber = 1;
                int sheetNumber = 1;
                int workbookNumber = 1;


                string filename = null;
                //Populate the sheet with values from the grid data

                //var currentSheet = sheet;
                foreach (var objArticles in tempArticles)
                {
                    if (rowNumber % rowsPerSheet == 0)
                    {
                        if ((rowNumber / rowsPerSheet) % 2 == 0)
                        {
                            filename = string.Concat("data", workbookNumber++, ".xls");

                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);

                            using (FileStream xfile = new FileStream(Path.Combine(path, filename), FileMode.Create, System.IO.FileAccess.Write))
                            {
                                workbook.Write(xfile);
                            }

                            workbook = null;
                            workbook = new HSSFWorkbook();
                            sheetNumber = 0;
                        }
                        sheet = workbook.CreateSheet(string.Concat("sheet ", ++sheetNumber));
                        headerRow = sheet.CreateRow(0);
                        i = 0;
                        foreach (var prop in objArticles.GetType().GetProperties())
                        {
                            //(Optional) set the width of the columns
                            sheet.SetColumnWidth(i, 20 * 256);


                            headerRow.CreateCell(i).SetCellValue(prop.Name);

                            i++;
                        }
                    }

                    //(Optional) freeze the header row so it is not scrolled
                    sheet.CreateFreezePane(1, 1, 1, 1);

                    var sheetRow = rowNumber / rowsPerSheet < 1 ? rowNumber++ % rowsPerSheet : rowNumber++ % rowsPerSheet + 1;
                    //Create a new Row
                    var row = sheet.CreateRow(sheetRow);

                    //Set the Values for Cells
                    i = 0;
                    foreach (var prop in objArticles.GetType().GetProperties())
                    {
                        var value = ((prop.GetValue(objArticles, null) == null) == true) ? "" : prop.GetValue(objArticles, null).ToString();
                        row.CreateCell(i).SetCellValue(value);
                        i++;
                    }

                }

                filename = accountNo == "" ? string.Concat("data", workbookNumber, ".xls") : string.Concat(accountNo, ".xls");

                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                FileStream xfile2 = new FileStream(Path.Combine(path, filename), FileMode.Create, System.IO.FileAccess.Write);

                using (FileStream xfile = xfile2)
                {
                    workbook.Write(xfile);
                    xfile2 = xfile;
                }

            }

            return "ok";

        }

        public Exception Import<T>(string path2, string tablename, T obj)
        {
            Exception ex = null;

            try
            {

                string sqlConnectionString = @"Data Source=wstc-03\SQLExpress;Initial Catalog=InvestmentFinanceNtwk;Integrated Security=SSPI;AttachDBfilePath=|DataDirectory|\InvestmentFinance.mdf;";

                //Create connection string to Excel work book
                string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path2 + ";Extended Properties=Excel 12.0; Persist Security Info=False";
                //Create Connection to Excel work book
                using (OleDbConnection excelConnection = new OleDbConnection(excelConnectionString))
                {

                    excelConnection.Open();
                    //Create OleDbCommand to fetch data from Excel
                    // Get the name of the first worksheet:
                    DataTable dbSchema = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();



                    // Now we have the table name; proceed as before:
                    using (OleDbDataAdapter cmd = new OleDbDataAdapter("SELECT * FROM[" + firstSheetName + "]", excelConnection)) //" + columns + "
                    {

                        // using (OleDbDataReader dReader = cmd.ExecuteReader()) if cmd were a OleDbCommand
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnectionString))
                            {
                                bulkCopy.DestinationTableName = "dbo." + tablename;

                                dbSchema = new DataTable();

                                foreach (var prop in obj.GetType().GetProperties())
                                {
                                    if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(double)
                                        || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(Boolean) || prop.PropertyType == typeof(DateTime))
                                    {
                                        SqlBulkCopyColumnMapping ColMap = new SqlBulkCopyColumnMapping(prop.Name, prop.Name);
                                        bulkCopy.ColumnMappings.Add(ColMap);
                                        dbSchema.Columns.Add(new DataColumn(prop.Name, prop.PropertyType));
                                    }
                                }

                                cmd.Fill(dbSchema);

                                // Write from the source to the destination.
                                bulkCopy.WriteToServer(dbSchema);
                            }
                        }
                    }


                    excelConnection.Close();

                    // SQL Server Connection String
                }

            }
            catch (Exception err)
            {
                while (!(err.InnerException == null))
                {
                    err = err.InnerException;
                }

                ex = err;
            }



            return ex;

        }


    }
}
