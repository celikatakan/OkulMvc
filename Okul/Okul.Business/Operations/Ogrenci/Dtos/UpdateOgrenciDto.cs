using System;
namespace Okul.Business.Operations.Ogrenci.Dtos
{
	public class UpdateOgrenciDto
	{
        public int OgrenciId { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string? Eposta { get; set; }
        public int BolumId { get; set; }
    }
}

