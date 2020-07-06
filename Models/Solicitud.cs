using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace checking_v2.Models
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Contenido { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaHora { get; set; }

        public ICollection<Respuesta> Respuestas { get; set; }
    }
}
