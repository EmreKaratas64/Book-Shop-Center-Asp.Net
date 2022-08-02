using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Book_Shop.Models.Entity;

namespace Book_Shop.Models.MyClasses
{
    public class Class1
    {
        public IEnumerable<KITAPLAR> Kitaplar { get; set; }

        public IEnumerable<KATEGORILER> Kategoriler { get; set; }

        public IEnumerable<KASA> Kasa { get; set; }

        public IEnumerable<MUSTERILER> Musteriler { get; set; }

        public IEnumerable<HAREKETLER> Hareketler { get; set; }

        public IEnumerable<TBLILETISIM> tbliletisim { get; set; }

        public IEnumerable<TBLMESAJLAR> mesajlar { get; set; }

    }
}