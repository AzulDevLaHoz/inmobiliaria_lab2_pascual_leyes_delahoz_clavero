using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Controllers
{
   public class UsuarioController : Controller
{
    private readonly RepositorioUsuario repoUsuario;
    private readonly IRepositorioRol repoRol;

    public UsuarioController(RepositorioUsuario repoUsuario, IRepositorioRol repoRol)
    {
        this.repoUsuario = repoUsuario;
        this.repoRol = repoRol;
    }

  //GET DFE ALTA
    public IActionResult Alta()
    {
        ViewBag.Roles = repoRol.ObtenerTodos();
        return View();
    }

  //POST DE ALATA
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Alta(Usuario u)
    {
        u.Estado = true;

        if (ModelState.IsValid)
        {
            repoUsuario.Alta(u);
            TempData["Mensaje"] = "Usuario creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Roles = repoRol.ObtenerTodos();
        return View(u);
    }

    [AllowAnonymous] 
public IActionResult Login()
{
    return View();
}

[HttpPost]
[AllowAnonymous]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Login(Login model)
{
    if (ModelState.IsValid)
    {
        var usuario = repoUsuario.ObtenerPorEmail(model.Email);

        if (usuario != null && usuario.Clave == model.Clave)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.rol?.Nombre ?? "Empleado"),
                new Claim("Avatar", usuario.Avatar ?? "/img/avatar-default.png")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            TempData["Mensaje"] = $"¡Bienvenido {usuario.Nombre}!";
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError("", "El email o la clave ingresada son incorrectos.");
    }

    return View(model);
}

public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction(nameof(Login));
}
}
}