namespace SporSalonu.Core.Varliklar
{
    // Üye ve antrenörün ortak bilgileri (kalıtım)
    public abstract class Kisi : VarlikTabani
    {
        public string Ad { get; set; } = "";
        public string Soyad { get; set; } = "";
        public string Telefon { get; set; } = "";
        public string? Eposta { get; set; }

        public string AdSoyad
        {
            get { return $"{Ad} {Soyad}"; }
        }

        public override string ToString()
        {
            return AdSoyad;
        }
    }
}
