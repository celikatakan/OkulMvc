using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Okul.Business.Operations.Ogretmen.Dtos;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

namespace Okul.Business.Operations.Ogretmen
{
    public class OgretmenManager : IOgretmenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Ogretmenler> _repository;

        public OgretmenManager(IUnitOfWork unitOfWork, IRepository<Ogretmenler> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddOgretmen(AddOgretmenDto ogretmen)
        {
            var hasOgretmen = _repository.GetAll(x => x.AdSoyad.ToLower() == ogretmen.AdSoyad.ToLower()).Any();

            if (hasOgretmen)
                return false;

            var ogretmenEntity = new Ogretmenler
            {
                AdSoyad = ogretmen.AdSoyad,
                Eposta = ogretmen.Eposta
            };

            _repository.Add(ogretmenEntity);

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

        public async Task<bool> DeleteOgretmen(Ogretmenler ogretmen, bool softDelete = true)
        {
            _repository.Delete(ogretmen, softDelete);
            
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


        public async Task<List<Ogretmenler>> GetAllOgretmenler()
        {
            var ogretmenler = await _repository.GetAll().ToListAsync();

            return ogretmenler;
        }

        public Ogretmenler GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<bool> UpdateOgretmen(UpdateOgretmenDto ogretmen)
        {
            var ogretmenEntity = _repository.GetById(ogretmen.OgretmenId);

            if (ogretmenEntity == null)
                return false;

            ogretmenEntity.AdSoyad = ogretmen.AdSoyad;
            ogretmenEntity.Eposta = ogretmen.Eposta;

            _repository.Update(ogretmenEntity);

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

        public async Task<IEnumerable<Ogretmenler>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }
    }
}

