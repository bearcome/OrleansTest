using KWKY.Common;
using KWKY.Model.DBModel;
using Microsoft.EntityFrameworkCore;

namespace KWKY.DBUtility
{
#warning    全局筛选器 可使程序员专注业务  https://docs.microsoft.com/zh-cn/ef/core/querying/filters
    //[DataType(DataType.Date)]
    //DataType 特性指定比数据库内部类型更具体的数据类型
    //DisplayFormat 特性用于显式指定日期格式
    //[Display]
    public partial class KWKYDBContext : DbContext
    {
        public KWKYDBContext ()
        {
        }

        public KWKYDBContext (DbContextOptions<KWKYDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DemoTable> DemoTable { get; set; }
        public virtual DbSet<OrleansMembershipTable> OrleansMembershipTable { get; set; }
        public virtual DbSet<OrleansMembershipVersionTable> OrleansMembershipVersionTable { get; set; }
        public virtual DbSet<OrleansQuery> OrleansQuery { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if ( !optionsBuilder.IsConfigured )
            {
                string connString =  AppConfiguration.GetConnectionString("KWKYOrleans");
                optionsBuilder.UseSqlServer(connString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
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
                //.ValueGeneratedOnAddOrUpdate().HasComputedColumnSql("(getutcdate())")
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

        partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
    }
}
