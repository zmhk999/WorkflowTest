using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RaceDay_API.Models;

public partial class RaceDayContext : DbContext
{
    public RaceDayContext()
    {
    }
    public RaceDayContext(DbContextOptions<RaceDayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventEnrolment> EventEnrolments { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RaceDay;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__6DB38D4E66E57DCD");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Category_ID");
            entity.Property(e => e.AgeCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Age_Category");
            entity.Property(e => e.DistanceCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Distance_Category");
            entity.Property(e => e.EventsId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Events_ID");

            entity.HasOne(d => d.Events).WithMany(p => p.Categories)
                .HasForeignKey(d => d.EventsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Category__Events__251C81ED");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventsId).HasName("PK__Events__E143B6CB91BF4A31");

            entity.Property(e => e.EventsId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Events_ID");
            entity.Property(e => e.EventDate).HasColumnName("Event_Date");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Description");
            entity.Property(e => e.EventDistance)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Distance");
            entity.Property(e => e.EventLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Location");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Name");
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Type");
        });

        modelBuilder.Entity<EventEnrolment>(entity =>
        {
            entity.HasKey(e => e.EventEnrolmentId).HasName("PK__Event_En__5D541F9D9A3047F9");

            entity.ToTable("Event_Enrolment");

            entity.Property(e => e.EventEnrolmentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Enrolment_ID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Category_ID");
            entity.Property(e => e.EventsId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Events_ID");
            entity.Property(e => e.UserProfileId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Profile_ID");

            entity.HasOne(d => d.Category).WithMany(p => p.EventEnrolments)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Enr__Categ__39237A9A");

            entity.HasOne(d => d.Events).WithMany(p => p.EventEnrolments)
                .HasForeignKey(d => d.EventsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Enr__Event__382F5661");

            entity.HasOne(d => d.UserProfile).WithMany(p => p.EventEnrolments)
                .HasForeignKey(d => d.UserProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Event_Enr__User___3A179ED3");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultsId).HasName("PK__Results__63E95BDD3EAA4169");

            entity.Property(e => e.ResultsId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Results_ID");
            entity.Property(e => e.EventEnrolmentId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Event_Enrolment_ID");
            entity.Property(e => e.FinishTimes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Finish_Times");
            entity.Property(e => e.FinishingPositions)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Finishing_Positions");

            entity.HasOne(d => d.EventEnrolment).WithMany(p => p.Results)
                .HasForeignKey(d => d.EventEnrolmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Results__Event_E__3CF40B7E");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("PK__User_Pro__C69B990E39B5B264");

            entity.ToTable("User_Profile");

            entity.HasIndex(e => e.UserName, "UQ__User_Pro__681E8A607DCF2056").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__User_Pro__A9D1053433225967").IsUnique();

            entity.Property(e => e.UserProfileId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Profile_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RegistrationDate).HasColumnName("Registration_Date");
            entity.Property(e => e.UserAge)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Age");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Password");
            entity.Property(e => e.UserRoleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Role_ID");

            entity.HasOne(d => d.UserRole).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User_Prof__User___336AA144");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__User_Rol__134E48EC9C6062B9");

            entity.ToTable("User_Role");

            entity.Property(e => e.UserRoleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Role_ID");
            entity.Property(e => e.UserFirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_FirstName");
            entity.Property(e => e.UserLastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_LastName");
            entity.Property(e => e.UserRoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Role_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
