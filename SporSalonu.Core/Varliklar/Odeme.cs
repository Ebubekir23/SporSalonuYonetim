namespace SporSalonu.Core.Varliklar
{
    public class Odeme : VarlikTabani
    {
        public int UyelikId { get; set; }
        public Uyelik Uyelik { get; set; } = null!;

        public decimal Tutar { get; set; }
        public DateTime Tarih { get; set; } = DateTime.Now;
        public OdemeYontemi Yontem { get; set; }
        public string? Aciklama { get; set; }
    }
}
