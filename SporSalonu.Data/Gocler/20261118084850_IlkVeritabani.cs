using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SporSalonu.Data.Gocler
{
    /// <inheritdoc />
    public partial class IlkVeritabani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "antrenorler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uzmanlik = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ise_baslama_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    aktif = table.Column<bool>(type: "boolean", nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    telefon = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    eposta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_antrenorler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "uyeler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dogum_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    cinsiyet = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    boy_cm = table.Column<int>(type: "integer", nullable: true),
                    kayit_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    aktif = table.Column<bool>(type: "boolean", nullable: false),
                    notlar = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    telefon = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    eposta = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uyeler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "uyelik_paketleri",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    sure_gun = table.Column<int>(type: "integer", nullable: false),
                    fiyat = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    aktif = table.Column<bool>(type: "boolean", nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uyelik_paketleri", x => x.id);
                    table.CheckConstraint("ck_paket_fiyat", "fiyat >= 0");
                    table.CheckConstraint("ck_paket_sure", "sure_gun > 0");
                });

            migrationBuilder.CreateTable(
                name: "grup_dersleri",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    antrenor_id = table.Column<int>(type: "integer", nullable: false),
                    gun = table.Column<int>(type: "integer", nullable: false),
                    baslangic_saati = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    sure_dakika = table.Column<int>(type: "integer", nullable: false),
                    kontenjan = table.Column<int>(type: "integer", nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grup_dersleri", x => x.id);
                    table.CheckConstraint("ck_ders_kontenjan", "kontenjan > 0");
                    table.ForeignKey(
                        name: "fk_grup_dersleri_antrenorler_antrenor_id",
                        column: x => x.antrenor_id,
                        principalTable: "antrenorler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "antrenman_programlari",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uye_id = table.Column<int>(type: "integer", nullable: false),
                    hedef = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    icerik = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: false),
                    yapay_zeka_ile_olusturuldu = table.Column<bool>(type: "boolean", nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_antrenman_programlari", x => x.id);
                    table.ForeignKey(
                        name: "fk_antrenman_programlari_uyeler_uye_id",
                        column: x => x.uye_id,
                        principalTable: "uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "giris_kayitlari",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uye_id = table.Column<int>(type: "integer", nullable: false),
                    giris_zamani = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    cikis_zamani = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_giris_kayitlari", x => x.id);
                    table.ForeignKey(
                        name: "fk_giris_kayitlari_uyeler_uye_id",
                        column: x => x.uye_id,
                        principalTable: "uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "olcumler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uye_id = table.Column<int>(type: "integer", nullable: false),
                    tarih = table.Column<DateOnly>(type: "date", nullable: false),
                    kilo_kg = table.Column<decimal>(type: "numeric(5,1)", precision: 5, scale: 1, nullable: false),
                    yag_orani = table.Column<decimal>(type: "numeric(4,1)", precision: 4, scale: 1, nullable: true),
                    bel_cevresi_cm = table.Column<decimal>(type: "numeric(5,1)", precision: 5, scale: 1, nullable: true),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_olcumler", x => x.id);
                    table.ForeignKey(
                        name: "fk_olcumler_uyeler_uye_id",
                        column: x => x.uye_id,
                        principalTable: "uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "uyelikler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uye_id = table.Column<int>(type: "integer", nullable: false),
                    paket_id = table.Column<int>(type: "integer", nullable: false),
                    baslangic_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    bitis_tarihi = table.Column<DateOnly>(type: "date", nullable: false),
                    fiyat = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_uyelikler", x => x.id);
                    table.CheckConstraint("ck_uyelik_tarih", "bitis_tarihi >= baslangic_tarihi");
                    table.ForeignKey(
                        name: "fk_uyelikler_uyeler_uye_id",
                        column: x => x.uye_id,
                        principalTable: "uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_uyelikler_uyelik_paketleri_paket_id",
                        column: x => x.paket_id,
                        principalTable: "uyelik_paketleri",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ders_kayitlari",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uye_id = table.Column<int>(type: "integer", nullable: false),
                    grup_dersi_id = table.Column<int>(type: "integer", nullable: false),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ders_kayitlari", x => x.id);
                    table.ForeignKey(
                        name: "fk_ders_kayitlari_grup_dersleri_grup_dersi_id",
                        column: x => x.grup_dersi_id,
                        principalTable: "grup_dersleri",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ders_kayitlari_uyeler_uye_id",
                        column: x => x.uye_id,
                        principalTable: "uyeler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "odemeler",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    uyelik_id = table.Column<int>(type: "integer", nullable: false),
                    tutar = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    tarih = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    yontem = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    aciklama = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    olusturma_tarihi = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_odemeler", x => x.id);
                    table.CheckConstraint("ck_odeme_tutar", "tutar > 0");
                    table.ForeignKey(
                        name: "fk_odemeler_uyelikler_uyelik_id",
                        column: x => x.uyelik_id,
                        principalTable: "uyelikler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "uyelik_paketleri",
                columns: new[] { "id", "aciklama", "ad", "aktif", "fiyat", "olusturma_tarihi", "sure_gun" },
                values: new object[,]
                {
                    { 1, "Tüm saatlerde salon kullanımı", "Aylık", true, 1200m, new DateTime(2026, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 },
                    { 2, "Aylığa göre yaklaşık %11 indirimli", "3 Aylık", true, 3200m, new DateTime(2026, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 90 },
                    { 3, "Aylığa göre yaklaşık %17 indirimli", "6 Aylık", true, 6000m, new DateTime(2026, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 180 },
                    { 4, "Aylığa göre yaklaşık %27 indirimli", "Yıllık", true, 10500m, new DateTime(2026, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 365 },
                    { 5, "Öğrenci belgesi ile", "Öğrenci Aylık", true, 900m, new DateTime(2026, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 30 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_antrenman_programlari_uye_id",
                table: "antrenman_programlari",
                column: "uye_id");

            migrationBuilder.CreateIndex(
                name: "ix_antrenorler_telefon",
                table: "antrenorler",
                column: "telefon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ders_kayitlari_grup_dersi_id",
                table: "ders_kayitlari",
                column: "grup_dersi_id");

            migrationBuilder.CreateIndex(
                name: "ix_ders_kayitlari_uye_id_grup_dersi_id",
                table: "ders_kayitlari",
                columns: new[] { "uye_id", "grup_dersi_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_giris_kayitlari_giris_zamani",
                table: "giris_kayitlari",
                column: "giris_zamani");

            migrationBuilder.CreateIndex(
                name: "ix_giris_kayitlari_uye_id",
                table: "giris_kayitlari",
                column: "uye_id");

            migrationBuilder.CreateIndex(
                name: "ix_grup_dersleri_antrenor_id",
                table: "grup_dersleri",
                column: "antrenor_id");

            migrationBuilder.CreateIndex(
                name: "ix_odemeler_uyelik_id",
                table: "odemeler",
                column: "uyelik_id");

            migrationBuilder.CreateIndex(
                name: "ix_olcumler_uye_id",
                table: "olcumler",
                column: "uye_id");

            migrationBuilder.CreateIndex(
                name: "ix_uyeler_telefon",
                table: "uyeler",
                column: "telefon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_uyelik_paketleri_ad",
                table: "uyelik_paketleri",
                column: "ad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_uyelikler_paket_id",
                table: "uyelikler",
                column: "paket_id");

            migrationBuilder.CreateIndex(
                name: "ix_uyelikler_uye_id",
                table: "uyelikler",
                column: "uye_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "antrenman_programlari");

            migrationBuilder.DropTable(
                name: "ders_kayitlari");

            migrationBuilder.DropTable(
                name: "giris_kayitlari");

            migrationBuilder.DropTable(
                name: "odemeler");

            migrationBuilder.DropTable(
                name: "olcumler");

            migrationBuilder.DropTable(
                name: "grup_dersleri");

            migrationBuilder.DropTable(
                name: "uyelikler");

            migrationBuilder.DropTable(
                name: "antrenorler");

            migrationBuilder.DropTable(
                name: "uyeler");

            migrationBuilder.DropTable(
                name: "uyelik_paketleri");
        }
    }
}
