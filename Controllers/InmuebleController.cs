using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
    public class InmuebleController : Controller
    {
        private readonly RepositorioInmueble repositorio;
        private readonly IRepositorioPropietario repoPropietario;
        private readonly RepositorioTipoInmueble repoTipoInmueble;
        private readonly RepositorioImagen repoImagen;

        public InmuebleController(RepositorioInmueble repositorio, RepositorioImagen repoImagen, IRepositorioPropietario repoPropietario, RepositorioTipoInmueble repoTipoInmueble)
        {
            this.repositorio = repositorio;
            this.repoPropietario = repoPropietario;
            this.repoTipoInmueble = repoTipoInmueble;
            this.repoImagen = repoImagen;
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
                    if (!ValidarImagen(inmueble.ImagenPortada))
                     {
                     ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
                     return View(inmueble);
                     }
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

        public IActionResult Detalles(int id)
        {
            var entidad = repositorio.ObtenerPorId(id);
            if (entidad == null) return NotFound();
            ViewBag.Propietario = repoPropietario.ObtenerPorId(entidad.PropietarioId);
            ViewBag.TipoInmueble = repoTipoInmueble.ObtenerPorId(entidad.TipoInmuebleId);
            var imagenesAdicionales = repoImagen.ObtenerPorInmueble(id);
            ViewBag.ImagenesJson = System.Text.Json.JsonSerializer.Serialize(imagenesAdicionales);
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
        public IActionResult Buscar()
        {
            ViewBag.TipoInmuebles = repoTipoInmueble.ObtenerTodos();
            return View();
        }

        [HttpGet]
        public IActionResult BuscarDisponibles(DateTime fechaEntrada, DateTime fechaSalida, int capacidad = 1, int idTipoInmueble = 0, int pagNro = 1, int tamPagina = 20)
        {
            if (fechaEntrada.Date < DateTime.Today)
            {
                return BadRequest(new { error = "La fecha de entrada no puede ser anterior a hoy." });
            }
            if (fechaSalida.Date <= fechaEntrada.Date)
            {
                return BadRequest(new { error = "La fecha de salida debe ser posterior a la de entrada." });
            }
            if (capacidad < 1)
            {
                return BadRequest(new { error = "La capacidad debe ser al menos 1." });
            }

            var lista = repositorio.BuscarDisponibles(fechaEntrada, fechaSalida, capacidad, idTipoInmueble, pagNro, tamPagina);

            var resultado = lista.Select(i => new
            {
                id = i.Id,
                direccion = i.Direccion,
                capacidad = i.Capacidad,
                montoDia = i.montoDia,
                imagenPortada = i.StringPortada,
                tipo = i.NombreTipo?.Nombre
            });

            return Json(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPortada(int id, IFormFile ImagenPortada, [FromServices] IWebHostEnvironment environment)
        {
            var inmueble = repositorio.ObtenerPorId(id);
            if (inmueble == null || ImagenPortada == null || ImagenPortada.Length == 0)
                return RedirectToAction("Detalles", new { id });
             
             if (!ValidarImagen(ImagenPortada))
             {
              var error = ModelState["ImagenPortada"]?.Errors.FirstOrDefault()?.ErrorMessage;
              TempData["Error"] = error ?? "La imagen no es válida.";
              return RedirectToAction("Detalles", new { id });
              }
            //  Borro la foto anterior 
            if (!string.IsNullOrEmpty(inmueble.StringPortada))
            {
                string fotoAntigua = Path.Combine(environment.WebRootPath, inmueble.StringPortada.TrimStart('/'));
                if (System.IO.File.Exists(fotoAntigua)) System.IO.File.Delete(fotoAntigua);
            }

            //  Guardo  la nuevaafoto
            string nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(ImagenPortada.FileName)}";
            string rutaFisica = Path.Combine(environment.WebRootPath, "Uploads", "Portadas", nombreArchivo);

            using (var stream = new FileStream(rutaFisica, FileMode.Create))
            {
                await ImagenPortada.CopyToAsync(stream);
            }


            inmueble.StringPortada = $"/Uploads/Portadas/{nombreArchivo}";
            repositorio.Modificar(inmueble);

            TempData["Mensaje"] = "Portada actualizada correctamente.";
            return RedirectToAction("Detalles", new { id });
        }



       private bool ValidarImagen(IFormFile archivo)
{
    long maxSizeBytes = 10 * 1024 * 1024;
    if (archivo.Length > maxSizeBytes)
    {
        ModelState.AddModelError("ImagenPortada", "La imagen no debe superar los 2 MB de peso.");
        return false;
    }

    var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
    var extension = Path.GetExtension(archivo.FileName).ToLowerInvariant();

    if (string.IsNullOrEmpty(extension) || !extensionesPermitidas.Contains(extension))
    {
        ModelState.AddModelError("ImagenPortada", "Solo se permiten imágenes con extensión .jpg, .jpeg, .png o .webp.");
        return false;
    }

    var mimeTypesPermitidos = new[] { "image/jpeg", "image/png", "image/webp" };
    if (!mimeTypesPermitidos.Contains(archivo.ContentType.ToLower()))
    {
        ModelState.AddModelError("ImagenPortada", "El archivo subido no es una imagen válida.");
        return false;
    }

    return true;
}

    }

}