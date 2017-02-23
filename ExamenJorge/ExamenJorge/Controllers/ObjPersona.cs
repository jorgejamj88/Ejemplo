using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenJorge.Controllers
{
    public class ObjPersona
    {
        public string Resultado { get; set; }
        public string Mensaje { get; set; }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CURP { get; set; }
        public int Estatus { get; set; }
    }
}