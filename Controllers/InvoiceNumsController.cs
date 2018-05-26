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
using Fresenius.Models;

namespace Fresenius.Controllers
{
    public class InvoiceNumsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceNumsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceNums
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceNum.ToListAsync());
        }

        // GET: InvoiceNums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceNum = await _context.InvoiceNum
                .SingleOrDefaultAsync(m => m.Id == id);

            var spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).Include(i => i.InvoiceSparepartNums).ToList();
            var listSparepartsInInvoiceNum = new List<Sparepart>();
            foreach (var sparepart in spareparts)
            {
                foreach (var item in sparepart.InvoiceSparepartNums)
                {
                    if (item.InvoiceNumId == id)
                    {
                        listSparepartsInInvoiceNum.Add(sparepart);
                    }
                }
            }
            ViewBag.listSparepartsInInvoice = listSparepartsInInvoiceNum;

            if (invoiceNum == null)
            {
                return NotFound();
            }

            return View(invoiceNum);
        }

        // GET: InvoiceNums/Create
        public IActionResult Create()
        {
            ViewBag.Spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).ToList();
            return View();
        }

        // POST: InvoiceNums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Num,Date,Sender,Recipient")] InvoiceNum invoiceNum, string[] markSparepartsInInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceNum);
                await _context.SaveChangesAsync();
                foreach (var item in markSparepartsInInvoice)
                {
                    int idSparePart;
                    string numSpareParts;
                    Char delimiter = '|';
                    String[] substrings = item.Split(delimiter);
                    idSparePart = Int32.Parse(substrings[0]);
                    numSpareParts = substrings[1];

                    InvoiceSparepartNum invoiceSparepartNum = new InvoiceSparepartNum { InvoiceNumId = _context.InvoiceNums.LastOrDefault().Id, SparepartId = idSparePart, Num = numSpareParts };
                    _context.Add(invoiceSparepartNum);
                }
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            return View(invoiceNum);
        }

        // GET: InvoiceNums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceNum = await _context.InvoiceNum.SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceNum == null)
            {
                return NotFound();
            }
            ViewBag.Spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).ToList();

            var SparapartsInVoiceNum = _context.InvoiceSparepartNums.Where(i => i.InvoiceNumId == id).ToList();

            Dictionary<int, string> sparapartNum = new Dictionary<int, string>();
            foreach (var item in SparapartsInVoiceNum)
            {
                sparapartNum.Add(item.SparepartId, item.Num);
            }

            ViewBag.SparapartsInVoiceNum = sparapartNum;

            foreach (var item in ViewBag.Spareparts)
                if (ViewBag.SparapartsInVoiceNum.ContainsKey(item.Id) == true)
                {
                    int i = item.Id;
                }


                        return View(invoiceNum);
        }

        // POST: InvoiceNums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Num,Date,Sender,Recipient")] InvoiceNum invoiceNum, string[] markSparepartsInInvoice)
        {
            if (id != invoiceNum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceNum);
                    await _context.SaveChangesAsync();

                    List<InvoiceSparepartNum> listForDell = _context.InvoiceSparepartNums.Where(i => i.InvoiceNumId == id).ToList();
                    _context.InvoiceSparepartNums.RemoveRange(listForDell);

                    foreach (var item in markSparepartsInInvoice)
                    {
                        int idSparePart;
                        string numSpareParts;
                        Char delimiter = '|';
                        String[] substrings = item.Split(delimiter);
                        idSparePart = Int32.Parse(substrings[0]);
                        numSpareParts = substrings[1];

                        InvoiceSparepartNum invoiceSparepartNum = new InvoiceSparepartNum { InvoiceNumId = _context.InvoiceNums.LastOrDefault().Id, SparepartId = idSparePart, Num = numSpareParts };
                        _context.Add(invoiceSparepartNum);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceNumExists(invoiceNum.Id))
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
            return View(invoiceNum);
        }

        // GET: InvoiceNums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceNum = await _context.InvoiceNum
                .SingleOrDefaultAsync(m => m.Id == id);
            if (invoiceNum == null)
            {
                return NotFound();
            }

            return View(invoiceNum);
        }

        // POST: InvoiceNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceNum = await _context.InvoiceNum.SingleOrDefaultAsync(m => m.Id == id);
            _context.InvoiceNum.Remove(invoiceNum);

            List<InvoiceSparepartNum> listForDell = _context.InvoiceSparepartNums.Where(i => i.InvoiceNumId == id).ToList();
            _context.InvoiceSparepartNums.RemoveRange(listForDell);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceNumExists(int id)
        {
            return _context.InvoiceNum.Any(e => e.Id == id);
        }

        /// <summary>
        /// Добавление запчати в Инвойс
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddSparepart()
        {
            List<Sparepart> spareparts = await _context.Spareparts.ToListAsync();

            return View(spareparts);
        }


        public async Task<FileResult> DetailsPdf(int? id, string[] column)
        {
            var invoiceNum = await _context.InvoiceNums.SingleOrDefaultAsync(m => m.Id == id);
            var spareparts = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer).Include(i => i.InvoiceSparepartNums).ToList();


            // var listSparepartsInInvoiceNum = new List<Sparepart>();
            List<ViewNumSparepart> viewNumSpareparts = new List<ViewNumSparepart>();


            foreach (var sparepart in spareparts)
            {
                foreach (var item in sparepart.InvoiceSparepartNums.OrderByDescending(s=>s.Num))
                {
                    if (item.InvoiceNumId == id)
                    {
                        viewNumSpareparts.Add(new ViewNumSparepart { Num = item.Num, Sparepart = sparepart });
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
            pdfDoc.Header = new HeaderFooter(new Phrase("Invoice: " + invoiceNum.Num), false);
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
            st = invoiceNum.Num + " / " + invoiceNum.Date;
            pdfCell = new PdfPCell(new Phrase(@st, tahomaFontBold))
            {
                Border = 0
            };
            table.AddCell(pdfCell);
            table.AddCell(pdfCellNull);
            st = "Отправитель: " + invoiceNum.Sender;
            pdfCell = new PdfPCell(new Phrase(@st, tahomaFont))
            {
                Border = 0
            };
            table.AddCell(pdfCell);
            table.AddCell(pdfCellNull);
            table.AddCell(pdfCellNull);
            table.AddCell(pdfCellNull);
            st = "Получатель: " + invoiceNum.Recipient;
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
            foreach (var item in viewNumSpareparts.OrderBy(x => x.Num))
            {
                var tahomaBold = tahomaFontBold;
                DateTime date = new DateTime();
                date = ParseDate(item.Sparepart.Equipment.Ps);
                if (dateTimenow > date) tahomaBold = tahomaFontRed;
                else
                    tahomaBold = tahomaFontBold;
                table = new PdfPTable(2)
                {
                    KeepTogether = true
                };
                iter++;
                //p = new Paragraph("" + item.FullName, tahomaFont); pdfDoc.Add(p);   
                nameImg = item.Sparepart.Image.Split("/");
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


                st =item.Num +" "+ item.Sparepart.Number + " " + item.Sparepart.NameEn + " / " + item.Sparepart.NameRus + "/" + item.Sparepart.Country.Nam;

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
                    st = st + " Описание: " + item.Sparepart.Description;
                }

                if (column.Contains("7"))
                {
                    st = st + " Используется в оборудовании: " + item.Sparepart.Equipment.Name;
                }
                if (column.Contains("8"))
                {
                    st = st + " Код: " + item.Sparepart.Equipment.Code;
                }
                if (column.Contains("9"))
                {
                    st = st + " " + item.Sparepart.Equipment.Description;
                }

                if (column.Contains("10"))
                {
                    st = st + " Рег.номер: " + item.Sparepart.Equipment.RegNumber;
                }

                if (column.Contains("11"))
                {
                    st = st + " Действительно до: " + item.Sparepart.Equipment.Ps;
                }

                pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                {
                    Border = 0,

                };
                table.AddCell(pdfCell);
                st = "";
                if (column.Contains("13"))
                {
                    st = st + "Изготовитель " + item.Sparepart.Manufacturer.NameFull + "/" + item.Sparepart.Manufacturer.NameShort;
                }
                if (column.Contains("15"))
                {
                    st = st + "Код изготовителя " + item.Sparepart.Manufacturer.Code;
                }
                if (column.Contains("16"))
                {
                    st = st + "Адрес " + item.Sparepart.Manufacturer.AdressOfDeparture;
                }
                pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                {
                    Border = 0,
                    Colspan = 2
                };
                table.AddCell(pdfCell);
                if (column.Contains("17") && (column.Contains("12")))
                {
                    nameImg = item.Sparepart.Manufacturer.Logo.Split("/");
                    pdfCell = new PdfPCell()
                    {
                        Border = 0,
                        Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                    };
                    table.AddCell(pdfCell);

                    nameImg = item.Sparepart.Equipment.Image.Split("/");
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
                        nameImg = item.Sparepart.Equipment.Image.Split("/");
                        pdfCell = new PdfPCell()
                        {
                            Border = 0,
                            Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                        };
                        table.AddCell(pdfCell);
                        st = "";
                        st = st + item.Sparepart.Equipment.Name;
                        st = st + " Код: " + item.Sparepart.Equipment.Code;
                        st = st + " " + item.Sparepart.Equipment.Description;
                        st = st + " Рег.номер: " + item.Sparepart.Equipment.RegNumber;
                        st = st + " Действительно до: " + item.Sparepart.Equipment.Ps;
                        pdfCell = new PdfPCell(new Phrase(st, tahomaFont))
                        {
                            Border = 0
                        };
                        table.AddCell(pdfCell);
                    }
                    if (column.Contains("17"))
                    {
                        nameImg = item.Sparepart.Manufacturer.Logo.Split("/");
                        pdfCell = new PdfPCell()
                        {
                            Border = 0,
                            Image = Image.GetInstance(MyPDF.GetImagePath(nameImg[2]))
                        };
                        table.AddCell(pdfCell);
                        st = "";
                        st = st + "" + item.Sparepart.Manufacturer.NameFull + "/" + item.Sparepart.Manufacturer.NameShort;
                        st = st + "Код  " + item.Sparepart.Manufacturer.Code;
                        st = st + " " + item.Sparepart.Manufacturer.AdressOfDeparture;
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
            string file_name = "Invoice" + invoiceNum.Num + invoiceNum.Date + ".pdf";
            return File(pdfBytes, file_type, file_name);
        }
        private DateTime ParseDate(string dateString)
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
