using System;
using Okul.Business.Operations.Bolum.Dtos;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Data.Models;

namespace Okul.Business.Operations.Bolum
{
	public interface IBolumService
	{
        Task<List<Bolumler>> GetAllAsync();
        Task<List<Bolumler>> GetAllBolumler();
        Task<bool> AddBolum(AddBolumDto bolum);
        Task<bool> UpdateBolum(UpdateBolumDto bolum);
        Bolumler GetById(int id);
        Task<bool> DeleteBolum(Bolumler bolum, bool softDelete = true);

        int Count();
    }
}

