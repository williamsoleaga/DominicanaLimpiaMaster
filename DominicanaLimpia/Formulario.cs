//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DominicanaLimpia
{
    using System;
    using System.Collections.Generic;
    
    public partial class Formulario
    {

        public int FormularioId { get; set; }
        public Nullable<int> PreguntaId { get; set; }
        public Nullable<int> Valor { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public Nullable<int> Idusuario { get; set; }
        public string Estatus { get; set; }
        public List<Preguntas> Preguntas { get; set; }
        public string Comentario { get; set; }
        public int[] Valores { get; set; }

        public string DesdeFecha { get; set; }
        public string HastaFecha { get; set; }

    }
}