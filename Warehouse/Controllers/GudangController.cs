using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

namespace Warehouse.Controllers;

public class GudangController : Controller
{
    private readonly AppDbContext _context;

    public GudangController(AppDbContext context)
    {
        _context = context;
    }
    
    // GET: Gudang
    public async Task<IActionResult> Index()
    {
        return View(await _context.Gudang.ToListAsync());
    }
    
    // GET: Gudang/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Gudang/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Gudang gudang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(gudang);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View();
    }
    
    // GET: Gudang/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gudang = await _context.Gudang.FindAsync(id);
        if (gudang == null)
        {
            return NotFound();
        }
        return View(gudang);
    }
    
    // POST: Gudang/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,KodeGudang,NamaGudang,AlamatGudang")] Gudang gudang)
    {
        if (id != gudang.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(gudang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GudangExists(gudang.Id))
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
        return View(gudang);
    }

    // GET: Gudang/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gudang = await _context.Gudang
            .FirstOrDefaultAsync(m => m.Id == id);
        if (gudang == null)
        {
            return NotFound();
        }

        return View(gudang);
    }

    // POST: Gudang/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var gudang = await _context.Gudang.FindAsync(id);
        _context.Gudang.Remove(gudang);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool GudangExists(int id)
    {
        return _context.Gudang.Any(e => e.Id == id);
    }
}