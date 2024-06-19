using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Warehouse.Data;

namespace Warehouse.Controllers;

public class MonitoringController : Controller
{
    private readonly AppDbContext _context;

    public MonitoringController(AppDbContext context)
    {
        _context = context;
    }
    
    // GET: Monitoring
    public async Task<IActionResult> Index(string namaGudang, DateTime? expiredDate)
    {
        var query = _context.Barang.AsQueryable();

        if (!string.IsNullOrEmpty(namaGudang))
        {
            query = query.Where(b => b.Gudang.NamaGudang == namaGudang);
        }

        if (expiredDate.HasValue)
        {
            query = query.Where(b => b.ExpiredDate == expiredDate.Value);
        }

        var barang = await query.Include(b => b.Gudang).ToListAsync();

        return View(barang);
    }
}