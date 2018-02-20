﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WorldEvents.DBModel;

namespace WorldEvents.EntityFramework.Migrations.SatteliteDb
{
    [DbContext(typeof(SatteliteDbContext))]
    partial class SatteliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WorldEvents.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationRole");
                });

            modelBuilder.Entity("WorldEvents.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.Property<long?>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserProfileId")
                        .IsUnique()
                        .HasFilter("[UserProfileId] IS NOT NULL");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("WorldEvents.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WorldEvents.Entities.CategoryPermission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<bool>("Edit");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<long?>("ProjectRoleId");

                    b.Property<bool>("ReadOnly");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProjectRoleId");

                    b.ToTable("CategoryPermission");
                });

            modelBuilder.Entity("WorldEvents.Entities.CategorySubscription", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("CategorySubscription");
                });

            modelBuilder.Entity("WorldEvents.Entities.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime>("Date");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description")
                        .HasMaxLength(2048);

                    b.Property<bool>("IsCancelled");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<int>("MaxRegistrationCount");

                    b.Property<int>("TenantId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("AppEvents");
                });

            modelBuilder.Entity("WorldEvents.Entities.EventRegistration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<Guid>("EventId");

                    b.Property<int>("TenantId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("EventRegistration");
                });

            modelBuilder.Entity("WorldEvents.Entities.News", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CategoryId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long?>("NewsContentId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NewsContentId");

                    b.ToTable("News");
                });

            modelBuilder.Entity("WorldEvents.Entities.NewsContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BigImage");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("MediumImage");

                    b.Property<long>("NumOfView");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("SmallImage");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("NewsContents");
                });

            modelBuilder.Entity("WorldEvents.Entities.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<long?>("CoordinatorId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<long?>("ProjectContentId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CoordinatorId");

                    b.HasIndex("ProjectContentId")
                        .IsUnique()
                        .HasFilter("[ProjectContentId] IS NOT NULL");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("WorldEvents.Entities.ProjectContent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<long>("NumOfView");

                    b.Property<string>("ShortDescription")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ProjectContents");
                });

            modelBuilder.Entity("WorldEvents.Entities.ProjectMember", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("ProjectId");

                    b.Property<long?>("ProjectRoleId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ProjectRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectMembers");
                });

            modelBuilder.Entity("WorldEvents.Entities.ProjectRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ProjectRoles");
                });

            modelBuilder.Entity("WorldEvents.Entities.UserProfile", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool?>("Gender");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("WorldEvents.Entities.ApplicationUser", b =>
                {
                    b.HasOne("WorldEvents.Entities.ApplicationRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("WorldEvents.Entities.UserProfile", "UserProfile")
                        .WithOne("User")
                        .HasForeignKey("WorldEvents.Entities.ApplicationUser", "UserProfileId");
                });

            modelBuilder.Entity("WorldEvents.Entities.CategoryPermission", b =>
                {
                    b.HasOne("WorldEvents.Entities.Category", "Category")
                        .WithMany("Permissions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorldEvents.Entities.ProjectRole")
                        .WithMany("CategoryPermissions")
                        .HasForeignKey("ProjectRoleId");
                });

            modelBuilder.Entity("WorldEvents.Entities.CategorySubscription", b =>
                {
                    b.HasOne("WorldEvents.Entities.Category", "Category")
                        .WithMany("Subscriptions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorldEvents.Entities.ApplicationUser", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WorldEvents.Entities.EventRegistration", b =>
                {
                    b.HasOne("WorldEvents.Entities.Event", "Event")
                        .WithMany("Registrations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorldEvents.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WorldEvents.Entities.News", b =>
                {
                    b.HasOne("WorldEvents.Entities.Category", "Category")
                        .WithMany("News")
                        .HasForeignKey("CategoryId");

                    b.HasOne("WorldEvents.Entities.NewsContent", "NewsContent")
                        .WithMany()
                        .HasForeignKey("NewsContentId");
                });

            modelBuilder.Entity("WorldEvents.Entities.Project", b =>
                {
                    b.HasOne("WorldEvents.Entities.Category", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WorldEvents.Entities.UserProfile", "Coordinator")
                        .WithMany()
                        .HasForeignKey("CoordinatorId");

                    b.HasOne("WorldEvents.Entities.ProjectContent", "ProjectContent")
                        .WithOne("Project")
                        .HasForeignKey("WorldEvents.Entities.Project", "ProjectContentId");
                });

            modelBuilder.Entity("WorldEvents.Entities.ProjectMember", b =>
                {
                    b.HasOne("WorldEvents.Entities.Project")
                        .WithMany("ProjectMembers")
                        .HasForeignKey("ProjectId");

                    b.HasOne("WorldEvents.Entities.ProjectRole", "ProjectRole")
                        .WithMany()
                        .HasForeignKey("ProjectRoleId");

                    b.HasOne("WorldEvents.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
