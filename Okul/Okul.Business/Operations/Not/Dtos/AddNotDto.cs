using System;
namespace Okul.Business.Operations.Not.Dtos
{
	public class AddNotDto
	{

        public int OgrenciId { get; set; }

        public int DersId { get; set; }

        public double? Sinav1 { get; set; }

        public double? Sinav2 { get; set; }

        public double? FinalNotu { get; set; }
    }
}

