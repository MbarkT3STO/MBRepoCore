using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace MBRepoCore.Models
{
    public partial class GVentesContext : DbContext,IDbContextFactory<GVentesContext>
    {

        public GVentesContext()
        {
        }

        public GVentesContext(DbContextOptions<GVentesContext> options)
            : base(options)
        {
        }

        public GVentesContext GetInstance(IConfiguration configuration, string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GVentesContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new GVentesContext(optionsBuilder.Options);
        }

        public virtual DbSet<ArchiveClient> ArchiveClients { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Commande> Commandes { get; set; }
        public virtual DbSet<DetFact> DetFacts { get; set; }
        public virtual DbSet<Facture> Factures { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Reglement> Reglements { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MBARK\\MBARK_SERVER;initial catalog=GVentes;Integrated Security=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ArchiveClient>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Nom).IsUnicode(false);

                entity.Property(e => e.Prenom).IsUnicode(false);

                entity.Property(e => e.Ville).IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Nom).IsUnicode(false);

                entity.Property(e => e.Prenom).IsUnicode(false);

                entity.Property(e => e.Ville).IsUnicode(false);
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Clt).IsUnicode(false);

                entity.Property(e => e.Prd).IsUnicode(false);

                entity.HasOne(d => d.CltNavigation)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.Clt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commande_Client");

                entity.HasOne(d => d.PrdNavigation)
                    .WithMany(p => p.Commandes)
                    .HasForeignKey(d => d.Prd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commande_Produit");
            });

            modelBuilder.Entity<DetFact>(entity =>
            {
                entity.Property(e => e.Cmd).IsUnicode(false);

                entity.Property(e => e.Fact).IsUnicode(false);

                entity.HasOne(d => d.CmdNavigation)
                    .WithMany(p => p.DetFacts)
                    .HasForeignKey(d => d.Cmd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Det_Fact_Commande");

                entity.HasOne(d => d.FactNavigation)
                    .WithMany(p => p.DetFacts)
                    .HasForeignKey(d => d.Fact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Det_Fact_Facture");
            });

            modelBuilder.Entity<Facture>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Clt).IsUnicode(false);

                entity.HasOne(d => d.CltNavigation)
                    .WithMany(p => p.Factures)
                    .HasForeignKey(d => d.Clt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facture_Client");
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.Property(e => e.Ref).IsUnicode(false);

                entity.Property(e => e.Desi).IsUnicode(false);
            });

            modelBuilder.Entity<Reglement>(entity =>
            {
                entity.Property(e => e.Id).IsUnicode(false);

                entity.Property(e => e.Fact).IsUnicode(false);

                entity.Property(e => e.TypeReg).IsUnicode(false);

                entity.HasOne(d => d.FactNavigation)
                    .WithMany(p => p.Reglements)
                    .HasForeignKey(d => d.Fact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reglement_Facture");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.Prd).IsUnicode(false);

                entity.HasOne(d => d.PrdNavigation)
                    .WithOne(p => p.Stock)
                    .HasForeignKey<Stock>(d => d.Prd)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Produit");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
