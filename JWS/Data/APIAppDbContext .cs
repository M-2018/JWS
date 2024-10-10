using JWS.Models;
using Microsoft.EntityFrameworkCore;

namespace JWS.Data
{
    public class APIAppDbContext : DbContext
    {
        public APIAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PersonaResponsable> PersonasResponsables { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }  // Agregado el DbSet de Asistencia

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la entidad Matricula
            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Estudiante)
                .WithMany(e => e.Matriculas)
                .HasForeignKey(m => m.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            modelBuilder.Entity<Matricula>()
                .HasOne(m => m.Materia)
                .WithMany(ma => ma.Matriculas)
                .HasForeignKey(m => m.MateriaId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            // Configuración para la entidad PersonaResponsable
            modelBuilder.Entity<PersonaResponsable>()
                .HasOne(pr => pr.Estudiante)
                .WithMany(e => e.PersonasResponsables)
                .HasForeignKey(pr => pr.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            // Configuración para la entidad Profesor y Materia
            modelBuilder.Entity<Materia>()
                .HasOne(ma => ma.Profesor)
                .WithMany(p => p.Materias)
                .HasForeignKey(ma => ma.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            // Configuración para la entidad Ciclo y Estudiantes
            modelBuilder.Entity<Ciclo>()
                .HasMany(c => c.Estudiantes)
                .WithOne(e => e.Ciclo)
                .HasForeignKey(e => e.CicloId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            // Configuración para la entidad Asistencia
            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Estudiante)
                .WithMany()  // Relación con estudiante
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Materia)
                .WithMany(m => m.Asistencias)  // Relación con materia
                .HasForeignKey(a => a.MateriaId)
                .OnDelete(DeleteBehavior.Restrict); // Evitar la eliminación en cascada
        }
    }
}
