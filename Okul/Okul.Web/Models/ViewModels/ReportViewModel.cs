using System;
using Okul.Data.Models;

namespace Okul.Web.Models.ViewModels
{
    public class ReportViewModel
    {
        public List<BolumOgrenciSayisiDto> BolumOgrenciSayilari { get; set; }
        public List<OgretmenDersSayisiDto> OgretmenDersSayilari { get; set; }
        public List<OgrenciNotOrtalamasiDto> OgrenciNotOrtalamalari { get; set; }
        public List<Ogrenciler> EnBasariliOgrenciler { get; set; }
        public List<OgretmenEnCokDerseGirenDto> EnCokDerseGirenOgretmenler { get; set; }
        public List<DersOrtalamaDto> DersOrtalamaNotlari { get; set; }
        public List<FinalNotuDusukOgrenciDto> FinalNotuDusukOgrenciler { get; set; } = new();

    }

    public class BolumOgrenciSayisiDto
    {
        public string BolumAdi { get; set; }
        public int OgrenciSayisi { get; set; }
    }

    public class OgretmenDersSayisiDto
    {
        public string OgretmenAdi { get; set; }
        public int DersSayisi { get; set; }
    }

    public class OgrenciNotOrtalamasiDto
    {
        public string OgrenciAdi { get; set; }
        public double Ortalama { get; set; }
    }

    public class OgretmenEnCokDerseGirenDto
    {
        public string OgretmenAdi { get; set; }
        public int DersSayisi { get; set; }
    }

    public class DersOrtalamaDto
    {
        public string DersAdi { get; set; }
        public double OrtalamaNot { get; set; }
    }
    public class FinalNotuDusukOgrenciDto
    {
        public string OgrenciAdi { get; set; } = null!;
        public string DersAdi { get; set; } = null!;
        public double FinalNotu { get; set; }
    }

}

