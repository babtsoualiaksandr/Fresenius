using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fresenius.Data; 
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Fresenius.Reports;
using System.Globalization;

namespace Fresenius.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public InvoicesController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }      
        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices
                  .Include(i => i.InvoiceSpareparts)
                  .ThenInclude(i => i.Sparepart)
                  .AsNoTracking()
                  .OrderBy(i => i.Num)
                  .ToListAsync();

            return View(invoices);
        }     
        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id);

            var spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).Include(i => i.InvoiceSpareparts).ToList();
            var listSparepartsInInvoice = new List<Sparepart>();
            foreach (var sparepart in spareparts)
            {
                foreach (var item in sparepart.InvoiceSpareparts)
                {
                    if (item.InvoiceId == id)
                    {
                        listSparepartsInInvoice.Add(sparepart);
                    }
                }
            }
            ViewBag.listSparepartsInInvoice = listSparepartsInInvoice;
            if (invoice == null)
            {
                return NotFound();
            }  
            return View(invoice);
        }     
        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewBag.Spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).ToList();

            return View();
        }    
        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Num,Date,Sender,Recipient")] Invoice invoice, string[] markSparepartsInInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();  
                foreach (var item in markSparepartsInInvoice)
                {
                    InvoiceSparepart invoiceSparepart = new InvoiceSparepart { InvoiceId = _context.Invoices.LastOrDefault().Id, SparepartId = Int32.Parse(item) };
                    _context.Add(invoiceSparepart);   
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }  
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            } 
            ViewBag.Spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).ToList();  
            ViewBag.SparapartsInVoice = _context.InvoiceSpareparts.Where(i => i.InvoiceId == id).Select(i => i.SparepartId).ToList();    
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Num,Date,Sender,Recipient")] Invoice invoice, string[] markSparepartsInInvoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    List<InvoiceSparepart> listForDell = _context.InvoiceSpareparts.Where(i => i.InvoiceId == id).ToList();
                    _context.InvoiceSpareparts.RemoveRange(listForDell);   
                    await _context.SaveChangesAsync();   
                    foreach (var item in markSparepartsInInvoice)
                    {
                        InvoiceSparepart invoiceSparepart = new InvoiceSparepart { InvoiceId = id, SparepartId = Int32.Parse(item) };
                        _context.Add(invoiceSparepart);

                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }   
            return View(invoice);
        }      
        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id);
            _context.Invoices.Remove(invoice);

            List<InvoiceSparepart> listForDell = _context.InvoiceSpareparts.Where(i => i.InvoiceId == id).ToList();
            _context.InvoiceSpareparts.RemoveRange(listForDell);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }   
        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }     
        ///Word 
        ///   
        public async Task<IActionResult> DetailsWord(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id);

            var spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).Include(i => i.InvoiceSpareparts).ToList();
            var listSparepartsInInvoice = new List<Sparepart>();
            foreach (var sparepart in spareparts)
            {
                foreach (var item in sparepart.InvoiceSpareparts)
                {
                    if (item.InvoiceId == id)
                    {
                        listSparepartsInInvoice.Add(sparepart);
                    }
                }
            }    
            if (invoice == null)
            {
                return NotFound();
            }     
            //Create an instance for word app 
            //Application appWord = new Application();
            //Document doc = appWord.Documents.Add(
            //    Visible: true,
            //    DocumentType: Word.WdNewDocumentType.wdNewBlankDocument);    
            //Range range = doc.Range();
            //Paragraph paragraph;    
            //var pathDoc = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Invoice.docx");
            //doc.SaveAs2(pathDoc);
            //paragraph = doc.Paragraphs.Add();
            //paragraph.Range.Text = "ИНВОЙС " + invoice.Num + " / " + invoice.Date;
            //paragraph.Range.InsertParagraphAfter(); 
            //paragraph = doc.Paragraphs.Add();
            //paragraph.Range.Text = " Отправитель " + invoice.Sender;
            //paragraph.Range.InsertParagraphAfter(); 
            //paragraph = doc.Paragraphs.Add();
            //paragraph.Range.Text = " Получатель  " + invoice.Recipient;
            //paragraph.Range.InsertParagraphAfter();       
            //string[] words; 
            //foreach (var item in listSparepartsInInvoice)
            //{
            //    paragraph = doc.Paragraphs.Add();
            //    paragraph.Range.Text = item.NameRus.ToString();
            //    paragraph.Range.InsertParagraphAfter();   
            //    words = item.Image.Split("/");    
            //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", words[2]);     
            //    appWord.Selection.InlineShapes.AddPicture(@path);   
            //}                               
            //doc.Save();
            //appWord.Documents.Open(@pathDoc);    
            //try
            //{
            //    doc.Close();
            //    appWord.Quit();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}  
            return RedirectToAction(nameof(Index)); ;
        }

        public async Task<FileResult> DetailsPdf(int? id, string[] column)
        {      
            var invoice = await _context.Invoices.SingleOrDefaultAsync(m => m.Id == id); 
            var spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).Include(i => i.InvoiceSpareparts).ToList();
            var listSparepartsInInvoice = new List<Sparepart>();
            foreach (var sparepart in spareparts)
            {
                foreach (var item in sparepart.InvoiceSpareparts)
                {
                    if (item.InvoiceId == id)
                    {
                        listSparepartsInInvoice.Add(sparepart);
                    }
                }
            }          
            var ms = new MemoryStream();
            var pdfDoc = new Document(PageSize.A4); 
            PdfWriter.GetInstance(pdfDoc, ms);
            var tahomaFont = MyPDF.GetUnicodeFont("Tahoma", MyPDF.GetTahomaFontPath(), 11, Font.NORMAL, BaseColor.Black);
            var tahomaFontBold = MyPDF.GetUnicodeFont("Tahoma", MyPDF.GetTahomaFontPath(), 13, Font.BOLD, BaseColor.Black);
            var tahomaFontRed = MyPDF.GetUnicodeFont("Tahoma", MyPDF.GetTahomaFontPath(), 13, Font.BOLD, BaseColor.Red);

            pdfDoc.AddAuthor("FreseBel and K"); 
            pdfDoc.Header = new HeaderFooter(new Phrase("Invoice: "+invoice.Num ), false);
            pdfDoc.Open();  
            string st;
            Font font = tahomaFont; 
            PdfPTable table = new PdfPTable(2);  
            var pdfCellNull = new PdfPCell(new Phrase("                 ", tahomaFont))
            {
                Border = 0
            };
            table.AddCell(pdfCellNull);
            var pdfCell = new PdfPCell(new Phrase("ИНВОЙС", tahomaFontBold))
            {
                Border = 0
            };
            table.AddCell(pdfCell);    
            table.AddCell(pdfCellNull);
            st = invoice.Num + " / " + invoice.Date;
            pdfCell = new PdfPCell(new Phrase(@st, tahomaFontBold))
            {
                Border = 0
            };
            table.AddCell(pdfCell);  
            table.AddCell(pdfCellNull);
            st = "Отправитель: " + invoice.Sender;
            pdfCell = new PdfPCell(new Phrase(@st, tahomaFont))
            {
                Border = 0
            };
            table.AddCell(pdfCell); 
            table.AddCell(pdfCellNull);
            table.AddCell(pdfCellNull);     
            table.AddCell(pdfCellNull);
            st = "Получатель: " + invoice.Recipient;
            pdfCell = new PdfPCell(new Phrase(@st, tahomaFont))
            {
                Border = 0,
            };
            table.AddCell(pdfCell);
            table.AddCell(pdfCellNull);
            table.AddCell(pdfCellNull);  
           // Paragraph p;
            pdfDoc.Add(table);

           // Image img;
            string[] nameImg;

            int iter = 0;
            DateTime dateTimenow = new DateTime();
            dateTimenow = DateTime.Now;
            foreach (var item in listSparepartsInInvoice)
            {
                var tahomaBold= tahomaFontBold;
                DateTime date = new DateTime();
                date = ParseDate(item.Equipment.Ps);
                if (dateTimenow > date) tahomaBold = tahomaFontRed;
                else
                    tahomaBold = tahomaFontBold;
                table = new PdfPTable(2)
                {   
                      KeepTogether = true  
                };
                iter++;
                //p = new Paragraph("" + item.FullName, tahomaFont); pdfDoc.Add(p);   
                nameImg = item.Image.Split("/");
                //img = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]));
                //img.ScaleAbsolute(159f, 159f);  
                //pdfDoc.Add(img);
                //p = new Paragraph("" + item.Description, tahomaFont); pdfDoc.Add(p);
                //p = new Paragraph("" + item.Manufacturer, tahomaFont); pdfDoc.Add(p);  
                  
                pdfCell = new PdfPCell(new Phrase("", tahomaBold))
                {
                    Border = 0,
                    Colspan = 2   
                };   
                table.AddCell(pdfCell);

                pdfCell = new PdfPCell(new Phrase("", tahomaBold))
                {
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(pdfCell);

                
                st =  item.Number + " " + item.NameEn + " / " + item.NameRus + "/" + item.Country.Nam;
                
                pdfCell = new PdfPCell(new Phrase(st, tahomaBold))
                {
                    Border = 0,
                    Colspan = 2     
                };
                table.AddCell(pdfCell);

                
                
                pdfCell = new PdfPCell(new Phrase("", tahomaFont))
                {
                    Border = 0,
                    Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                };
                table.AddCell(pdfCell);


                st = "";
                if (column.Contains("4"))
                {
                    st = st + " Описание: " + item.Description;
                }

                if (column.Contains("7"))
                {
                    st = st + " Используется в оборудовании: " + item.Equipment.Name;
                }
                if (column.Contains("8"))
                {
                    st = st + " Код: " + item.Equipment.Code;
                }
                if (column.Contains("9"))
                {
                    st = st + " " + item.Equipment.Description;
                }

                if (column.Contains("10"))
                {
                    st = st + " Рег.номер: " + item.Equipment.RegNumber;
                }

                if (column.Contains("11"))
                {
                    st = st + " Действительно до: " + item.Equipment.Ps;
                }

                pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                {
                    Border = 0,

                };
                table.AddCell(pdfCell);
                st = "";
                if (column.Contains("13"))
                {
                    st = st + "Изготовитель " + item.Manufacturer.NameFull + "/" + item.Manufacturer.NameShort;
                }
                if (column.Contains("15"))
                {
                    st = st + "Код изготовителя " + item.Manufacturer.Code;
                }
                if (column.Contains("16"))
                {
                    st = st + "Адрес " + item.Manufacturer.AdressOfDeparture;
                }   
                pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                {
                    Border = 0,
                    Colspan = 2    
                };
                table.AddCell(pdfCell);    
                if (column.Contains("17") && (column.Contains("12")))
                {
                    nameImg = item.Manufacturer.Logo.Split("/");
                    pdfCell = new PdfPCell()
                    {
                        Border = 0,
                        Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                    };
                    table.AddCell(pdfCell); 

                    nameImg = item.Equipment.Image.Split("/");
                    pdfCell = new PdfPCell()
                    {
                        Border = 0,
                        Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                    };
                    table.AddCell(pdfCell);  
                }
                else
                {
                    if (column.Contains("12"))
                    {
                        nameImg = item.Equipment.Image.Split("/");
                        pdfCell = new PdfPCell()
                        {
                            Border = 0,
                            Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                        };
                        table.AddCell(pdfCell);
                        st = "";
                        st = st + item.Equipment.Name;
                        st = st + " Код: " + item.Equipment.Code;
                        st = st + " " + item.Equipment.Description;
                        st = st + " Рег.номер: " + item.Equipment.RegNumber;
                        st = st + " Действительно до: " + item.Equipment.Ps;
                        pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                        {
                            Border = 0  
                        };
                        table.AddCell(pdfCell);    
                    }
                    if (column.Contains("17"))
                    {
                        nameImg = item.Manufacturer.Logo.Split("/");
                        pdfCell = new PdfPCell()
                        {
                            Border = 0,
                            Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                        };
                        table.AddCell(pdfCell);
                        st = "";
                        st = st + "" + item.Manufacturer.NameFull + "/" + item.Manufacturer.NameShort;
                        st = st + "Код  " + item.Manufacturer.Code;
                        st = st + " " + item.Manufacturer.AdressOfDeparture;
                        pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                        {
                            Border = 0
                        };
                        table.AddCell(pdfCell);  
                    }                   
                } 
                
                
                pdfDoc.Add(table);
              // if (iter%3==0) pdfDoc.NewPage();
            }                      
            pdfDoc.Close();
            var pdfBytes = ms.ToArray();
            ms.Dispose();  
            MyPDF.VerifyPdfFileIsReadable(pdfBytes);   
            string file_type = "application/pdf";
            string file_name = "Invoice" + invoice.Num + invoice.Date + ".pdf";
            return File(pdfBytes, file_type, file_name);  
        }  

        private  DateTime ParseDate(string dateString)
        {
            
            CultureInfo culture;
            DateTimeStyles styles;   
            // Parse a date and time with no styles.
            
            culture = CultureInfo.CreateSpecificCulture("en-US");
            styles = DateTimeStyles.None;
            try
            {
                return DateTime.Parse(dateString, culture, styles);
                
            }
            catch (FormatException)
            {
                return DateTime.Now;                      
            }             
        }     
    }
}
