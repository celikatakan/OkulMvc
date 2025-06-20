using System;
namespace Okul.Business.Operations.Ogrenci.Dtos
{
	public class AddOgrenciDto
	{
        public string AdSoyad { get; set; } = null!;    
        public string? Eposta { get; set; }             
        public int BolumId { get; set; }
    }
}

