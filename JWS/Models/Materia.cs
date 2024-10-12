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

        public long CicloId { get; set; }
        [JsonIgnore]
        public Ciclo Ciclo { get; set; }

        
    }
}
