using Microsoft.EntityFrameworkCore;
using SporSalonu.Core.Varliklar;
using SporSalonu.Data.Yapilandirma;

namespace SporSalonu.Data
{
    public class SporSalonuDbContext : DbContext
    {
        private readonly string? baglantiDizesi;

        public DbSet<Uye> Uyeler => Set<Uye>();
        public DbSet<Antrenor> Antrenorler => Set<Antrenor>();
        public DbSet<UyelikPaketi> UyelikPaketleri => Set<UyelikPaketi>();
        public DbSet<Uyelik> Uyelikler => Set<Uyelik>();
        public DbSet<Odeme> Odemeler => Set<Odeme>();
        public DbSet<GrupDersi> GrupDersleri => Set<GrupDersi>();
        public DbSet<DersKaydi> DersKayitlari => Set<DersKaydi>();
        public DbSet<Olcum> Olcumler => Set<Olcum>();
        public DbSet<GirisKaydi> GirisKayitlari => Set<GirisKaydi>();
        public DbSet<AntrenmanProgrami> AntrenmanProgramlari => Set<AntrenmanProgrami>();

        // Bağlantı dizesi verilmezse appsettings.json'dan okunur
        public SporSalonuDbContext(string? baglantiDizesi = null)
        {
            this.baglantiDizesi = baglantiDizesi;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder secenekler)
        {
            if (!secenekler.IsConfigured)
            {
                secenekler
                    .UseNpgsql(baglantiDizesi ?? AyarOkuyucu.BaglantiDizesi())
                    .UseSnakeCaseNamingConvention(); // tablo ve sütun adları: uyelik_paketleri, dogum_tarihi
            }
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder kurallar)
        {
            // Tarihler yerel saat olarak saklanır; para alanları 2 ondalık basamaklı
            kurallar.Properties<DateTime>().HaveColumnType("timestamp without time zone");
            kurallar.Properties<decimal>().HavePrecision(10, 2);
            kurallar.Properties<string>().HaveMaxLength(200);
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Uye>(e =>
            {
                e.Property(u => u.Ad).HasMaxLength(50).IsRequired();
                e.Property(u => u.Soyad).HasMaxLength(50).IsRequired();
                e.Property(u => u.Telefon).HasMaxLength(20).IsRequired();
                e.Property(u => u.Eposta).HasMaxLength(100);
                e.Property(u => u.Notlar).HasMaxLength(1000);
                e.Property(u => u.Cinsiyet).HasConversion<string>().HasMaxLength(20);
                e.HasIndex(u => u.Telefon).IsUnique();
                e.Ignore(u => u.AdSoyad);
            });

            model.Entity<Antrenor>(e =>
            {
                e.Property(a => a.Ad).HasMaxLength(50).IsRequired();
                e.Property(a => a.Soyad).HasMaxLength(50).IsRequired();
                e.Property(a => a.Telefon).HasMaxLength(20).IsRequired();
                e.Property(a => a.Eposta).HasMaxLength(100);
                e.Property(a => a.Uzmanlik).HasMaxLength(100);
                e.HasIndex(a => a.Telefon).IsUnique();
                e.Ignore(a => a.AdSoyad);
            });

            model.Entity<UyelikPaketi>(e =>
            {
                e.Property(p => p.Ad).HasMaxLength(50).IsRequired();
                e.HasIndex(p => p.Ad).IsUnique();
                e.ToTable(t => t.HasCheckConstraint("ck_paket_sure", "sure_gun > 0"));
                e.ToTable(t => t.HasCheckConstraint("ck_paket_fiyat", "fiyat >= 0"));
            });

            model.Entity<Uyelik>(e =>
            {
                // Satış ve ödeme kaydı olan üye/paket yanlışlıkla silinmesin
                e.HasOne(u => u.Uye).WithMany(u => u.Uyelikler).HasForeignKey(u => u.UyeId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(u => u.Paket).WithMany(p => p.Uyelikler).HasForeignKey(u => u.PaketId).OnDelete(DeleteBehavior.Restrict);
                e.ToTable(t => t.HasCheckConstraint("ck_uyelik_tarih", "bitis_tarihi >= baslangic_tarihi"));
                e.Ignore(u => u.OdenenTutar);
                e.Ignore(u => u.KalanBorc);
            });

            model.Entity<Odeme>(e =>
            {
                e.HasOne(o => o.Uyelik).WithMany(u => u.Odemeler).HasForeignKey(o => o.UyelikId).OnDelete(DeleteBehavior.Restrict);
                e.Property(o => o.Yontem).HasConversion<string>().HasMaxLength(20);
                e.ToTable(t => t.HasCheckConstraint("ck_odeme_tutar", "tutar > 0"));
            });

            model.Entity<GrupDersi>(e =>
            {
                e.Property(d => d.Ad).HasMaxLength(50).IsRequired();
                e.HasOne(d => d.Antrenor).WithMany(a => a.GrupDersleri).HasForeignKey(d => d.AntrenorId).OnDelete(DeleteBehavior.Restrict);
                e.ToTable(t => t.HasCheckConstraint("ck_ders_kontenjan", "kontenjan > 0"));
                e.Ignore(d => d.DoluMu);
            });

            model.Entity<DersKaydi>(e =>
            {
                e.HasOne(k => k.Uye).WithMany(u => u.DersKayitlari).HasForeignKey(k => k.UyeId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(k => k.GrupDersi).WithMany(d => d.Kayitlar).HasForeignKey(k => k.GrupDersiId).OnDelete(DeleteBehavior.Cascade);
                e.HasIndex(k => new { k.UyeId, k.GrupDersiId }).IsUnique(); // aynı derse iki kez kayıt olunmaz
            });

            model.Entity<Olcum>(e =>
            {
                e.HasOne(o => o.Uye).WithMany(u => u.Olcumler).HasForeignKey(o => o.UyeId).OnDelete(DeleteBehavior.Cascade);
                e.Property(o => o.KiloKg).HasPrecision(5, 1);
                e.Property(o => o.YagOrani).HasPrecision(4, 1);
                e.Property(o => o.BelCevresiCm).HasPrecision(5, 1);
            });

            model.Entity<GirisKaydi>(e =>
            {
                e.HasOne(g => g.Uye).WithMany(u => u.GirisKayitlari).HasForeignKey(g => g.UyeId).OnDelete(DeleteBehavior.Cascade);
                e.HasIndex(g => g.GirisZamani);
            });

            model.Entity<AntrenmanProgrami>(e =>
            {
                e.HasOne(p => p.Uye).WithMany(u => u.AntrenmanProgramlari).HasForeignKey(p => p.UyeId).OnDelete(DeleteBehavior.Cascade);
                e.Property(p => p.Hedef).HasConversion<string>().HasMaxLength(20);
                e.Property(p => p.Icerik).HasMaxLength(8000).IsRequired();
            });

            BaslangicVerileriniEkle(model);
        }

        // Uygulama ilk açıldığında hazır bulunan üyelik paketleri
        private static void BaslangicVerileriniEkle(ModelBuilder model)
        {
            DateTime tarih = new DateTime(2026, 11, 18);
            model.Entity<UyelikPaketi>().HasData(
                new UyelikPaketi { Id = 1, Ad = "Aylık", SureGun = 30, Fiyat = 1200m, Aciklama = "Tüm saatlerde salon kullanımı", OlusturmaTarihi = tarih },
                new UyelikPaketi { Id = 2, Ad = "3 Aylık", SureGun = 90, Fiyat = 3200m, Aciklama = "Aylığa göre yaklaşık %11 indirimli", OlusturmaTarihi = tarih },
                new UyelikPaketi { Id = 3, Ad = "6 Aylık", SureGun = 180, Fiyat = 6000m, Aciklama = "Aylığa göre yaklaşık %17 indirimli", OlusturmaTarihi = tarih },
                new UyelikPaketi { Id = 4, Ad = "Yıllık", SureGun = 365, Fiyat = 10500m, Aciklama = "Aylığa göre yaklaşık %27 indirimli", OlusturmaTarihi = tarih },
                new UyelikPaketi { Id = 5, Ad = "Öğrenci Aylık", SureGun = 30, Fiyat = 900m, Aciklama = "Öğrenci belgesi ile", OlusturmaTarihi = tarih });
        }
    }
}
