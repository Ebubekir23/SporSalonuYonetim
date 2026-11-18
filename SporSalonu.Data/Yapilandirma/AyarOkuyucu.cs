using Microsoft.Extensions.Configuration;

namespace SporSalonu.Data.Yapilandirma
{
    // Bağlantı dizesini ve API anahtarlarını repoya girmeyen appsettings.json dosyasından
    // veya ortam değişkenlerinden okur.
    public static class AyarOkuyucu
    {
        private const string DosyaAdi = "appsettings.json";

        public static IConfiguration Oku()
        {
            ConfigurationBuilder olusturucu = new ConfigurationBuilder();

            string? dosya = AyarDosyasiniBul();
            if (dosya != null)
                olusturucu.AddJsonFile(dosya, optional: true);

            // Ortam değişkenleri dosyadaki değerleri ezer (ör. SPORSALONU_ConnectionStrings__SporSalonu)
            olusturucu.AddEnvironmentVariables("SPORSALONU_");
            return olusturucu.Build();
        }

        public static string BaglantiDizesi()
        {
            string? baglanti = Oku().GetConnectionString("SporSalonu");
            if (string.IsNullOrWhiteSpace(baglanti))
                throw new InvalidOperationException(
                    "Veritabanı bağlantı dizesi bulunamadı. Proje kök klasöründeki appsettings.ornek.json dosyasını " +
                    "appsettings.json adıyla kopyalayıp bilgileri doldurun.");
            return baglanti;
        }

        // Çalışma klasöründen ve programın bulunduğu klasörden yukarı doğru appsettings.json arar
        private static string? AyarDosyasiniBul()
        {
            foreach (string baslangic in new[] { Directory.GetCurrentDirectory(), AppContext.BaseDirectory })
            {
                DirectoryInfo? klasor = new DirectoryInfo(baslangic);
                while (klasor != null)
                {
                    string yol = Path.Combine(klasor.FullName, DosyaAdi);
                    if (File.Exists(yol))
                        return yol;
                    klasor = klasor.Parent;
                }
            }
            return null;
        }
    }
}
