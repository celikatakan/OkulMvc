using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations;
using Okul.Business.Operations.Bolum;
using Okul.Business.Operations.Ders;
using Okul.Business.Operations.Ogrenci;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Data.Models;
using Okul.Data.Repository;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Okul.Web.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly IOgrenciService _ogrenciService;
        private readonly IBolumService _bolumService;
        private readonly IDersService _dersService;

        public OgrenciController(IOgrenciService ogrenciService, IBolumService bolumService, IDersService dersService)
        {
            _ogrenciService = ogrenciService;
            _bolumService = bolumService;
            _dersService = dersService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.ToplamOgrenci = _ogrenciService.Count();
            ViewBag.AktifDers = _dersService.Count();
            ViewBag.BolumSayisi = _bolumService.Count(); 
            var ogrenciler = await _ogrenciService.GetAllOgrenciler();
            return View(ogrenciler);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var bolumler = await _bolumService.GetAllAsync();
            ViewBag.Bolumler = new SelectList(bolumler, "BolumId", "BolumAdi");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Ogrenciler ogrenciler)
        {
            var addOgrenciDto = new AddOgrenciDto
            {
                AdSoyad = ogrenciler.AdSoyad,
                Eposta = ogrenciler.Eposta,
                BolumId = ogrenciler.BolumId
            };

            var isAdded = await _ogrenciService.AddOgrenci(addOgrenciDto);

            if (isAdded)
                return RedirectToAction("Index");

            var bolumler = await _bolumService.GetAllAsync();
            ViewBag.Bolumler = new SelectList(bolumler, "BolumId", "BolumAdi", ogrenciler.BolumId);

            ModelState.AddModelError("", "Bu isimde bir öğrenci zaten mevcut.");

            return View(ogrenciler); 
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var ogrenci = _ogrenciService.GetById(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            var bolumler = await _bolumService.GetAllAsync();
            ViewBag.Bolumler = new SelectList(bolumler, "BolumId", "BolumAdi");

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Ogrenciler ogrenciler)
        {
            var updateOgrenciDto = new UpdateOgrenciDto
            {
                OgrenciId = id,
                AdSoyad = ogrenciler.AdSoyad,
                Eposta = ogrenciler.Eposta,
                BolumId = ogrenciler.BolumId
            };

            var result = await _ogrenciService.UpdateOgrenci(updateOgrenciDto);
            if (result)
                return RedirectToAction("Index");
            return View(ogrenciler);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var ogrenci = _ogrenciService.GetById(id);
            if (ogrenci == null)
                return NotFound();

            bool sonuc = await _ogrenciService.DeleteOgrenci(ogrenci, softDelete: false);

            if (sonuc)
                return RedirectToAction("Index");

            return View(ogrenci);
        }

    }
}

