using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Okul.Business.Operations.Bolum;
using Okul.Business.Operations.Ders;
using Okul.Business.Operations.Ders.Dtos;
using Okul.Business.Operations.Ogrenci;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Business.Operations.Ogretmen;
using Okul.Data.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Okul.Web.Controllers
{
    public class DersController : Controller
    {
        private readonly IOgretmenService _ogretmenService;
        private readonly IDersService _dersService;

        public  DersController(IOgretmenService ogretmenService, IDersService dersService)
        {
            _ogretmenService = ogretmenService;
            _dersService = dersService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.AktifDers = _dersService.Count();
            ViewBag.ToplamOgretmen = _ogretmenService.Count();
            var dersler = await _dersService.GetAllDersler();
            return View(dersler);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var ogretmenler = await _ogretmenService.GetAllAsync();
            ViewBag.Ogretmenler = new SelectList(ogretmenler, "OgretmenId", "AdSoyad");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Dersler dersler)
        {
            var addDersDto = new AddDersDto
            {
               DersAdi = dersler.DersAdi,
               OgretmenId = dersler.OgretmenId
            };

            var isAdded = await _dersService.AddDers(addDersDto);

            if (isAdded)
                return RedirectToAction("Index");

            var ogretmenler = await _ogretmenService.GetAllAsync();
            ViewBag.Bolumler = new SelectList(ogretmenler, "OgretmenId", "AdSoyad", dersler.OgretmenId);

            ModelState.AddModelError("", "Bu isimde bir öğrenci zaten mevcut.");

            return View(dersler);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var ders = _dersService.GetById(id);

            if (ders == null)
            {
                return NotFound();
            }

            var ogretmenler = await _ogretmenService.GetAllAsync();
            ViewBag.Ogretmenler = new SelectList(ogretmenler, "OgretmenId", "AdSoyad");

            return View(ders);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Dersler dersler)
        {
            var updateDersDto = new UpdateDersDto
            {
                DersId = id,
                DersAdi = dersler.DersAdi,
                OgretmenId = dersler.OgretmenId
                
            };

            var result = await _dersService.UpdateDers(updateDersDto);
            if (result)
                return RedirectToAction("Index");
            return View(dersler);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var ders = _dersService.GetById(id);
            if (ders == null)
                return NotFound();

            bool sonuc = await _dersService.DeleteDers(ders, softDelete: false);

            if (sonuc)
                return RedirectToAction("Index");

            return View(ders);
        }
    }
}

