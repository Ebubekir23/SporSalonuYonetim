namespace SporSalonu.Core.Varliklar
{
    public class Uye : Kisi
    {
        public DateOnly DogumTarihi { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public int? BoyCm { get; set; }
        public DateOnly KayitTarihi { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public bool Aktif { get; set; } = true;
        public string? Notlar { get; set; }

        public List<Uyelik> Uyelikler { get; set; } = new List<Uyelik>();
        public List<Olcum> Olcumler { get; set; } = new List<Olcum>();
        public List<GirisKaydi> GirisKayitlari { get; set; } = new List<GirisKaydi>();
        public List<DersKaydi> DersKayitlari { get; set; } = new List<DersKaydi>();
        public List<AntrenmanProgrami> AntrenmanProgramlari { get; set; } = new List<AntrenmanProgrami>();

        public int Yas(DateOnly bugun)
        {
            int yas = bugun.Year - DogumTarihi.Year;
            if (bugun < DogumTarihi.AddYears(yas))
                yas--;
            return yas;
        }

        // Bugün geçerli bir üyeliği varsa onu, yoksa null döner
        public Uyelik? AktifUyelik(DateOnly bugun)
        {
            Uyelik? sonuc = null;
            foreach (Uyelik uyelik in Uyelikler)
            {
                if (uyelik.AktifMi(bugun) && (sonuc == null || uyelik.BitisTarihi > sonuc.BitisTarihi))
                    sonuc = uyelik;
            }
            return sonuc;
        }
    }
}
