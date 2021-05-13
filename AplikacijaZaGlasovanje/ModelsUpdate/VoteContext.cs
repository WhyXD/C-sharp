using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class VoteContext : DbContext
    {
        public VoteContext()
        {
        }
     
        public VoteContext(DbContextOptions<VoteContext> options)
            : base(options)
        {
        }
     

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Glasovanja> Glasovanja { get; set; }
        public virtual DbSet<MozniOdgovori> MozniOdgovori { get; set; }
        public virtual DbSet<OddaniOdgovori> OddaniOdgovori { get; set; }
        public virtual DbSet<Odgovori> Odgovori { get; set; }
        public virtual DbSet<PravilniOdgovori> PravilniOdgovori { get; set; }
        public virtual DbSet<SeznamVprasanj> SeznamVprasanj { get; set; }
        public virtual DbSet<Vprasanja> Vprasanja { get; set; }
        public virtual DbSet<Vprasanjas> Vprasanjas { get; set; }


        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("povezava");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany()
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.LoginProvider)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Glasovanja>(entity =>
            {
                entity.HasKey(e => e.GlasovanjeId);

                entity.Property(e => e.GlasovanjeId).HasColumnName("GlasovanjeID");

                entity.Property(e => e.GlasovanjeSeKonca).HasColumnType("date");

                entity.Property(e => e.GlasovanjeSeZacne).HasColumnType("date");

                entity.Property(e => e.ImeGlasovanja)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UporabnikiId)
                    .IsRequired()
                    .HasColumnName("UporabnikiID")
                    .HasMaxLength(450);

                entity.Property(e => e.VprasanjeId).HasColumnName("VprasanjeID");

                entity.HasOne(d => d.Uporabniki)
                    .WithMany(p => p.Glasovanja)
                    .HasForeignKey(d => d.UporabnikiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Glasovanja_AspNetUsers1");
            });

            modelBuilder.Entity<MozniOdgovori>(entity =>
            {
                entity.HasKey(e => e.MozenOdgovorId);

                entity.Property(e => e.MozenOdgovorId).HasColumnName("MozenOdgovorID");

                entity.Property(e => e.Odgovor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VprasanjeIzSeznamaId).HasColumnName("VprasanjeIzSeznamaID");

                entity.HasOne(d => d.VprasanjeIzSeznama)
                    .WithMany(p => p.MozniOdgovori)
                    .HasForeignKey(d => d.VprasanjeIzSeznamaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MozniOdgovori_SeznamVprasanj");
            });

            modelBuilder.Entity<OddaniOdgovori>(entity =>
            {
                entity.Property(e => e.OddaniOdgovoriId).HasColumnName("OddaniOdgovoriID");

                entity.Property(e => e.OddaniOdgovor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UporabnikId)
                    .IsRequired()
                    .HasColumnName("UporabnikID")
                    .HasMaxLength(450);

                entity.Property(e => e.VprasanjeIzSeznamaId).HasColumnName("VprasanjeIzSeznamaID");

                entity.HasOne(d => d.Uporabnik)
                    .WithMany(p => p.OddaniOdgovori)
                    .HasForeignKey(d => d.UporabnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OddaniOdgovori_AspNetUsers");

                entity.HasOne(d => d.VprasanjeIzSeznama)
                    .WithMany(p => p.OddaniOdgovori)
                    .HasForeignKey(d => d.VprasanjeIzSeznamaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OddaniOdgovori_SeznamVprasanj");
               
            });

            modelBuilder.Entity<Odgovori>(entity =>
            {
                entity.Property(e => e.OdgovoriId).HasColumnName("OdgovoriID");

                entity.Property(e => e.OddaniOdgovoriId).HasColumnName("OddaniOdgovoriID");

                entity.Property(e => e.PravilniOdgovoriId).HasColumnName("PravilniOdgovoriID");

                entity.Property(e => e.VprasanjeIzSeznamaId).HasColumnName("VprasanjeIzSeznamaID");

                entity.HasOne(d => d.VprasanjeIzSeznama)
                    .WithMany(p => p.Odgovori)
                    .HasForeignKey(d => d.VprasanjeIzSeznamaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Odgovori_SeznamVprasanj");
                //--
                entity.HasOne(d => d.OddaniOdgovori)
                    .WithMany(p => p.Odgovori)
                    .HasForeignKey(d => d.OddaniOdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Odgovori_OddaniOdgovori");
                //--
                entity.HasOne(d => d.PravilniOdgovori)
                    .WithMany(p => p.Odgovori)
                    .HasForeignKey(d => d.PravilniOdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Odgovori_PravilniOdgovori");
            });

            modelBuilder.Entity<PravilniOdgovori>(entity =>
            {
                entity.Property(e => e.PravilniOdgovoriId).HasColumnName("PravilniOdgovoriID");

                entity.Property(e => e.PravilniOdgovor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VprasanjeIzSeznamaId).HasColumnName("VprasanjeIzSeznamaID");

                entity.HasOne(d => d.VprasanjeIzSeznama)
                    .WithMany(p => p.PravilniOdgovori)
                    .HasForeignKey(d => d.VprasanjeIzSeznamaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PravilniOdgovori_SeznamVprasanj");
            });

            modelBuilder.Entity<SeznamVprasanj>(entity =>
            {
                entity.Property(e => e.SeznamVprasanjId).HasColumnName("SeznamVprasanjID");

                entity.Property(e => e.VprasanjeIzSeznama)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Vprasanja>(entity =>
            {
                entity.Property(e => e.VprasanjaId).HasColumnName("VprasanjaID");

                entity.Property(e => e.GlasovanjeId).HasColumnName("GlasovanjeID");

                entity.Property(e => e.MozniOdgovoriId).HasColumnName("MozniOdgovoriID");

                entity.Property(e => e.OddaniOdgovoriId).HasColumnName("OddaniOdgovoriID");

                entity.Property(e => e.OdgovoriId).HasColumnName("OdgovoriID");

                entity.Property(e => e.PravilniOdgovoriId).HasColumnName("PravilniOdgovoriID");

                entity.Property(e => e.VprasanjeIzSeznamaId).HasColumnName("VprasanjeIzSeznamaID");

                entity.HasOne(d => d.Glasovanje)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.GlasovanjeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_Glasovanja");

                entity.HasOne(d => d.MozniOdgovori)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.MozniOdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_MozniOdgovori1");

                entity.HasOne(d => d.OddaniOdgovori)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.OddaniOdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_OddaniOdgovori1");

                entity.HasOne(d => d.Odgovori)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.OdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_Odgovori1");

                entity.HasOne(d => d.PravilniOdgovori)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.PravilniOdgovoriId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_PravilniOdgovori1");

                entity.HasOne(d => d.VprasanjeIzSeznama)
                    .WithMany(p => p.Vprasanja)
                    .HasForeignKey(d => d.VprasanjeIzSeznamaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vprasanja_SeznamVprasanj");
            });

            modelBuilder.Entity<Vprasanjas>(entity =>
            {
                entity.Property(e => e.IzbranOdgovor).HasColumnName("Izbran_Odgovor");

                entity.Property(e => e.OdgovorA).HasColumnName("Odgovor_A");

                entity.Property(e => e.OdgovorB).HasColumnName("Odgovor_B");

                entity.Property(e => e.OdgovorC).HasColumnName("Odgovor_C");

                entity.Property(e => e.OdgovorD).HasColumnName("Odgovor_D");

                entity.Property(e => e.PravilniOdgovor).HasColumnName("Pravilni_Odgovor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
