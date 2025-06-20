using System;
namespace Okul.Business.Operations.Ogretmen.Dtos
{
	public class UpdateOgretmenDto
	{
        public int OgretmenId { get; set; }

        public string AdSoyad { get; set; } = null!;

        public string? Eposta { get; set; }
    }
}

