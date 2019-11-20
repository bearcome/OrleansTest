using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KWKY.Model.DBModel
{
    public partial class KWKYDBContext : DbContext
    {
        public KWKYDBContext()
        {
        }

        public KWKYDBContext(DbContextOptions<KWKYDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DemoTable> DemoTable { get; set; }
        public virtual DbSet<OrleansMembershipTable> OrleansMembershipTable { get; set; }
        public virtual DbSet<OrleansMembershipVersionTable> OrleansMembershipVersionTable { get; set; }
        public virtual DbSet<OrleansQuery> OrleansQuery { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase"));//"Server=192.168.1.30;Database=OrleansDemo;uid=kwjq;pwd=Test123;"
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrleansMembershipTable>(entity =>
            {
                entity.HasKey(e => new { e.DeploymentId, e.Address, e.Port, e.Generation })
                    .HasName("PK_MembershipTable_DeploymentId");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.SuspectTimes).IsUnicode(false);

                entity.HasOne(d => d.Deployment)
                    .WithMany(p => p.OrleansMembershipTable)
                    .HasForeignKey(d => d.DeploymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MembershipTable_MembershipVersionTable_DeploymentId");
            });

            modelBuilder.Entity<OrleansMembershipVersionTable>(entity =>
            {
                entity.HasKey(e => e.DeploymentId)
                    .HasName("PK_OrleansMembershipVersionTable_DeploymentId");

                entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<OrleansQuery>(entity =>
            {
                entity.HasKey(e => e.QueryKey)
                    .HasName("OrleansQuery_Key");

                entity.Property(e => e.QueryKey).IsUnicode(false);

                entity.Property(e => e.QueryText).IsUnicode(false);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => new { e.GrainIdHash, e.GrainTypeHash })
                    .HasName("IX_Storage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
