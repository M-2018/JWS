using Microsoft.EntityFrameworkCore;
using JWS.Models;

namespace JWS.Data
{
    public class APIAppDbContext : DbContext
    {
        public APIAppDbContext(DbContextOptions<APIAppDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Ciclo> Ciclos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<CicloMateria> CicloMaterias { get; set; } // Nuevo DbSet para la entidad intermedia
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<PersonaResponsable> PersonasResponsables { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la entidad intermedia CicloMateria (muchos a muchos)
            modelBuilder.Entity<CicloMateria>()
                .HasKey(cm => new { cm.CicloId, cm.MateriaId }); // Llave compuesta

            modelBuilder.Entity<CicloMateria>()
                .HasOne(cm => cm.Ciclo)
                .WithMany(c => c.CicloMaterias)
                .HasForeignKey(cm => cm.CicloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CicloMateria>()
                .HasOne(cm => cm.Materia)
                .WithMany(m => m.CicloMaterias)
                .HasForeignKey(cm => cm.MateriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de relaciones existentes
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Ciclo)
                .WithMany(c => c.Estudiantes)
                .HasForeignKey(e => e.CicloId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Estudiante>()
                .HasMany(e => e.PersonasResponsables)
                .WithOne(pr => pr.Estudiante)
                .HasForeignKey(pr => pr.EstudianteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Materia>()
                .HasOne(m => m.Profesor)
                .WithMany(p => p.Materias)
                .HasForeignKey(m => m.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Estudiante)
                .WithMany()
                .HasForeignKey(a => a.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Materia)
                .WithMany()
                .HasForeignKey(a => a.MateriaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Ciclo)
                .WithMany()
                .HasForeignKey(a => a.CicloId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Estudiante)
                .WithMany()
                .HasForeignKey(c => c.EstudianteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Ciclo)
                .WithMany()
                .HasForeignKey(c => c.CicloId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Calificacion>()
                .HasOne(c => c.Materia)
                .WithMany()
                .HasForeignKey(c => c.MateriaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
