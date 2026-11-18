# Yapay Zeka Destekli Spor Salonu Yönetim ve Kişisel Antrenman Sistemi

Nesne Tabanlı Programlama (YMH 219) dönem projesi. Spor salonunun üye, üyelik paketi, ödeme, antrenör, grup dersi, vücut ölçümü ve giriş kayıtlarını yöneten; yapay zekâ ile üyeye özel antrenman programı öneren bir Windows Forms uygulaması.

> Proje geliştirme aşamasındadır. Arayüz (DevExpress) ve yapay zekâ özellikleri sonraki adımlarda eklenecektir.

## Kullanılan Teknolojiler

- C# / .NET 10, Visual Studio 2026
- Arayüz: Windows Forms + DevExpress (eklenecek)
- Veritabanı: PostgreSQL 17 (Docker), Entity Framework Core 10 + Npgsql
- Yapay zekâ: Google Gemini API (eklenecek)

## Proje Yapısı

| Proje | Görevi |
|---|---|
| `SporSalonu.Core` | Varlık sınıfları (`Uye`, `Antrenor`, `Uyelik`, `Odeme`...) ve enum'lar; başka katmana bağımlı değildir |
| `SporSalonu.Data` | `SporSalonuDbContext`, tablo yapılandırmaları, migration'lar (`Gocler` klasörü) |

## Veritabanı Tasarımı

| Tablo | Açıklama |
|---|---|
| `uyeler` | Üye bilgileri (telefon benzersiz) |
| `antrenorler` | Antrenör bilgileri |
| `uyelik_paketleri` | Satılan paketler (Aylık, 3 Aylık, 6 Aylık, Yıllık, Öğrenci) |
| `uyelikler` | Üyenin aldığı paket, başlangıç/bitiş tarihi ve satış fiyatı |
| `odemeler` | Bir üyeliğe yapılan ödemeler (taksit olabilir) |
| `grup_dersleri` | Haftalık grup dersleri ve kontenjanları |
| `ders_kayitlari` | Üye – grup dersi (çoka-çok) |
| `olcumler` | Kilo, yağ oranı, bel çevresi ölçümleri |
| `giris_kayitlari` | Salona giriş/çıkış saatleri |
| `antrenman_programlari` | Üyeye hazırlanan (veya yapay zekâ ile üretilen) programlar |

Veri bütünlüğü için benzersizlik, CHECK kısıtları (ör. bitiş tarihi ≥ başlangıç tarihi, ödeme > 0) ve silme kuralları (ödemesi olan üyelik silinemez) tanımlıdır.

## Kurulum ve Çalıştırma

Gereksinimler: .NET 10 SDK, Docker Desktop, `dotnet-ef` aracı (`dotnet tool install -g dotnet-ef --version 10.0.15`).

1. `.env.ornek` dosyasını `.env` adıyla kopyalayıp bir parola yazın.
2. `appsettings.ornek.json` dosyasını `appsettings.json` adıyla kopyalayıp aynı parolayı bağlantı dizesine yazın.
3. Veritabanını başlatın ve tabloları oluşturun:

```bash
docker compose up -d
```

```bash
dotnet ef database update --project SporSalonu.Data --startup-project SporSalonu.Data
```

`.env` ve `appsettings.json` dosyaları parola ve API anahtarı içerdiği için repoya yüklenmez.

## Geliştirici

Ebubekir Yılmaz – 250541051
