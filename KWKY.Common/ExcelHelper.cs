using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace KWKY.Common
{
    /// <summary>
    /// 对2003版，最大行数是65536行
    /// 对2007以上版本，最大行数是1048576行  最大列 16384 XFD
    /// The maximum length of cell contents (text) is 32,767 characters
    /// 当前封装 无特定样式 全是字符串 无行列数分页 无单元格合并 无内容长度验证
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 生成Excel
        /// </summary>
        /// <param name="fullPath">newbook.core.xlsx</param>
        /// <param name="sheetData"></param>
        public bool ExportToExcel (string fullPath,params ExcelSheetData[] sheetDataArr)
        {

            IWorkbook workbook = new XSSFWorkbook();
            for ( int i = 0; i < sheetDataArr.Length; i++ )
            {
                ExcelSheetData sheetData = sheetDataArr[i];
                ISheet sheet = workbook.CreateSheet(sheetData.SheetName);

                IFont font = workbook.CreateFont();
                font.FontHeightInPoints = 10;
                font.Boldweight = 700;

                ICellStyle headStyle = workbook.CreateCellStyle();
                headStyle.Alignment = HorizontalAlignment.Left;
                headStyle.SetFont(font);


                IRow headRow = sheet.CreateRow(0);
                WriteRow(headRow, sheetData.HeadArr, headStyle);

                for ( int rowIndex = 0; rowIndex < sheetData.CellData.Length; rowIndex++ )
                {
                    IRow row = sheet.CreateRow(rowIndex+1);
                    string[] rowData = sheetData.CellData[rowIndex];
                    WriteRow(row, rowData,null);
                }
                SetAutoSize(sheet, sheetData.HeadArr.Length);

            }

            try
            {
                using ( var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write) )
                workbook.Write(fs);
                return true;
            }
            catch {
                return false;
            }
            
        }

        private void WriteRow (IRow row,string[] rowData, ICellStyle headStyle)
        {
            for ( int columIndex = 0; columIndex < rowData.Length; columIndex++ )
            {
                var cell = row.CreateCell(columIndex);
                cell.CellStyle = headStyle;
                cell.SetCellValue(rowData[columIndex]);
            }
        }

        private void SetAutoSize (ISheet sheet,int columCount)
        {
            for ( int columIndex = 0; columIndex < columCount; columIndex++ )
            {
                sheet.AutoSizeColumn(columIndex);
            }
        }

        /// <summary>
        /// 读取Excel数据
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public List<ExcelSheetData> ReadExcel (string fullPath)
        {
            List<ExcelSheetData> excelData = new List<ExcelSheetData>();

            using var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read) ;
            IWorkbook workbook = new XSSFWorkbook(fs);

            for ( int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++ )
            {
                ExcelSheetData sheetData = new ExcelSheetData();
                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                sheetData.SheetName = sheet.SheetName;

                IRow headRow = sheet.GetRow(0);
                sheetData.HeadArr = new string[headRow.LastCellNum];
                ReadRow(sheetData.HeadArr, headRow);


                sheetData.CellData = new string[sheet.LastRowNum][];
                for ( int rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++ )
                {
                    sheetData.CellData[rowIndex] = new string[sheetData.HeadArr.Length];
                    IRow row = sheet.GetRow(rowIndex+1);
                    ReadRow(sheetData.CellData[rowIndex], row);
                }
                excelData.Add(sheetData);
            }
            return excelData;

        }

        private void ReadRow (string[] rowData,IRow row)
        {
            for ( int columIndex = 0; columIndex < rowData.Length; columIndex++ )
            {
                rowData[columIndex] = row.GetCell(columIndex).StringCellValue;
            }
        }
    }

    public class ExcelSheetData
    { 
        public string SheetName { get; set; }
        public string[] HeadArr { get; set; }
        public string[][] CellData { get; set; }
    }
}
