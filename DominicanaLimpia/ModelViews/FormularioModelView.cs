using DominicanaLimpia.Models;
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
        public string[] DescripcionP { get; set; }
        public List<Preguntas> Preguntas { get; set; }
        public int[] Escuelas13 { get; set; }
        public List<ObjetivoLista1> ObjetivoLista { get; set; }

        public List<Objetivo1> objetivo1 { get; set; }
        public List<FormularioM> formularioM { get; set; }

        public string[] Comentarios { get; set; }
        public int Pregunta16 { get; set; }

        public string Comentarioobj1 { get; set; }
        public string Comentarioobj2 { get; set; }
        public string Comentarioobj3 { get; set; }
        public string Comentarioobj4 { get; set; }


    }
}