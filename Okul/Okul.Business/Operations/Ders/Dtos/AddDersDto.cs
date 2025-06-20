using System;
namespace Okul.Business.Operations.Ders.Dtos
{
	public class AddDersDto
	{
        public string DersAdi { get; set; } = null!;

        public int OgretmenId { get; set; }
    }
}

