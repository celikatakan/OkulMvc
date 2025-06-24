using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Okul.Business.Operations.Bolum;
using Okul.Business.Operations.Bolum.Dtos;
using Okul.Business.Operations.Ders;
using Okul.Business.Operations.Ogretmen;
using Okul.Business.Operations.Ogretmen.Dtos;
using Okul.Data.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Okul.Web.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly IOgretmenService _ogretmenService;
        private readonly IDersService _dersService;
        private readonly IBolumService _bolumService;

        public OgretmenController(IOgretmenService ogretmenService, IDersService dersService, IBolumService bolumService)
        {
            _ogretmenService = ogretmenService;
            _dersService = dersService;
            _bolumService = bolumService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ToplamOgretmen = _ogretmenService.Count();
            ViewBag.AktifDers = _dersService.Count();
            ViewBag.BolumSayisi = _bolumService.Count();

            var ogretmenler = await _ogretmenService.GetAllOgretmenler();

            return View(ogretmenler);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Ogretmenler ogretmenler)
        {
            var addOgretmenDto = new AddOgretmenDto
            {
                AdSoyad = ogretmenler.AdSoyad,
                Eposta = ogretmenler.Eposta
            };


            var isAdded = await _ogretmenService.AddOgretmen(addOgretmenDto);

            if (isAdded)
                return RedirectToAction("Index");

            return View(ogretmenler);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var ogretmen = _ogretmenService.GetById(id);

            if (ogretmen == null)
            {
                return NotFound();
            }

            return View(ogretmen);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Ogretmenler ogretmenler)
        {
            var updateOgretmenDto = new UpdateOgretmenDto
            {
              OgretmenId = id,
              AdSoyad = ogretmenler.AdSoyad,
              Eposta = ogretmenler.Eposta
            };

            var result = await _ogretmenService.UpdateOgretmen(updateOgretmenDto);
            if (result)
                return RedirectToAction("Index");
            return View(ogretmenler);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var ogretmen = _ogretmenService.GetById(id);
            if (ogretmen == null)
                return NotFound();

            bool sonuc = await _ogretmenService.DeleteOgretmen(ogretmen, softDelete: false);

            if (sonuc)
                return RedirectToAction("Index");

            return View(ogretmen);
        }
    }
}

