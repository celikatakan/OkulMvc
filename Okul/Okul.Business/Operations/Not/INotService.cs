using System;
using System.Collections;
using Okul.Business.Operations.Ders.Dtos;
using Okul.Business.Operations.Not.Dtos;
using Okul.Data.Models;

namespace Okul.Business.Operations.Not
{
	public interface INotService
	{
       
        Task<List<Notlar>> GetAllNotlar();
        Task<bool> AddNot(AddNotDto not);
        Notlar GetById(int id);
        Task<bool> UpdateNot(UpdateNotDto not);
        Task<bool> DeleteNot(Notlar not, bool softDelete = true);
        Task<IEnumerable<Notlar>> GetAllAsync();
        int Count();
    }
}

