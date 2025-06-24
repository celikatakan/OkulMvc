using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Okul.Business.Operations.Ders;
using Okul.Business.Operations.Ders.Dtos;
using Okul.Business.Operations.Not;
using Okul.Business.Operations.Not.Dtos;
using Okul.Business.Operations.Ogrenci;
using Okul.Business.Operations.Ogretmen;
using Okul.Data.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Okul.Web.Controllers
{
    public class NotController : Controller
    {
        private readonly INotService _notService;
        private readonly IOgrenciService _ogrenciService;
        private readonly IDersService _dersService;

        public NotController(INotService notService, IOgrenciService ogrenciService, IDersService dersService)
        {
            _notService = notService;
            _ogrenciService = ogrenciService;
            _dersService = dersService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.ToplamNot = _notService.Count();
            var notlar = await _notService.GetAllNotlar();
            return View(notlar);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var ogrenciler = await _ogrenciService.GetAllAsync();
            ViewBag.Ogrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad");
            var dersler = await _dersService.GetAllAsync();
            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Notlar notlar)
        {
            var addNotDto = new AddNotDto
            {
               OgrenciId=notlar.OgrenciId,
               DersId =notlar.DersId,
               Sinav1 = notlar.Sinav1,
               Sinav2 = notlar.Sinav2,
               FinalNotu = notlar.FinalNotu
            };

            var isAdded = await _notService.AddNot(addNotDto);

            if (isAdded)
                return RedirectToAction("Index");

            var ogrenciler = await _ogrenciService.GetAllAsync();
            ViewBag.Ogrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad", notlar.OgrenciId);

            var dersler = await _dersService.GetAllAsync();
            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi", notlar.DersId);

            ModelState.AddModelError("", "Bu isimde bir öğrenci zaten mevcut.");

            return View(notlar);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var not = _notService.GetById(id);

            if (not == null)
            {
                return NotFound();
            }

            var ogrenciler = await _ogrenciService.GetAllAsync();
            ViewBag.Ogrenciler = new SelectList(ogrenciler, "OgrenciId", "AdSoyad");
            var dersler = await _dersService.GetAllAsync();
            ViewBag.Dersler = new SelectList(dersler, "DersId", "DersAdi");

            return View(not);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Notlar notlar)
        {
            var updateNotDto = new UpdateNotDto
            {
                NotId = id,
                DersId = notlar.DersId,
                OgrenciId = notlar.OgrenciId,
                Sinav1 = notlar.Sinav1,
                Sinav2 = notlar.Sinav2,
                FinalNotu = notlar.FinalNotu
            };

            var result = await _notService.UpdateNot(updateNotDto);
            if (result)
                return RedirectToAction("Index");
            return View(notlar);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var not = _notService.GetById(id);
            if (not == null)
                return NotFound();

            bool sonuc = await _notService.DeleteNot(not, softDelete: false);

            if (sonuc)
                return RedirectToAction("Index");

            return View(not);
        }
    }
}

