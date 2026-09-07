using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{


public  class ImagenController :Controller 
{
    
       private readonly RepositorioImagen repoImagen;
       private readonly RepositorioInmueble repoInmueble;
       private readonly IRepositorioPropietario repoPropietario;

     public ImagenController (RepositorioImagen repoImagen, IRepositorioPropietario repoPropietario, RepositorioInmueble repoInmueble){
         this.repoInmueble=repoInmueble;
         this.repoPropietario=repoPropietario;
         this.repoImagen= repoImagen;
     }  

[HttpPost]
public async Task<IActionResult> AgregarImagen(Imagen imagen, [FromServices] IWebHostEnvironment environment)
{
    try
    {
        if (imagen.Archivo != null && imagen.Archivo.Length > 0)
        {
            string wwwPath = environment.WebRootPath;
            string path = Path.Combine(wwwPath, "Uploads", "Galeria");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string extension = Path.GetExtension(imagen.Archivo.FileName);
            string nombreArchivo = $"{Guid.NewGuid()}{extension}";
            string rutaArchivo = Path.Combine(path, nombreArchivo);

            using (var stream = new FileStream(rutaArchivo, FileMode.Create))
            {
                await imagen.Archivo.CopyToAsync(stream);
            }

            imagen.ImagenString = $"/Uploads/Galeria/{nombreArchivo}";

            repoImagen.Alta(imagen);
            TempData["Mensaje"] = "Imagen agregada a la galería correctamente.";
        }
        else
        {
            TempData["Error"] = "Debe seleccionar un archivo de imagen válido.";
        }

        return RedirectToAction("Detalles", "Inmueble", new { id = imagen.IdInmueble });
    }
    catch (Exception e)
    {
        TempData["Error"] = "Ocurrió un error al guardar la imagen: " + e.Message;
        return RedirectToAction("Detalles", "Inmueble", new { id = imagen.IdInmueble });
    }
}

[HttpPost]
public IActionResult EliminarImagen(int id, int idInmueble, [FromServices] IWebHostEnvironment environment)
{
    var img = repoImagen.ObtenerPorId(id);

    if (img != null)
    {
        // 1. Borramos el archivo físico del servidor
        string rutaFisica = Path.Combine(environment.WebRootPath, img.ImagenString.TrimStart('/'));
        if (System.IO.File.Exists(rutaFisica))
        {
            System.IO.File.Delete(rutaFisica);
        }

        // 2. Eliminamos el registro de la BD
        repoImagen.Baja(id);
        TempData["Mensaje"] = "La imagen fue eliminada.";
    }
    else
    {
        TempData["Error"] = "La imagen no existe.";
    }

    return RedirectToAction("Detalles","Inmueble", new { id = idInmueble });
}

}
}