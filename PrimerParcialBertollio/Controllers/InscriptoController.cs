using Microsoft.AspNetCore.Mvc;
using PrimerParcialBertollio.Datos;
using PrimerParcialBertollio.Models;

namespace PrimerParcialBertollio.Controllers
{
    public class InscriptoController : Controller
    {
        InscriptoDatos _BD=new InscriptoDatos();
        public IActionResult Index()
        {
            return View(_BD.ListaInscripto());
        }
        public IActionResult Create()
        {
            ViewBag.Disciplina = _BD.ListarDisciplina();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Inscripto inscripto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                ViewBag.Error = _BD.CrearInscripto(inscripto);
                if (ViewBag.Error != "")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        public IActionResult CantidadPorDisciplina()
        {
            var resultado = _BD.ObtenerCantidadPorDisciplina();
            return View(resultado);
        }
    }
}
