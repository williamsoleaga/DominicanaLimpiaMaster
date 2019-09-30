using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominicanaLimpia.ModelViews
{
    public class FormularioModelView 
    {
        public string DesdeFecha { get; set; }
        public string HastaFecha { get; set; }
        public int[] Valores { get; set; }
        public List<Preguntas> Preguntas { get; set; }
        public int[] Escuelas13 { get; set; }

    }
}