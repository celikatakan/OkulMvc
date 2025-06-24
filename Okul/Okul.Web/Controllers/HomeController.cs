using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Ogrenci;
using Okul.Data.Models;
using Okul.Web.Models;
using Okul.Web.Models.ViewModels;

namespace Okul.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly OkulDbContext _context;

    public HomeController(ILogger<HomeController> logger, IOgrenciService ogrenciService, OkulDbContext context)
    {
        _logger = logger;
        _context = context;
       
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> Report()
    {
        var bolumOgrenciSayilari = await _context.Bolumlers
            .Select(b => new BolumOgrenciSayisiDto
            {
                BolumAdi = b.BolumAdi,
                OgrenciSayisi = b.Ogrencilers.Count
            }).ToListAsync();

        var ogretmenDersSayilari = await _context.Ogretmenlers
            .Select(o => new OgretmenDersSayisiDto
            {
                OgretmenAdi = o.AdSoyad,
                DersSayisi = o.Derslers.Count
            }).ToListAsync();

        var ogrenciler = await _context.Ogrencilers
            .Include(o => o.Notlars)
            .ToListAsync();

        var ogrenciNotOrtalamalari = ogrenciler.Select(o => new OgrenciNotOrtalamasiDto
        {
            OgrenciAdi = o.AdSoyad,
            Ortalama = o.Notlars.Any()
                ? o.Notlars.Average(n => ((n.Sinav1 ?? 0) + (n.Sinav2 ?? 0) + (n.FinalNotu ?? 0)) / 3)
                : 0
        }).ToList();

        var dersOrtalamaNotlari = await _context.Derslers
            .Select(d => new DersOrtalamaDto
            {
                DersAdi = d.DersAdi,
                OrtalamaNot = d.Notlars.Any()
                    ? d.Notlars.Average(n => ((n.Sinav1 ?? 0) + (n.Sinav2 ?? 0) + (n.FinalNotu ?? 0)) / 3)
                    : 0
            }).ToListAsync();

        var finalNotuDusukOgrenciler = await _context.Notlars
     .Include(n => n.Ogrenci)
     .Include(n => n.Ders)
     .Where(n => (n.FinalNotu ?? 0) < 50)
     .Select(n => new FinalNotuDusukOgrenciDto
     {
         OgrenciAdi = n.Ogrenci.AdSoyad,
         DersAdi = n.Ders.DersAdi,
         FinalNotu = n.FinalNotu ?? 0
     }).ToListAsync();
        var model = new ReportViewModel
        {
            BolumOgrenciSayilari = bolumOgrenciSayilari,
            OgretmenDersSayilari = ogretmenDersSayilari,
            OgrenciNotOrtalamalari = ogrenciNotOrtalamalari,
            DersOrtalamaNotlari = dersOrtalamaNotlari,
            FinalNotuDusukOgrenciler = finalNotuDusukOgrenciler
        };

        return View(model);
    }


    public IActionResult About()
    {
        return View();
    }
}

