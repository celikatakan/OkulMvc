using System;
using System.Collections;
using Okul.Business.Operations.Bolum.Dtos;
using Okul.Business.Operations.Ogretmen.Dtos;
using Okul.Data.Models;

namespace Okul.Business.Operations.Ogretmen
{
	public interface IOgretmenService
	{
        Task<List<Ogretmenler>>GetAllOgretmenler();
        Task<bool> AddOgretmen(AddOgretmenDto ogretmen);
        Ogretmenler GetById(int id);
        Task<bool> UpdateOgretmen(UpdateOgretmenDto ogretmen);
        Task<bool> DeleteOgretmen(Ogretmenler ogretmen, bool softDelete = true);
        Task<IEnumerable<Ogretmenler>> GetAllAsync();
        int Count();
    }
}

