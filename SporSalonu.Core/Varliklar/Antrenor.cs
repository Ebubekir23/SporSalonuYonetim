namespace SporSalonu.Core.Varliklar
{
    public class Antrenor : Kisi
    {
        public string Uzmanlik { get; set; } = "";
        public DateOnly IseBaslamaTarihi { get; set; }
        public bool Aktif { get; set; } = true;

        public List<GrupDersi> GrupDersleri { get; set; } = new List<GrupDersi>();
    }
}
