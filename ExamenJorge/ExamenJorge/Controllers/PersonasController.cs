using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamenJorge.Models;
using Newtonsoft.Json;

namespace ExamenJorge.Controllers
{
    public class PersonasController : Controller
    {
        private Entities db = new Entities();

        public ActionResult ListaPersonas() {
            return View(db.Tbl_Personas.ToList());
        }

        [HttpGet]
        public JsonResult Buscar(string valor)
        {

            var Resultdo = db.Tbl_Personas.Where(a => a.Nombre.Contains(valor) ||
                                                      a.ApellidoPaterno.Contains(valor) ||
                                                      a.ApellidoMaterno.Contains(valor) ||
                                                      a.CURP.Contains(valor) &&
                                                      a.Estatus == 1).ToList();

            if (valor == "") {
                Resultdo = db.Tbl_Personas.ToList();
            }

            return Json(Resultdo, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult BuscarPersona(int Id)
        {
            var Resultdo = db.Tbl_Personas.Where(a => a.Id == Id && a.Estatus == 1).ToList();
            return Json(Resultdo, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult Guardar(Tbl_Personas tbl_persona) 
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Personas.Add(tbl_persona);
                db.SaveChanges();

                var Resultado = from p in db.Tbl_Personas
                                select new
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    ApellidoPaterno = p.ApellidoPaterno,
                                    ApellidoMaterno = p.ApellidoMaterno,
                                    CURP = p.CURP,
                                    Estatus = p.Estatus
                                };

                return Json(Resultado.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ObjPersona Persona = new ObjPersona();
                Persona.Resultado = "Error";
                Persona.Mensaje = "Favor de validar los datos.";
                return Json(JsonConvert.SerializeObject(Persona), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Editar(Tbl_Personas tbl_persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_persona).State = EntityState.Modified;
                db.SaveChanges();

                var Resultado = from p in db.Tbl_Personas
                                where p.Estatus == 1
                                select new
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    ApellidoPaterno = p.ApellidoPaterno,
                                    ApellidoMaterno = p.ApellidoMaterno,
                                    CURP = p.CURP,
                                    Estatus = p.Estatus
                                };

                return Json(Resultado.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ObjPersona Persona = new ObjPersona();
                Persona.Resultado = "Error";
                Persona.Mensaje = "Favor de validar los datos.";
                return Json(JsonConvert.SerializeObject(Persona), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult Eliminar(int id)
        {
            if (ModelState.IsValid)
            {
                Tbl_Personas tbl_personas = db.Tbl_Personas.Find(id);
                db.Tbl_Personas.Remove(tbl_personas);
                db.SaveChanges();

                var Resultado = from p in db.Tbl_Personas
                                select new
                                {
                                    Id = p.Id,
                                    Nombre = p.Nombre,
                                    ApellidoPaterno = p.ApellidoPaterno,
                                    ApellidoMaterno = p.ApellidoMaterno,
                                    CURP = p.CURP,
                                    Estatus = p.Estatus
                                };

                return Json(Resultado.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                ObjPersona Persona = new ObjPersona();
                Persona.Resultado = "Error";
                Persona.Mensaje = "Error al generar la solicitud.";
                return Json(JsonConvert.SerializeObject(Persona), JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}