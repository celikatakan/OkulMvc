using System;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Okul.Business.Operations.Not.Dtos;
using Okul.Data.Models;
using Okul.Data.Repository;
using Okul.Data.UnitOfWork;

namespace Okul.Business.Operations.Not
{
    public class NotManager : INotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Notlar> _repository;

        public NotManager(IUnitOfWork unitOfWork, IRepository<Notlar> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddNot(AddNotDto not)
        {

            var notEntity = new Notlar
            {
                DersId = not.DersId,
                OgrenciId = not.OgrenciId,
                Sinav1 = not.Sinav1,
                Sinav2 = not.Sinav2,
                FinalNotu = not.FinalNotu
            };

            _repository.Add(notEntity);

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

        public async Task<bool> DeleteNot(Notlar not, bool softDelete = true)
        {
            _repository.Delete(not, softDelete);

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

        public async Task<List<Notlar>> GetAllNotlar()
        {
            var notlar = await _repository.GetAll()
                             .Include(x => x.Ogrenci).Include(x => x.Ders)
                             .ToListAsync();

            return notlar;
        }

        public Notlar GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<bool> UpdateNot(UpdateNotDto not)
        {
            var notEntity = _repository.GetById(not.NotId);

            if (notEntity == null)
                return false;

            notEntity.OgrenciId = not.OgrenciId;
            notEntity.DersId = not.DersId;
            notEntity.Sinav1 = not.Sinav1;
            notEntity.Sinav2 = not.Sinav2;
            notEntity.FinalNotu = not.FinalNotu;

            _repository.Update(notEntity);

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

        public async Task<IEnumerable<Notlar>> GetAllAsync()
        {
            return await _repository.GetAll()
                                    .Include(x => x.Ogrenci)
                                    .Include(x => x.Ders)
                                    .ToListAsync();
        }

        public int Count()
        {
            return _repository.Count();
        }
    }
}

