using System;
namespace Okul.Business.Operations.Ders.Dtos
{
	public class UpdateDersDto
	{
        public int DersId { get; set; }

        public string DersAdi { get; set; } = null!;

        public int OgretmenId { get; set; }
    }
}

