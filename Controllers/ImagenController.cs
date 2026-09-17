using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{


    public class ImagenController : Controller
    {

        private readonly RepositorioImagen repoImagen;
        private readonly RepositorioInmueble repoInmueble;
        private readonly IRepositorioPropietario repoPropietario;

        public ImagenController(RepositorioImagen repoImagen, IRepositorioPropietario repoPropietario, RepositorioInmueble repoInmueble)
        {
            this.repoInmueble = repoInmueble;
            this.repoPropietario = repoPropietario;
            this.repoImagen = repoImagen;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarImagen(int IdInmueble, List<IFormFile> Archivos, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (Archivos != null && Archivos.Count > 0)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads", "Galeria");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    int subidas = 0;
                    foreach (var archivo in Archivos)
                    {
                        if (archivo == null || archivo.Length == 0) continue;

                        string extension = Path.GetExtension(archivo.FileName);
                        string nombreArchivo = $"{Guid.NewGuid()}{extension}";
                        string rutaArchivo = Path.Combine(path, nombreArchivo);

                        using (var stream = new FileStream(rutaArchivo, FileMode.Create))
                        {
                            await archivo.CopyToAsync(stream);
                        }

                        var imagen = new Imagen
                        {
                            ImagenString = $"/Uploads/Galeria/{nombreArchivo}",
                            IdInmueble = IdInmueble
                        };
                        repoImagen.Alta(imagen);
                        subidas++;
                    }

                    TempData["Mensaje"] = subidas == 1
                        ? "Imagen agregada a la galería correctamente."
                        : $"{subidas} imágenes agregadas a la galería correctamente.";
                }
                else
                {
                    TempData["Error"] = "Debe seleccionar al menos un archivo de imagen válido.";
                }

                return RedirectToAction("Detalles", "Inmueble", new { id = IdInmueble });
            }
            catch (Exception e)
            {
                TempData["Error"] = "Ocurrió un error al guardar las imágenes: " + e.Message;
                return RedirectToAction("Detalles", "Inmueble", new { id = IdInmueble });
            }
        }

        [HttpPost]
        public IActionResult EliminarImagen(int id, int idInmueble, [FromServices] IWebHostEnvironment environment)
        {
            var img = repoImagen.ObtenerPorId(id);

            if (img != null)
            {

                string rutaFisica = Path.Combine(environment.WebRootPath, img.ImagenString.TrimStart('/'));
                if (System.IO.File.Exists(rutaFisica))
                {
                    System.IO.File.Delete(rutaFisica);
                }


                repoImagen.Baja(id);
                TempData["Mensaje"] = "La imagen fue eliminada.";
            }
            else
            {
                TempData["Error"] = "La imagen no existe.";
            }

            return RedirectToAction("Detalles", "Inmueble", new { id = idInmueble });
        }

    }
}