using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly RepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repoPropietario;
        private readonly RepositorioTipoInmueble repoTipoInmueble;

        public InmuebleController(RepositorioInmueble repositorio, IRepositorioPropietario repoPropietario, RepositorioTipoInmueble repoTipoInmueble)
        {
            this.repositorio = repositorio;
            this.repoPropietario = repoPropietario;
            this.repoTipoInmueble = repoTipoInmueble;
        }

        public IActionResult Index()
        {
            var lista = repositorio.ObtenerLista();
            return View(lista);
        }

        public IActionResult Alta()
        {
            ViewBag.Propietarios = repoPropietario.ObtenerLista();
            ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
            return View();
        }

     [HttpPost]
     
public async Task<IActionResult> Alta(Inmueble inmueble, [FromServices] IWebHostEnvironment environment)
{
    try
    {
        if (inmueble.ImagenPortada != null && inmueble.ImagenPortada.Length > 0)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads", "Portadas");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extension = Path.GetExtension(inmueble.ImagenPortada.FileName);
            string nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string rutaArchivo = Path.Combine(path, nombreArchivo);

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await inmueble.ImagenPortada.CopyToAsync(stream);
            }

            inmueble.StringPortada = $"/Uploads/Portadas/{nombreArchivo}";
        }

        if (ModelState.IsValid)
        {
            repositorio.Alta(inmueble);
            TempData["Id"] = inmueble.Id;
            return RedirectToAction(nameof(Index));
        }

        ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
        return View(inmueble);
    }
    catch (Exception e)
    {
        ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
        ViewBag.Error = e.Message;
        ViewBag.StackTrate = e.StackTrace;
        return View(inmueble);
    }
}
        public IActionResult Modificar(int id)
        {
            ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
            var entidad = repositorio.ObtenerPorId(id);
            ViewBag.Propietario = repoPropietario.ObtenerPorId(entidad.PropietarioId);

            return View(entidad);
        }

        [HttpPost]
        public IActionResult Modificar(int id, Inmueble entidad)
        {
            try
            {
                entidad.Id = id;
                repositorio.Modificar(entidad);
                TempData["Mensaje"] = "Datos guardados correctamente";             
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Propietarios = repoPropietario.ObtenerLista(1, 50);
                ViewBag.Error = ex.Message;
                ViewBag.StackTrate = ex.StackTrace;
                return View(entidad);
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int id)
        {
            repositorio.Baja(id);
            TempData["Mensaje"] = "Se dio de baja correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Buscar(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return Json(new List<object>());
            }

            var propietarios = repoPropietario.BuscarPorTexto(q)
                .Select(p => new
                {
                    id = p.IdPropietario,
                    texto = $"{p.Nombre} {p.Apellido} DNI: {p.Dni}"
                });

            return Json(propietarios);
        }

    }

}