using System;
using System.Collections.Generic;

namespace Okul.Data.Models;

public partial class Dersler
{
    public int DersId { get; set; }

    public string DersAdi { get; set; } = null!;

    public int OgretmenId { get; set; }

    public virtual ICollection<Notlar> Notlars { get; set; } = new List<Notlar>();

    public virtual Ogretmenler Ogretmen { get; set; } = null!;
}
