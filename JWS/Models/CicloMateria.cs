namespace JWS.Models
{
    public class CicloMateria
    {
        public long CicloId { get; set; }
        public Ciclo Ciclo { get; set; }

        public long MateriaId { get; set; }
        public Materia Materia { get; set; }
    }
}
