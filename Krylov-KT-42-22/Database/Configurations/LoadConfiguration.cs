﻿using Krylov_KT_42_22.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Krylov_KT_42_22.Database.Configurations
{
    public class LoadConfiguration : IEntityTypeConfiguration<Load>
    {
        private const string TableName = "Loads";
        public void Configure(EntityTypeBuilder<Load> builder)
        {
            builder.HasKey(p => p.Id)
               .HasName($"pl_{TableName}_load_id");

            //Автоинкрементация
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Load_Id")
                .HasComment("Id нагрузки");

            // Настройка связи c teachers
            builder.Property(p => p.TeacherId)
                .HasColumnName("Teacher_Id")
                .HasComment("Id преподавателя");


            builder.HasOne(p => p.Teacher)
                .WithMany()
                .HasForeignKey(p => p.TeacherId)
                .HasConstraintName("fk_f_teacher_id");
                

            builder.ToTable(TableName)
                .HasIndex(p => p.TeacherId, $"idx_{TableName}_fk_f_teacher_id");

            // Настройка связи c дисциплинами
            builder.Property(p => p.DisciplineId)
                .HasColumnName("Discipline_Id")
                .HasComment("Id дисциплины");


            builder.HasOne(p => p.Discipline)
                .WithMany()
                .HasForeignKey(p => p.DisciplineId)
                .HasConstraintName("fk_f_discipline_id")
                .OnDelete(DeleteBehavior.Cascade); //Пересмотреть

            builder.ToTable(TableName)
                .HasIndex(p => p.DisciplineId, $"idx_{TableName}_fk_f_discipline_id");

            builder.Property(p => p.Hours)
                .IsRequired()
                .HasColumnName("Hours")
                .HasComment("Количество часов нагрузки");

            builder.ToTable(TableName);
        }

    }
}
