using System;
using System.Collections.Generic;

namespace Okul.Data.Models;

public partial class Notlar
{
    public int NotId { get; set; }

    public int OgrenciId { get; set; }

    public int DersId { get; set; }

    public double? Sinav1 { get; set; }

    public double? Sinav2 { get; set; }

    public double? FinalNotu { get; set; }

    public virtual Dersler Ders { get; set; } = null!;

    public virtual Ogrenciler Ogrenci { get; set; } = null!;
}
