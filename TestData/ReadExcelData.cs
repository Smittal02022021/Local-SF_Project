using NUnit.Framework;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using excel = Microsoft.Office.Interop.Excel;

namespace SalesForce_Project.TestData
{
    class ReadExcelData
    {
        public static string directory = TestContext.CurrentContext.TestDirectory + @"\TestData\";
        public static string path = null;
        public static excel.Application xlApp = null;
        public static excel.Workbooks workbooks = null;
        public static excel.Workbook workbook = null;
        public static Hashtable sheets;
        //public string xlFilePath;

        public static int GetRowCount(string Path, string Sheet)
        {
            //string actualfile = directory + Path;
            excel.Application excelApp = new excel.Application();
            excel.Workbook workBook = excelApp.Workbooks.Open(Path);
            excel._Worksheet workSheet = workBook.Worksheets.get_Item(Sheet);
            excel.Range range = workSheet.UsedRange;
            int rowCount = range.Rows.Count;
            workBook.Close();
            excelApp.Quit();
            return rowCount;
        }

       public static string ReadData(string path, string sheet, int col)
        {
            excel.Application excelApp = new excel.Application();
            excel.Workbook workBook = excelApp.Workbooks.Open(path);
            excel._Worksheet workSheet = workBook.Worksheets.get_Item(sheet);
            excel.Range range = workSheet.UsedRange;
            string data = range.Cells[col][2].value;
            workBook.Close();
            excelApp.Quit();
            return data;

        }
        //To read data from multiple row
        public static string ReadDataMultipleRows(string path, string sheet, int row, int col)
        {
            excel.Application excelApp = new excel.Application();
            excel.Workbook workBook = excelApp.Workbooks.Open(path);
            excel._Worksheet workSheet = workBook.Worksheets.get_Item(sheet);
            excel.Range range = workSheet.UsedRange;
            string data = range.Cells[col][row].value;
            workBook.Close();
            excelApp.Quit();
            return data;
        }
        public static bool SetCellData(string xlFilePath, string sheetName, string colName, int rowNumber, string value)
        {
            OpenExcel(xlFilePath);

            int sheetValue = 0;
            int colNumber = 0;

            try
            {
                if (sheets.ContainsValue(sheetName))
                {
                    foreach (DictionaryEntry sheet in sheets)
                    {
                        if (sheet.Value.Equals(sheetName))
                        {
                            sheetValue = (int)sheet.Key;
                        }
                    }

                    excel.Worksheet worksheet = null;
                    worksheet = workbook.Worksheets[sheetValue] as excel.Worksheet;
                    excel.Range range = worksheet.UsedRange;

                    for (int i = 1; i <= range.Columns.Count; i++)
                    {
                        string colNameValue = Convert.ToString((range.Cells[1, i] as excel.Range).Value2);
                        if (colNameValue.ToLower() == colName.ToLower())
                        {
                            colNumber = i;
                            break;
                        }
                    }

                    range.Cells[rowNumber, colNumber] = value;
                    value.ToString();

                    workbook.Save();
                    Marshal.FinalReleaseComObject(worksheet);
                    worksheet = null;

                    CloseExcel(xlFilePath);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static void OpenExcel(string xlFilePath)
        {
            xlApp = new excel.Application();
            workbooks = xlApp.Workbooks;
            workbook = workbooks.Open(xlFilePath);
            sheets = new Hashtable();
            int count = 1;
            // Storing worksheet names in Hashtable.
            foreach (excel.Worksheet sheet in workbook.Sheets)
            {
                sheets[count] = sheet.Name;
                count++;
            }
        }
        public static void CloseExcel(string xlFilePath)
        {
            workbook.Close(false, xlFilePath, null); // Close the connection to workbook
            Marshal.FinalReleaseComObject(workbook); // Release unmanaged object references.
            workbook = null;

            workbooks.Close();
            Marshal.FinalReleaseComObject(workbooks);
            workbooks = null;

            xlApp.Quit();
            Marshal.FinalReleaseComObject(xlApp);
            xlApp = null;
        }
    }
}

