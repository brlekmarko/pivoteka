using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace Pivoteka.DataAccess.PostgreSQL.Data
{
    public partial class PivotekaContext : DbContext
    {
        public PivotekaContext()
        {
        }

        public PivotekaContext(DbContextOptions<PivotekaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dobavljac> Dobavljac { get; set; } = null!;
        public virtual DbSet<Korisnik> Korisnik { get; set; } = null!;
        public virtual DbSet<Kupac> Kupac { get; set; } = null!;
        public virtual DbSet<NarucioPivo> NarucioPivo { get; set; } = null!;
        public virtual DbSet<Narudzba> Narudzba { get; set; } = null!;
        public virtual DbSet<Ocjena> Ocjena { get; set; } = null!;
        public virtual DbSet<Pivo> Pivo { get; set; } = null!;
        public virtual DbSet<Vrstum> Vrstum { get; set; } = null!;
        public virtual DbSet<Zaposlenik> Zaposlenik { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=pivoteka;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dobavljac>(entity =>
            {
                entity.HasKey(e => e.Ime)
                    .HasName("dobavljac_pkey");

                entity.ToTable("dobavljac");

                entity.Property(e => e.Ime)
                    .HasMaxLength(64)
                    .HasColumnName("ime");

                entity.Property(e => e.Adresa)
                    .HasMaxLength(256)
                    .HasColumnName("adresa");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email");
            });

            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.HasKey(e => e.KorisnickoIme)
                    .HasName("korisnik_pkey");

                entity.ToTable("korisnik");

                entity.Property(e => e.KorisnickoIme)
                    .HasMaxLength(32)
                    .HasColumnName("korisnicko_ime");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .HasColumnName("email");

                entity.Property(e => e.Ime)
                    .HasMaxLength(64)
                    .HasColumnName("ime");

                entity.Property(e => e.Lozinka)
                    .HasMaxLength(256)
                    .HasColumnName("lozinka");

                entity.Property(e => e.Prezime)
                    .HasMaxLength(64)
                    .HasColumnName("prezime");
            });

            modelBuilder.Entity<Kupac>(entity =>
            {
                entity.HasKey(e => e.KorisnickoIme)
                    .HasName("kupac_pkey");

                entity.ToTable("kupac");

                entity.Property(e => e.KorisnickoIme)
                    .HasMaxLength(32)
                    .HasColumnName("korisnicko_ime");

                entity.Property(e => e.AdresaDostave)
                    .HasMaxLength(256)
                    .HasColumnName("adresa_dostave");

                entity.HasOne(d => d.KorisnickoImeNavigation)
                    .WithOne(p => p.Kupac)
                    .HasForeignKey<Kupac>(d => d.KorisnickoIme)
                    .HasConstraintName("kupac_korisnicko_ime_fkey");
            });

            modelBuilder.Entity<NarucioPivo>(entity =>
            {
                entity.HasKey(e => new { e.ImePiva, e.IdNarudzbe })
                    .HasName("narucio_pivo_pkey");

                entity.ToTable("narucio_pivo");

                entity.Property(e => e.ImePiva)
                    .HasMaxLength(64)
                    .HasColumnName("ime_piva");

                entity.Property(e => e.IdNarudzbe).HasColumnName("id_narudzbe");

                entity.Property(e => e.CijenaPiva)
                    .HasPrecision(8, 2)
                    .HasColumnName("cijena_piva");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.HasOne(d => d.IdNarudzbeNavigation)
                    .WithMany(p => p.NarucioPivos)
                    .HasForeignKey(d => d.IdNarudzbe)
                    .HasConstraintName("narucio_pivo_id_narudzbe_fkey");

                entity.HasOne(d => d.ImePivaNavigation)
                    .WithMany(p => p.NarucioPivos)
                    .HasForeignKey(d => d.ImePiva)
                    .HasConstraintName("narucio_pivo_ime_piva_fkey");
            });

            modelBuilder.Entity<Narudzba>(entity =>
            {
                entity.ToTable("narudzba");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Datum).HasColumnName("datum");

                entity.Property(e => e.KorisnickoIme)
                    .HasMaxLength(32)
                    .HasColumnName("korisnicko_ime");

                entity.Property(e => e.UkupnaCijena)
                    .HasPrecision(8, 2)
                    .HasColumnName("ukupna_cijena");

                entity.HasOne(d => d.KorisnickoImeNavigation)
                    .WithMany(p => p.Narudzbas)
                    .HasForeignKey(d => d.KorisnickoIme)
                    .HasConstraintName("narudzba_korisnicko_ime_fkey");
            });

            modelBuilder.Entity<Ocjena>(entity =>
            {
                entity.HasKey(e => new { e.KorisnickoImeKupca, e.ImePiva })
                    .HasName("ocjena_pkey");

                entity.ToTable("ocjena");

                entity.Property(e => e.KorisnickoImeKupca)
                    .HasMaxLength(32)
                    .HasColumnName("korisnicko_ime_kupca");

                entity.Property(e => e.ImePiva)
                    .HasMaxLength(64)
                    .HasColumnName("ime_piva");

                entity.Property(e => e.Ocjena1).HasColumnName("ocjena");

                entity.Property(e => e.Tekst)
                    .HasMaxLength(512)
                    .HasColumnName("tekst");

                entity.HasOne(d => d.ImePivaNavigation)
                    .WithMany(p => p.Ocjenas)
                    .HasForeignKey(d => d.ImePiva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ocjena_ime_piva_fkey");

                entity.HasOne(d => d.KorisnickoImeKupcaNavigation)
                    .WithMany(p => p.Ocjenas)
                    .HasForeignKey(d => d.KorisnickoImeKupca)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ocjena_korisnicko_ime_kupca_fkey");
            });

            modelBuilder.Entity<Pivo>(entity =>
            {
                entity.HasKey(e => e.Ime)
                    .HasName("pivo_pkey");

                entity.ToTable("pivo");

                entity.Property(e => e.Ime)
                    .HasMaxLength(64)
                    .HasColumnName("ime");

                entity.Property(e => e.Cijena)
                    .HasPrecision(8, 2)
                    .HasColumnName("cijena");

                entity.Property(e => e.ImeDobavljaca)
                    .HasMaxLength(64)
                    .HasColumnName("ime_dobavljaca");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.NetoVolumen).HasColumnName("neto_volumen");

                entity.Property(e => e.Opis)
                    .HasMaxLength(512)
                    .HasColumnName("opis");

                entity.Property(e => e.Vrsta)
                    .HasMaxLength(64)
                    .HasColumnName("vrsta");

                entity.Property(e => e.ZemljaPodrijetla)
                    .HasMaxLength(64)
                    .HasColumnName("zemlja_podrijetla");

                entity.HasOne(d => d.ImeDobavljacaNavigation)
                    .WithMany(p => p.Pivos)
                    .HasForeignKey(d => d.ImeDobavljaca)
                    .HasConstraintName("pivo_ime_dobavljaca_fkey");

                entity.HasOne(d => d.VrstaNavigation)
                    .WithMany(p => p.Pivos)
                    .HasForeignKey(d => d.Vrsta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pivo_vrsta_fkey");
            });

            modelBuilder.Entity<Vrstum>(entity =>
            {
                entity.HasKey(e => e.Ime)
                    .HasName("vrsta_pkey");

                entity.ToTable("vrsta");

                entity.Property(e => e.Ime)
                    .HasMaxLength(64)
                    .HasColumnName("ime");
            });

            modelBuilder.Entity<Zaposlenik>(entity =>
            {
                entity.HasKey(e => e.KorisnickoIme)
                    .HasName("zaposlenik_pkey");

                entity.ToTable("zaposlenik");

                entity.Property(e => e.KorisnickoIme)
                    .HasMaxLength(32)
                    .HasColumnName("korisnicko_ime");

                entity.Property(e => e.DatumZaposljenja).HasColumnName("datum_zaposljenja");

                entity.Property(e => e.KrajZaposlenja).HasColumnName("kraj_zaposlenja");

                entity.HasOne(d => d.KorisnickoImeNavigation)
                    .WithOne(p => p.Zaposlenik)
                    .HasForeignKey<Zaposlenik>(d => d.KorisnickoIme)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("zaposlenik_korisnicko_ime_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
