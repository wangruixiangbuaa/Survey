namespace HPIT.Survey.Data.Entitys
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SurveyContext : DbContext
    {

        public static SurveyContext Instance = new SurveyContext();
        public SurveyContext()
            : base("name=SurveyContext")
        {
        }

        public virtual DbSet<ActiveJobs> ActiveJobs { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Dictionary> Dictionary { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RoleMenus> RoleMenus { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SkillTag> SkillTag { get; set; }
        public virtual DbSet<SkillTags> SkillTags { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentTags> StudentTags { get; set; }
        public virtual DbSet<SurveyModel> SurveyModel { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.CompanyNo)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectType)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectDesc)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.ClassNo)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.WagesOfPeriod)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Student>()
                .Property(e => e.WagesOfFull)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Student>()
                .Property(e => e.WagesOfReal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Student>()
                .Property(e => e.CurrentCompanyNo)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Batch)
                .IsUnicode(false);

            modelBuilder.Entity<SurveyModel>()
                .Property(e => e.SurveyTickNumber)
                .IsUnicode(false);

            modelBuilder.Entity<SurveyModel>()
                .Property(e => e.WagesOfPeriod)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SurveyModel>()
                .Property(e => e.WagesOfFull)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SurveyModel>()
                .Property(e => e.WagesOfReal)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Users>()
                .HasOptional(e => e.Department)
                .WithRequired(e => e.Users);
        }
    }
}
