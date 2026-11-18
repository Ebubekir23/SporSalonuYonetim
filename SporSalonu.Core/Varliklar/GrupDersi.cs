namespace SporSalonu.Core.Varliklar
{
    // Haftalık tekrar eden grup dersi (Pilates, Spinning...)
    public class GrupDersi : VarlikTabani
    {
        public string Ad { get; set; } = "";

        public int AntrenorId { get; set; }
        public Antrenor Antrenor { get; set; } = null!;

        public DayOfWeek Gun { get; set; }
        public TimeOnly BaslangicSaati { get; set; }
        public int SureDakika { get; set; }
        public int Kontenjan { get; set; }

        public List<DersKaydi> Kayitlar { get; set; } = new List<DersKaydi>();

        public bool DoluMu
        {
            get { return Kayitlar.Count >= Kontenjan; }
        }
    }
}
