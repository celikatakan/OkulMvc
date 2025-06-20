using System;
using System.Collections.Generic;

namespace Okul.Data.Models;

public partial class Ogrenciler
{
    public int OgrenciId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string? Eposta { get; set; }

    public int BolumId { get; set; }

    public virtual Bolumler Bolum { get; set; } = null!;

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();
}
