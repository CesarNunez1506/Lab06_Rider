using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using Microsoft.Extensions.Configuration;

namespace LAB06_Cesar.Models;

public partial class Lab06DbContext : DbContext
{
    public Lab06DbContext()
    {
    }

    public Lab06DbContext(DbContextOptions<Lab06DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PRIMARY");

            entity.ToTable("asistencias");

            entity.Property(e => e.IdAsistencia)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_asistencia");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso)
                .HasColumnType("int(11)")
                .HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante)
                .HasColumnType("int(11)")
                .HasColumnName("id_estudiante");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PRIMARY");

            entity.ToTable("cursos");

            entity.Property(e => e.IdCurso)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_curso");
            entity.Property(e => e.Creditos)
                .HasColumnType("int(11)")
                .HasColumnName("creditos");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PRIMARY");

            entity.ToTable("estudiantes");

            entity.Property(e => e.IdEstudiante)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_estudiante");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad)
                .HasColumnType("int(11)")
                .HasColumnName("edad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("PRIMARY");

            entity.ToTable("evaluaciones");

            entity.Property(e => e.IdEvaluacion)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_evaluacion");
            entity.Property(e => e.Calificacion)
                .HasPrecision(5, 2)
                .HasColumnName("calificacion");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCurso)
                .HasColumnType("int(11)")
                .HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante)
                .HasColumnType("int(11)")
                .HasColumnName("id_estudiante");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("PRIMARY");

            entity.ToTable("materias");

            entity.Property(e => e.IdMateria)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_materia");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.IdCurso)
                .HasColumnType("int(11)")
                .HasColumnName("id_curso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("PRIMARY");

            entity.ToTable("matriculas");

            entity.Property(e => e.IdMatricula)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_matricula");
            entity.Property(e => e.IdCurso)
                .HasColumnType("int(11)")
                .HasColumnName("id_curso");
            entity.Property(e => e.IdEstudiante)
                .HasColumnType("int(11)")
                .HasColumnName("id_estudiante");
            entity.Property(e => e.Semestre)
                .HasMaxLength(20)
                .HasColumnName("semestre");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PRIMARY");

            entity.ToTable("profesores");

            entity.Property(e => e.IdProfesor)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_profesor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Especialidad)
                .HasMaxLength(100)
                .HasColumnName("especialidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.User, "user").IsUnique();

            entity.Property(e => e.IdUser)
                .HasColumnType("bigint(20) unsigned")
                .HasColumnName("id_user");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .HasColumnName("user");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
