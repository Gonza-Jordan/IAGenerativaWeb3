using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAGenerativa.Data.EF
{
    public partial class PalabraClave
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public virtual ICollection<PalabraClaveAmbito> PalabraClaveAmbitos { get; set; }
    }
}