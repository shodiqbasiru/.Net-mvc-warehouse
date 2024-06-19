using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;
using Warehouse.Models;

public class BarangController : Controller
{
    private readonly AppDbContext _context;

    public BarangController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Barang
    public async Task<IActionResult> Index()
    {
        var context = _context.Barang.Include(b => b.Gudang);
        return View(await context.ToListAsync());
    }

    // GET: Barang/Create
    public IActionResult Create()
    {
        ViewData["GudangId"] = new SelectList(_context.Gudang, "Id", "NamaGudang");
        return View();
    }

    // POST: Barang/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,KodeBarang,NamaBarang,HargaBarang,JumlahBarang,ExpiredDate,GudangId")] Barang barang)
    {
        if (ModelState.IsValid)
        {
            _context.Add(barang);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewData["GudangId"] = new SelectList(_context.Gudang, "Id", "NamaGudang", barang.GudangId);
        return View(barang);
    }

    // GET: Barang/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var barang = await _context.Barang.FindAsync(id);
        if (barang == null)
        {
            return NotFound();
        }

        ViewData["GudangId"] = new SelectList(_context.Gudang, "Id", "NamaGudang", barang.GudangId);
        return View(barang);
    }

    // POST: Barang/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,KodeBarang,NamaBarang,HargaBarang,JumlahBarang,ExpiredDate,GudangId")] Barang barang)
    {
        if (id != barang.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(barang);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarangExists(barang.Id))
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

        ViewData["GudangId"] = new SelectList(_context.Gudang, "Id", "NamaGudang", barang.GudangId);
        return View(barang);
    }

    // GET: Barang/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var barang = await _context.Barang
            .Include(b => b.Gudang)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (barang == null)
        {
            return NotFound();
        }

        return View(barang);
    }

    // POST: Barang/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var barang = await _context.Barang.FindAsync(id);
        _context.Barang.Remove(barang);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BarangExists(int id)
    {
        return _context.Barang.Any(e => e.Id == id);
    }
}