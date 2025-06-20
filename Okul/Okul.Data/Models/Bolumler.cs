using System;
using System.Collections.Generic;

namespace Okul.Data.Models;

public partial class Bolumler
{
    public int BolumId { get; set; }

    public string BolumAdi { get; set; } = null!;

    public virtual ICollection<Ogrenciler> Ogrencilers { get; set; } = new List<Ogrenciler>();
}
