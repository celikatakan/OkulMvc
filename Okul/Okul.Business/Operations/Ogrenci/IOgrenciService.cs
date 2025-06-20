using System;
using System.Collections;
using Okul.Business.Operations.Ogrenci.Dtos;
using Okul.Business.Types;
using Okul.Data.Models;

namespace Okul.Business.Operations.Ogrenci
{
	public interface IOgrenciService
	{
        Task<List<Ogrenciler>> GetAllOgrenciler();
        Task<bool> AddOgrenci(AddOgrenciDto ogrenci);
        Task<bool> UpdateOgrenci(UpdateOgrenciDto ogrenci);
        Ogrenciler GetById(int id);
        Task<bool> DeleteOgrenci(Ogrenciler ogrenci, bool softDelete = true);
        Task<IEnumerable<Ogrenciler>> GetAllAsync();
    }
}

