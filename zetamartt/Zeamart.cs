using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zetamartt
{
    class Zeamart
    {
        public string NamaBarang { get; set; }
        public string KodeBarang { get; set; }
        public string Stok { get; set; }
        public string Harga { get; set; }
        public string Expired { get; set; }
        public string Image { get; set; }

        public Zeamart(string namaBarang, string kodeBarang, string stok, string harga, string expired, string image)
        {
            NamaBarang = namaBarang;
            KodeBarang = kodeBarang;
            Stok = stok;
            Harga = harga;
            Expired = expired;
            Image = image;
        }
    }
}
