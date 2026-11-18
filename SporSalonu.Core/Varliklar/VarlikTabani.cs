namespace SporSalonu.Core.Varliklar
{
    // Tüm tabloların ortak alanları
    public abstract class VarlikTabani
    {
        public int Id { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    }
}
