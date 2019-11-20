using KWKY.Common;
using System;
using Xunit;

namespace XUnitCommonTest
{
    public class ExcelHelperTest
    {
        [Fact]
        public void ExcelExport()
        {
            ExcelHelper excelHelper = new ExcelHelper();

            ExcelSheetData sheet1 = new ExcelSheetData()
            {
                SheetName="aaa",
                HeadArr=new string[]{ "��һ��", "��2��", "��3��", "��444444444444444444444444444444��", "��5��" },
                CellData=new string[][]{ 
                new string[]{ "1",DateTime.Now.ToString("yyyy-MM-dd"),"12312.123","asdasdasdadaqweasdasda","rrrrrrrrrrrr"},
                new string[]{ "1",DateTime.Now.ToString("yyyy-MM-dd"),"12312.123","asdasdasdadaqweasdasda","rrrrrrrrrrrr"},
                }

            };
            ExcelSheetData sheet2 =  new ExcelSheetData()
            {
                SheetName="bbb",
                HeadArr=new string[]{ "��һ��", "��2��", "��3��", "��444444444444444444444444444444��", "��5��" },
                CellData=new string[][]{
                new string[]{ "1",DateTime.Now.ToString("yyyy-MM-dd"),"12312.123","asdasdasdadaqweasdasda","rrrrrrrrrrrr"},
                new string[]{ "1",DateTime.Now.ToString("yyyy-MM-dd"),null, "asdasdasdadaqweasdasdaasdasdasdadaqweasdasdaasdasdasdadaqweasdasdaasdasdasdadaqweasdasdaasdasdasdadaqweasdasda", "rrrrrrrrrrrr"},
                }

            };
            excelHelper.ExportToExcel("D://aaa.xlsx", sheet1, sheet2);

        }

        [Fact]
        public void ReadExcel ()
        {
            ExcelHelper excelHelper = new ExcelHelper();

            var aa = excelHelper.ReadExcel("D://aaa.xlsx");

        }
    }
}
