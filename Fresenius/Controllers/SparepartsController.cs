using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fresenius.Data;
using Microsoft.AspNetCore.Http;
using System.IO;
using Fresenius.Models;
using Microsoft.AspNetCore.Hosting;

namespace Fresenius.Controllers
{
    public class SparepartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public SparepartsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Spareparts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexDataTable()
        {
            var applicationDbContext = _context.Spareparts.Include(s => s.Country).Include(s => s.Equipment).Include(s => s.Manufacturer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Spareparts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparepart = await _context.Spareparts
                .Include(s => s.Country)
                .Include(s => s.Equipment)
                .Include(s => s.Manufacturer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sparepart == null)
            {
                return NotFound();
            }

            return View(sparepart);
        }

        // GET: Spareparts/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Nam");
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name");
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "NameFull");
            return View();
        }

        // POST: Spareparts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,NameEn,NameRus,EquipmentId,Description,ManufacturerId,CountryId,Image")] Sparepart sparepart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sparepart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Nam", sparepart.CountryId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", sparepart.EquipmentId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "NameFull", sparepart.ManufacturerId);
            
            return View(sparepart);
        }

        // GET: Spareparts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparepart = await _context.Spareparts.SingleOrDefaultAsync(m => m.Id == id);
            if (sparepart == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Nam", sparepart.CountryId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", sparepart.EquipmentId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "NameFull", sparepart.ManufacturerId);
            return View(sparepart);
        }

        // POST: Spareparts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,NameEn,NameRus,EquipmentId,Description,ManufacturerId,CountryId,Image")] Sparepart sparepart)
        {
            if (id != sparepart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparepart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparepartExists(sparepart.Id))
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Nam", sparepart.CountryId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", sparepart.EquipmentId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "NameFull", sparepart.ManufacturerId);
            return View(sparepart);
        }

        // GET: Spareparts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparepart = await _context.Spareparts
                .Include(s => s.Country)
                .Include(s => s.Equipment)
                .Include(s => s.Manufacturer)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (sparepart == null)
            {
                return NotFound();
            }

            return View(sparepart);
        }

        // POST: Spareparts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sparepart = await _context.Spareparts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Spareparts.Remove(sparepart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparepartExists(int id)
        {
            return _context.Spareparts.Any(e => e.Id == id);
        }
       
        [HttpPost]
        public async Task<IActionResult> AddFileImg(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/images/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot    D:\Fresenius\Fresenius\Fresenius\wwwroot\images\5085641.png
               //                                                     D:\Fresenius\Fresenius\Fresenius\wwwroot\Files\__VSDeploymentFailure__.txt
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
      
            }                  
            return RedirectToAction("Create");

        }
    }
}
