namespace SporSalonu.Core.Varliklar
{
    // Bir üyenin satın aldığı paket ve geçerlilik tarihleri
    public class Uyelik : VarlikTabani
    {
        public int UyeId { get; set; }
        public Uye Uye { get; set; } = null!;

        public int PaketId { get; set; }
        public UyelikPaketi Paket { get; set; } = null!;

        public DateOnly BaslangicTarihi { get; set; }
        public DateOnly BitisTarihi { get; set; }

        // Paket fiyatı sonradan değişse de satış anındaki fiyat korunur
        public decimal Fiyat { get; set; }

        public List<Odeme> Odemeler { get; set; } = new List<Odeme>();

        public bool AktifMi(DateOnly bugun)
        {
            return bugun >= BaslangicTarihi && bugun <= BitisTarihi;
        }

        public int KalanGun(DateOnly bugun)
        {
            if (bugun > BitisTarihi)
                return 0;
            return BitisTarihi.DayNumber - bugun.DayNumber;
        }

        public decimal OdenenTutar
        {
            get
            {
                decimal toplam = 0;
                foreach (Odeme odeme in Odemeler)
                    toplam += odeme.Tutar;
                return toplam;
            }
        }

        public decimal KalanBorc
        {
            get { return Math.Max(0, Fiyat - OdenenTutar); }
        }
    }
}
