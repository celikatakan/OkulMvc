using System;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Bolum.Dtos;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

namespace Okul.Business.Operations.Bolum
{
    public class BolumManager : IBolumService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Bolumler> _repository;

        public BolumManager(IUnitOfWork unitOfWork, IRepository<Bolumler> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddBolum(AddBolumDto bolum)
        {
            var hasBolum = _repository.GetAll(x => x.BolumAdi.ToLower() == bolum.BolumAdi.ToLower()).Any();

            if (hasBolum)
                return false;

            var bolumEntity = new Bolumler
            {
                BolumAdi = bolum.BolumAdi
            };

            _repository.Add(bolumEntity);

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

        public async Task<bool> DeleteBolum(Bolumler bolum, bool softDelete = true)
        {
            _repository.Delete(bolum, softDelete);

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

        public async Task<List<Bolumler>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<List<Bolumler>> GetAllBolumler()
        {
            var bolumler = await _repository.GetAll().ToListAsync();
                         
            return bolumler;
        }

        public Bolumler GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<bool> UpdateBolum(UpdateBolumDto bolum)
        {
            var bolumEntity = _repository.GetById(bolum.BolumId);

            if (bolumEntity == null)
                return false;

            bolumEntity.BolumAdi = bolum.BolumAdi;
        
            _repository.Update(bolumEntity);

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
    }
}

