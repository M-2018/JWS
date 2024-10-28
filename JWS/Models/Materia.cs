using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JWS.Models
{
    public class Materia
    {
        [Key]
        public long Id { get; set; }
        public string Nombre { get; set; }

        public long ProfesorId { get; set; }
        [JsonIgnore]
        public Profesor Profesor { get; set; }

        [JsonIgnore]
        public ICollection<CicloMateria> CicloMaterias { get; set; }
    }
}
