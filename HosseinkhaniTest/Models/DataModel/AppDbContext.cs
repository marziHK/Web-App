using Microsoft.EntityFrameworkCore;

namespace HosseinkhaniTest.Models.DataModel
{
    public class AppDbContext : DbContext
    {


        public AppDbContext()
        {
        }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Tbl_Personnels> Tbl_Personnels { get; set; }
        public virtual DbSet<Tbl_PersonnelDocument> Tbl_PersonnelDocument { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Tbl_Personnels>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Tbl_Personnels");

                entity.Property(e => e.State).HasDefaultValueSql("((0))");


            });

            modelBuilder.Entity<Tbl_PersonnelDocument>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Tbl_PersonnelDocument");

                entity.HasIndex(e => e.FK_PersonnelId, "IX_Tbl_Personnels_Tbl_PersonnelDocument");


                entity.HasOne(d => d.Personnel)
                    .WithMany(p => p.Tbl_PersonnelDocument)
                    .HasForeignKey(d => d.FK_PersonnelId)
                    .HasConstraintName("FK_Tbl_Personnels_Tbl_PersonnelDocument");
            });


        }

    }
}
