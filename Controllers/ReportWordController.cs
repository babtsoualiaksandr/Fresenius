using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Fresenius.Controllers
{
    public class ReportWordController : Controller
    {
        // GET: ReportWord
        public ActionResult GetReportWord()
        {
            //Application appWord = new Application();
            //Document doc = appWord.Documents.Add(Visible: true);
            //Range range = doc.Range();
            //range.Text = "Heloo World";
            ////range.Bold = 20;
            //Table table = doc.Tables.Add(range, 5,5);
            //table.Borders.Enable = 1;


            //foreach (Row row in table.Rows)
            //{
            //    foreach (Cell cell  in row.Cells )
            //    {
            //        if (cell.RowIndex==1)
            //        {
            //            cell.Range.Text = "Прикольная колонка"+ cell.ColumnIndex.ToString();
            //            cell.Range.Bold = 1;
            //            cell.Range.Font.Name = "verdana";
            //            cell.Range.Font.Size = 10;
            //            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            //            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

            //        }
            //        else
            //        {
            //            cell.Range.Text = (cell.RowIndex - 2 + cell.ColumnIndex).ToString();
            //        }

            //    }

            //}

            //doc.Save();
            //appWord.Documents.Open(@"D:\Doc3.docx");
            
             
            //try
            //{
            //    doc.Close();
            //    appWord.Quit();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            
                return View();
        }

        
    }
}