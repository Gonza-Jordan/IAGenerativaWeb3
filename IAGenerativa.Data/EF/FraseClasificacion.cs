using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace IAGenerativa.Data.EF
{
    public partial class FraseClasificacion
    {
        public int Id { get; set; }
        public string Texto { get; set; } = null!;
        public int ClasificacionId { get; set; }
        public virtual Clasificacion Clasificacion { get; set; } = null!;
    }
}
