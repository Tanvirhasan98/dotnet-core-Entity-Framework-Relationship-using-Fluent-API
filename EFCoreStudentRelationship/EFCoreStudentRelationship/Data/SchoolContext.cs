using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreStudentRelationship.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {


        }

 

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades{ get; set; }


      
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>()
                .HasOne<Student>(s => s.Student)
                .WithOne(ad => ad.Grades)
                .HasForeignKey<Grade>(ad => ad.StudentId);

            modelBuilder.Entity<StudentAddress>()
                .HasOne<Student>(s => s.Student)
                .WithMany(ad => ad.Addresses)
                .HasForeignKey(ad => ad.StudentId);

            modelBuilder.Entity<Course>()
             .HasMany(p => p.Students)
             .WithMany(p => p.Courses)
             .UsingEntity(j => j.ToTable("CourseStudent"));







        }





    }
}
