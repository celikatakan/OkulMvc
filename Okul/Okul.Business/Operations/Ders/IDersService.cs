using System;
using System.Collections;
using Okul.Business.Operations.Ders.Dtos;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Data.Models;

namespace Okul.Business.Operations.Ders
{
	public interface IDersService
	{
        Task<IEnumerable<Dersler>> GetAllAsync();
        Task<List<Dersler>> GetAllDersler();
        Task<bool> AddDers(AddDersDto ders);
        Dersler GetById(int id);
        Task<bool> UpdateDers(UpdateDersDto ders);
        Task<bool> DeleteDers(Dersler ders, bool softDelete = true);
        int Count();
    }
}

