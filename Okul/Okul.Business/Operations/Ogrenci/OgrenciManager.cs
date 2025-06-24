using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Business.Types;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

namespace Okul.Business.Operations.Ogrenci
{
    public class OgrenciManager : IOgrenciService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Ogrenciler> _repository;

        public OgrenciManager(IUnitOfWork unitOfWork, IRepository<Ogrenciler> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddOgrenci(AddOgrenciDto ogrenci)
        {
            var hasOgrenci = _repository.GetAll(x => x.AdSoyad.ToLower() == ogrenci.AdSoyad.ToLower()).Any();

            if (hasOgrenci)
                return false;

            var ogrenciEntity = new Ogrenciler
            {
                AdSoyad = ogrenci.AdSoyad,
                Eposta = ogrenci.Eposta,
                BolumId = ogrenci.BolumId
            };

            _repository.Add(ogrenciEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu." + ex);
            }
            return true;
        }

        public int Count()
        {
            return _repository.Count();
        }

        public async Task<bool> DeleteOgrenci(Ogrenciler ogrenci, bool softDelete = true)
        {
            _repository.Delete(ogrenci, softDelete);

            try
            {
                 await _unitOfWork.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu: " + ex.Message);
            }
            return true;
        }

        public async Task<IEnumerable<Ogrenciler>> GetAllAsync()
        {
            return await _repository.GetAll()
                            .Include(x => x.Bolum)
                            .ToListAsync();
        }

        public async Task<List<Ogrenciler>> GetAllOgrenciler()
        {
            var ogrenciler = await _repository.GetAll()
                            .Include(x => x.Bolum)
                            .ToListAsync();

            return ogrenciler;
        }

        public Ogrenciler GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<bool> UpdateOgrenci(UpdateOgrenciDto ogrenci)
        {
            var ogrenciEntity = _repository.GetById(ogrenci.OgrenciId);

            if (ogrenciEntity == null)
                return false;

            ogrenciEntity.AdSoyad = ogrenci.AdSoyad;
            ogrenciEntity.Eposta = ogrenci.Eposta;
            ogrenciEntity.BolumId = ogrenci.BolumId;

            _repository.Update(ogrenciEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Beklenmeyen bir hata oluştu."+ ex);
            }
            return true;
        }
    }

}

