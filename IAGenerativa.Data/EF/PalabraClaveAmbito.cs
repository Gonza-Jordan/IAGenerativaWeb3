using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.EF
{
    public partial class PalabraClaveAmbito
    {
        public int Id { get; set; }
        public int PalabraClaveId { get; set; }
        public int AmbitoId { get; set; }

        public virtual PalabraClave PalabraClave { get; set; }
        public virtual Ambito Ambito { get; set; }
    }
}

