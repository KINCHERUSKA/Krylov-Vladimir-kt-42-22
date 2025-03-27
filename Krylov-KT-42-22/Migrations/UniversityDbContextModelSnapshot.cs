﻿// <auto-generated />
using System;
using Krylov_KT_42_22.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Krylov_KT_42_22.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    partial class UniversityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Krylov_KT_42_22.Models.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("degree_Id")
                        .HasComment("Id уч. степени");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("Name")
                        .HasComment("Название уч. степени");

                    b.HasKey("Id")
                        .HasName("pl_Degree_degree_id");

                    b.ToTable("Degree", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Department_Id")
                        .HasComment("Id факультета");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("FoundedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("Founded_Date")
                        .HasComment("Дата основания факультета");

                    b.Property<int?>("HeadId")
                        .HasColumnType("integer")
                        .HasColumnName("Head_Id")
                        .HasComment("Id зав. кафедры");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("Name")
                        .HasComment("Название факультета");

                    b.HasKey("Id")
                        .HasName("pl_Departments_department_id");

                    b.HasIndex("HeadId")
                        .IsUnique();

                    b.HasIndex(new[] { "HeadId" }, "idx_Departments_fk_f_head_id");

                    b.ToTable("Departments", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Discipline_Id")
                        .HasComment("Id дисциплины");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Discipline_Name")
                        .HasComment("Название дисциплины");

                    b.HasKey("Id")
                        .HasName("pl_Disciplines_discipline_id");

                    b.HasIndex("Name")
                        .HasDatabaseName("idx_Disciplines_name");

                    b.ToTable("Disciplines", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Load", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Load_Id")
                        .HasComment("Id нагрузки");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DisciplineId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("Discipline_Id")
                        .HasComment("Id дисциплины");

                    b.Property<int>("Hours")
                        .HasColumnType("integer")
                        .HasColumnName("Hours")
                        .HasComment("Количество часов нагрузки");

                    b.Property<int?>("TeacherId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("Teacher_Id")
                        .HasComment("Id преподавателя");

                    b.HasKey("Id")
                        .HasName("pl_Loads_load_id");

                    b.HasIndex(new[] { "DisciplineId" }, "idx_Loads_fk_f_discipline_id");

                    b.HasIndex(new[] { "TeacherId" }, "idx_Loads_fk_f_teacher_id");

                    b.ToTable("Loads", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Position_Id")
                        .HasComment("Id должности");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar")
                        .HasColumnName("Position_Name")
                        .HasComment("Название должности");

                    b.HasKey("Id")
                        .HasName("pl_Positions_position_id");

                    b.ToTable("Positions", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Teacher_Id")
                        .HasComment("Id преподавателя");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DegreeId")
                        .IsRequired()
                        .HasColumnType("int4")
                        .HasColumnName("Degree_Id")
                        .HasComment("Id ученой степени");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int4")
                        .HasColumnName("Department_Id")
                        .HasComment("Id кафедры");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("First_Name")
                        .HasComment("Имя преподавателя");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("Last_Name")
                        .HasComment("Отчество преподавателя");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar")
                        .HasColumnName("Middle_Name")
                        .HasComment("Фамилия преподавателя");

                    b.Property<int>("PositionId")
                        .HasColumnType("int4")
                        .HasColumnName("Position_Id")
                        .HasComment("Id занимаемой должности");

                    b.HasKey("Id")
                        .HasName("pl_Teachers_teacher_id");

                    b.HasIndex("PositionId");

                    b.HasIndex(new[] { "DegreeId" }, "idx_Teachers_fk_f_degree_id");

                    b.HasIndex(new[] { "DepartmentId" }, "idx_Teachers_fk_f_department_id");

                    b.HasIndex(new[] { "DegreeId" }, "idx_Teachers_fk_f_position_id");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Department", b =>
                {
                    b.HasOne("Krylov_KT_42_22.Models.Teacher", "Head")
                        .WithOne()
                        .HasForeignKey("Krylov_KT_42_22.Models.Department", "HeadId")
                        .HasConstraintName("fk_f_head_id");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Load", b =>
                {
                    b.HasOne("Krylov_KT_42_22.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_f_discipline_id");

                    b.HasOne("Krylov_KT_42_22.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_f_teacher_id");

                    b.Navigation("Discipline");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Krylov_KT_42_22.Models.Teacher", b =>
                {
                    b.HasOne("Krylov_KT_42_22.Models.Degree", "Degree")
                        .WithMany()
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_f_degree_id");

                    b.HasOne("Krylov_KT_42_22.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_f_department_id");

                    b.HasOne("Krylov_KT_42_22.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_f_position_id");

                    b.Navigation("Degree");

                    b.Navigation("Department");

                    b.Navigation("Position");
                });
#pragma warning restore 612, 618
        }
    }
}
