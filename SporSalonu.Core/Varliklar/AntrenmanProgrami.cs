namespace SporSalonu.Core.Varliklar
{
    // Üyeye hazırlanan program; yapay zekâ ile de üretilebilir
    public class AntrenmanProgrami : VarlikTabani
    {
        public int UyeId { get; set; }
        public Uye Uye { get; set; } = null!;

        public AntrenmanHedefi Hedef { get; set; }
        public string Icerik { get; set; } = "";
        public bool YapayZekaIleOlusturuldu { get; set; }
    }
}
