using System;
using System.Collections.Generic;

namespace Okul.Data.Models;

public partial class Ogretmenler
{
    public int OgretmenId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string? Eposta { get; set; }

    public virtual ICollection<Dersler> Derslers { get; set; } = new List<Dersler>();
}
