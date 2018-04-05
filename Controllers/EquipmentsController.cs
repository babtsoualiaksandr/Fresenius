using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fresenius.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using AngleSharp;
using Microsoft.AspNetCore.Authorization;

namespace Fresenius.Controllers
{
    [Authorize(Roles = "admin")]
    public class EquipmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public EquipmentsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        static public async Task<IdentityCard> GetDatarcethby(string InIm)
        {
            string str = InIm.Substring(3);
            // Setup the configuration to support document loading
            var config = Configuration.Default.WithDefaultLoader();
            // Load the names of all The Big Bang Theory episodes from Wikipedia
            var address = "http://rceth.by/Refbank/reestr_medicinskoy_tehniki/details/" + str + "?allproducts=True";
            // Asynchronously get the document in a new context using the configuration
            var document = await BrowsingContext.New(config).OpenAsync(address);
            // This CSS selector gets the desired content
            var cellSelector = ".results table td";
            // Perform the query to get all cells with the content
            var cells = document.QuerySelectorAll(cellSelector);
            // We are only interested in the text - select it with LINQ
            var titles = cells.Select(m => m.TextContent);
            string[] title = new string[titles.Count()];
            int i = 0;
            foreach (var item in titles)
            {
                title[i] = item;
                i++;
            }
            IdentityCard identityCard;
            if (i > 4)
            {
                identityCard = new IdentityCard
                {
                    Number = title[0],
                    DateOfRegistration = DateTime.Parse(title[1]),
                    Expiration = DateTime.Parse(title[2]),
                    Applicant = title[3],
                    Purpose = title[4]
                };
            }
            else
            {
                identityCard = new IdentityCard
                {
                    Number = "-------",
                    DateOfRegistration = DateTime.Parse("01.01.1900"),
                    Expiration = DateTime.Parse("01.01.1900"),
                    Applicant = "------",
                    Purpose = "не найдено"
                };
            };
            return identityCard;
        }




        // GET: Equipments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipments.ToListAsync());
        }

        // GET: Equipments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            ViewBag.DataSite = await GetDatarcethby(equipment.RegNumber);

            return View(equipment);
        }

        // GET: Equipments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,Image,RegNumber,Ps")] Equipment equipment, IFormFile uploadedFile, string btn)
        {
            if (btn == "Info")
            {
                IdentityCard identityCard = await GetDatarcethby(equipment.RegNumber);
                equipment.Ps = identityCard.Expiration.ToShortDateString();
                return View("Create", equipment);
            }
            if (ModelState.IsValid)
            {
                // Загрузить на сервер
                if (uploadedFile != null)
                {
                    // путь к папке Files
                    string path = "/images/" + uploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot    D:\Fresenius\Fresenius\Fresenius\wwwroot\images\5085641.png

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    //сохранить в базе сохраненный путь
                    equipment.Image = "~/images/" + uploadedFile.FileName;

                }


                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipment);
        }

        // GET: Equipments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments.SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }
            return View(equipment);
        }

        // POST: Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Description,Image,RegNumber,Ps")] Equipment equipment, IFormFile uploadedFile, string btn)
        {

            if (btn == "Info")
            {

                IdentityCard identityCard = await GetDatarcethby(equipment.RegNumber);
                equipment.Ps = identityCard.Expiration.ToShortDateString();
                return View("Edit", equipment);

            }


            if (id != equipment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Загрузить на сервер
                    if (uploadedFile != null)
                    {
                        // путь к папке Files
                        string path = "/images/" + uploadedFile.FileName;
                        // сохраняем файл в папку Files в каталоге wwwroot    

                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        equipment.Image = "~/images/" + uploadedFile.FileName;

                    }


                    _context.Update(equipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipmentExists(equipment.Id))
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
            return View(equipment);


        }

        // GET: Equipments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipment = await _context.Equipments
                .SingleOrDefaultAsync(m => m.Id == id);
            if (equipment == null)
            {
                return NotFound();
            }

            return View(equipment);
        }

        // POST: Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipment = await _context.Equipments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Equipments.Remove(equipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipments.Any(e => e.Id == id);
        }
    }
}
