using DotNetCoreArchitecture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DotNetCoreArchitecture.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190101000000_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DotNetCoreArchitecture.Domain.UserEntity", b =>
            {
                b.Property<long>("UserId").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                b.Property<string>("Email").IsRequired().HasMaxLength(300);
                b.Property<int>("Roles");
                b.Property<int>("Status");
                b.HasKey("UserId");
                b.HasIndex("Email").IsUnique();
                b.ToTable("Users", "User");
                b.HasData(new { UserId = 1L, Email = "administrator@administrator.com", Roles = 3, Status = 1 });
            });

            modelBuilder.Entity("DotNetCoreArchitecture.Domain.UserLogEntity", b =>
            {
                b.Property<long>("UserLogId").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                b.Property<DateTime>("DateTime");
                b.Property<int>("LogType");
                b.Property<long>("UserId");
                b.HasKey("UserLogId");
                b.HasIndex("UserId");
                b.ToTable("UsersLogs", "User");
            });

            modelBuilder.Entity("DotNetCoreArchitecture.Domain.UserEntity", b =>
            {
                b.OwnsOne("DotNetCoreArchitecture.Domain.FullName", "FullName", b1 =>
                {
                    b1.Property<long>("UserEntityUserId").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                    b1.Property<string>("Name").IsRequired().HasMaxLength(100);
                    b1.Property<string>("Surname").IsRequired().HasMaxLength(200);
                    b1.HasKey("UserEntityUserId");
                    b1.ToTable("Users", "User");
                    b1.HasOne("DotNetCoreArchitecture.Domain.UserEntity").WithOne("FullName").HasForeignKey("DotNetCoreArchitecture.Domain.FullName", "UserEntityUserId").OnDelete(DeleteBehavior.Cascade);
                    b1.HasData(new { UserEntityUserId = 1L, Name = "Administrator", Surname = "Administrator" });
                });

                b.OwnsOne("DotNetCoreArchitecture.Domain.SignIn", "SignIn", b1 =>
                {
                    b1.Property<long>("UserEntityUserId").ValueGeneratedOnAdd().HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                    b1.Property<string>("Login").IsRequired().HasMaxLength(100);
                    b1.Property<string>("Password").IsRequired().HasMaxLength(500);
                    b1.HasKey("UserEntityUserId");
                    b1.HasIndex("Login").IsUnique();
                    b1.ToTable("Users", "User");
                    b1.HasOne("DotNetCoreArchitecture.Domain.UserEntity").WithOne("SignIn").HasForeignKey("DotNetCoreArchitecture.Domain.SignIn", "UserEntityUserId").OnDelete(DeleteBehavior.Cascade);
                    b1.HasData(new { UserEntityUserId = 1L, Login = "admin", Password = "1h0ATANFe6x7kMHo1PURE74WI0ayevUwfK/+Ie+IWX/xWrFWngcVUwL/ewryn38EMVMQBFaNo4SaVwgXaBWnDw==" });
                });
            });

            modelBuilder.Entity("DotNetCoreArchitecture.Domain.UserLogEntity", b =>
            {
                b.HasOne("DotNetCoreArchitecture.Domain.UserEntity", "User").WithMany("UsersLogs").HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
