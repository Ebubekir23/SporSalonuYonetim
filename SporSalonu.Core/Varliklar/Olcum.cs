namespace SporSalonu.Core.Varliklar
{
    // Üyenin zaman içindeki vücut ölçümleri
    public class Olcum : VarlikTabani
    {
        public int UyeId { get; set; }
        public Uye Uye { get; set; } = null!;

        public DateOnly Tarih { get; set; }
        public decimal KiloKg { get; set; }
        public decimal? YagOrani { get; set; }
        public decimal? BelCevresiCm { get; set; }

        // Vücut kitle indeksi = kilo / boy(m)^2
        public decimal? VucutKitleIndeksi(int? boyCm)
        {
            if (boyCm == null || boyCm <= 0)
                return null;

            decimal boyMetre = boyCm.Value / 100m;
            return Math.Round(KiloKg / (boyMetre * boyMetre), 1);
        }
    }
}
