using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Ders.Dtos;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

namespace Okul.Business.Operations.Ders
{
    public class DersManager : IDersService
	{

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Dersler> _repository;

        public DersManager(IUnitOfWork unitOfWork, IRepository<Dersler> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddDers(AddDersDto ders)
        {
            var hasDers = _repository.GetAll(x => x.DersAdi.ToLower() == ders.DersAdi.ToLower()).Any();

            if (hasDers)
                return false;

            var dersEntity = new Dersler
            {
                DersAdi = ders.DersAdi,
                OgretmenId = ders.OgretmenId
            };

            _repository.Add(dersEntity);

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

        public async Task<bool> DeleteDers(Dersler ders, bool softDelete = true)
        {
            _repository.Delete(ders, softDelete);

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

        public async Task<IEnumerable<Dersler>> GetAllAsync()
        {
            return await _repository.GetAll()
                             .Include(x=>x.Ogretmen)
                             .ToListAsync();
        }

        public async Task<List<Dersler>> GetAllDersler()
        {
            var dersler = await _repository.GetAll()
                            .Include(x => x.Ogretmen)
                            .ToListAsync();

            return dersler;
        }

        public Dersler GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<bool> UpdateDers(UpdateDersDto ders)
        {
            var dersEntity = _repository.GetById(ders.DersId);

            if (dersEntity == null)
                return false;

            dersEntity.DersAdi = ders.DersAdi;
            dersEntity.OgretmenId = ders.OgretmenId;
           

            _repository.Update(dersEntity);

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

