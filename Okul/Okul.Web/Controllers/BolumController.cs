using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Okul.Business.Operations.Bolum;
using Okul.Business.Operations.Bolum.Dtos;
using Okul.Business.Operations.Ogrenci;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Data.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Okul.Web.Controllers
{
    public class BolumController : Controller
    {
        private readonly IBolumService _bolumService;

        public BolumController(IBolumService bolumService)
        {
            _bolumService = bolumService;
        }

        public async Task<IActionResult> Index()
        {
            var bolumler = await _bolumService.GetAllBolumler();

            return View(bolumler);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Bolumler bolumler)
        {
            var addBolumDto = new AddBolumDto
           {
               BolumAdi = bolumler.BolumAdi
           };


            var isAdded = await _bolumService.AddBolum(addBolumDto);

            if (isAdded)
                return RedirectToAction("Index");

            return View(bolumler);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var ogrenci = _bolumService.GetById(id);

            if (ogrenci == null)
            {
                return NotFound();
            }

            return View(ogrenci);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Bolumler bolumler)
        {
            var updateBolumDto = new UpdateBolumDto
            {
                BolumId = id,
                BolumAdi = bolumler.BolumAdi
            };

            var result = await _bolumService.UpdateBolum(updateBolumDto);
            if (result)
                return RedirectToAction("Index");
            return View(bolumler);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var bolum = _bolumService.GetById(id);
            if (bolum == null)
                return NotFound();

            bool sonuc = await _bolumService.DeleteBolum(bolum, softDelete: false);

            if (sonuc)
                return RedirectToAction("Index");

            return View(bolum);
        }
    }
}

