namespace SporSalonu.Core.Varliklar
{
    // Üye ile grup dersi arasındaki çoka-çok ilişki
    public class DersKaydi : VarlikTabani
    {
        public int UyeId { get; set; }
        public Uye Uye { get; set; } = null!;

        public int GrupDersiId { get; set; }
        public GrupDersi GrupDersi { get; set; } = null!;
    }
}
