
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
    using DominicanaLimpia.ModelViews;
    using System;
    using System.Collections.Generic;
    
public partial class Usuarios  : UsuariosModelView
    {

    public int UsuarioId { get; set; }

    public string Usuario { get; set; }

    public string Clave { get; set; }

    public string Nombre_Completo { get; set; }

    public string Correo { get; set; }

    public Nullable<int> RolId { get; set; }

    public string Estatus { get; set; }

    public Nullable<int> MunicipioId { get; set; }

    public string MunicipiosId { get; set; }

    public Nullable<int> ResponsableId { get; set; }

}

}
