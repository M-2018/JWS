﻿// <auto-generated />
using System;
using JWS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JWS.Migrations
{
    [DbContext(typeof(APIAppDbContext))]
    [Migration("20241012180119_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JWS.Models.Asistencia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool?>("Ausente")
                        .HasColumnType("bit");

                    b.Property<long>("CicloId")
                        .HasColumnType("bigint");

                    b.Property<long>("EstudianteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<long>("MateriaId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Presente")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Asistencias");
                });

            modelBuilder.Entity("JWS.Models.Calificacion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CicloId")
                        .HasColumnType("bigint");

                    b.Property<long>("EstudianteId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("Habilitacion")
                        .HasColumnType("bit");

                    b.Property<long>("MateriaId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("NotaActitudinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaDefinitiva")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaEvaluacion1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaEvaluacion2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaExamenFinal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaTrabajo1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NotaTrabajo2")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool?>("Recuperacion")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.HasIndex("EstudianteId");

                    b.HasIndex("MateriaId");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("JWS.Models.Ciclo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ciclos");
                });

            modelBuilder.Entity("JWS.Models.Estudiante", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CicloId")
                        .HasColumnType("bigint");

                    b.Property<string>("CodigoUnico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("FotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NroDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("SemestrePagado")
                        .HasColumnType("bit");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("JWS.Models.Materia", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("CicloId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ProfesorId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("JWS.Models.Matricula", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AnioLectivo")
                        .HasColumnType("int");

                    b.Property<long>("CicloId")
                        .HasColumnType("bigint");

                    b.Property<long>("EstudianteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CicloId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("JWS.Models.PersonaResponsable", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("EstudianteId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.ToTable("PersonasResponsables");
                });

            modelBuilder.Entity("JWS.Models.Profesor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NroDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("JWS.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("JWS.Models.Asistencia", b =>
                {
                    b.HasOne("JWS.Models.Ciclo", "Ciclo")
                        .WithMany()
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("JWS.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("JWS.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("JWS.Models.Calificacion", b =>
                {
                    b.HasOne("JWS.Models.Ciclo", "Ciclo")
                        .WithMany()
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("JWS.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("JWS.Models.Materia", "Materia")
                        .WithMany()
                        .HasForeignKey("MateriaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Estudiante");

                    b.Navigation("Materia");
                });

            modelBuilder.Entity("JWS.Models.Estudiante", b =>
                {
                    b.HasOne("JWS.Models.Ciclo", "Ciclo")
                        .WithMany("Estudiantes")
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ciclo");
                });

            modelBuilder.Entity("JWS.Models.Materia", b =>
                {
                    b.HasOne("JWS.Models.Ciclo", "Ciclo")
                        .WithMany("Materias")
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("JWS.Models.Profesor", "Profesor")
                        .WithMany("Materias")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("JWS.Models.Matricula", b =>
                {
                    b.HasOne("JWS.Models.Ciclo", "Ciclo")
                        .WithMany()
                        .HasForeignKey("CicloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JWS.Models.Estudiante", "Estudiante")
                        .WithMany()
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("JWS.Models.PersonaResponsable", b =>
                {
                    b.HasOne("JWS.Models.Estudiante", "Estudiante")
                        .WithMany("PersonasResponsables")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("JWS.Models.Ciclo", b =>
                {
                    b.Navigation("Estudiantes");

                    b.Navigation("Materias");
                });

            modelBuilder.Entity("JWS.Models.Estudiante", b =>
                {
                    b.Navigation("PersonasResponsables");
                });

            modelBuilder.Entity("JWS.Models.Profesor", b =>
                {
                    b.Navigation("Materias");
                });
#pragma warning restore 612, 618
        }
    }
}