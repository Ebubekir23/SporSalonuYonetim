namespace SporSalonu.Core.Varliklar
{
    // Turnike gibi: üyenin salona giriş ve çıkış saati
    public class GirisKaydi : VarlikTabani
    {
        public int UyeId { get; set; }
        public Uye Uye { get; set; } = null!;

        public DateTime GirisZamani { get; set; } = DateTime.Now;
        public DateTime? CikisZamani { get; set; }
    }
}
