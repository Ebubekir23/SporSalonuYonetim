namespace SporSalonu.Core.Varliklar
{
    // Satılan paket tanımı (Aylık, Yıllık, Öğrenci...)
    public class UyelikPaketi : VarlikTabani
    {
        public string Ad { get; set; } = "";
        public int SureGun { get; set; }
        public decimal Fiyat { get; set; }
        public string? Aciklama { get; set; }
        public bool Aktif { get; set; } = true;

        public List<Uyelik> Uyelikler { get; set; } = new List<Uyelik>();

        public override string ToString()
        {
            return $"{Ad} ({SureGun} gün)";
        }
    }
}
