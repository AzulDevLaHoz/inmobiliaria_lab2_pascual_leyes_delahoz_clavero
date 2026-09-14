using inmobiliaria_lab2_pascual_leyes_delahoz_clavero.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

        //GET DE ALTA
        public IActionResult Alta()
        {
            ViewBag.Roles = repoRol.ObtenerTodos();
            return View();
        }

        //POST DE ALTA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Alta(Usuario u)
        {
            u.Estado = true;
                       //VALIDE ACA POR QUE SE SACO ELRQUIRED DEL MODEL 
            if (string.IsNullOrWhiteSpace(u.Clave))
           {
             ModelState.AddModelError("Clave", "La clave es obligatoria al crear un usuario.");
           }

            if (ModelState.IsValid)
            {

                var passwordHasher = new PasswordHasher<Usuario>();

                u.Clave = passwordHasher.HashPassword(u, u.Clave);
                repoUsuario.Alta(u);
                TempData["Mensaje"] = "Usuario creado correctamente.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Roles = repoRol.ObtenerTodos();
            return View(u);
        }

       // GET MODIFICAR
[HttpGet]
[Authorize]
public IActionResult Modificar(int id)
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    int.TryParse(userIdClaim, out int idUsuarioLogueado);
    bool esAdmin = User.IsInRole("Administrador");

    // SOLO PUEDE EDITR SU PERF SI NO ES ADM
    if (!esAdmin && id != idUsuarioLogueado)
    {
        TempData["Error"] = "No tienes permisos para modificar otros usuarios.";
        return RedirectToAction("Index", "Home");
    }

    var usuario = repoUsuario.ObtenerPorId(id);
    if (usuario == null) return NotFound();

    ViewBag.Roles = repoRol.ObtenerTodos();
    return View(usuario);
}

// POST MODIFICAR
[HttpPost]
[ValidateAntiForgeryToken]
[Authorize]
public IActionResult Modificar(int id, Usuario u)
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    int.TryParse(userIdClaim, out int idUsuarioLogueado);
    bool esAdmin = User.IsInRole("Administrador");

    if (!esAdmin && id != idUsuarioLogueado)
    {
        TempData["Error"] = "No tienes permisos para modificar otros usuarios.";
        return RedirectToAction("Index", "Home");
    }

    var p = repoUsuario.ObtenerPorId(id);
    if (p == null) return NotFound();

    if (ModelState.IsValid)
    {
        p.Nombre = u.Nombre;
        p.Apellido = u.Apellido;
        p.Email = u.Email;
        p.Avatar = u.Avatar;

        // SOLO ADMIN PUEDdE CAMBIAR ROLES
        if (esAdmin)
        {
            p.IdRol = u.IdRol;
        }

        repoUsuario.Modificar(p);
        TempData["Mensaje"] = "Usuario modificado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    ViewBag.Roles = repoRol.ObtenerTodos();
    return View(u);
}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            repoUsuario.Baja(id);
            TempData["Mensaje"] = "Se dio de baja correctamente.";
            return RedirectToAction(nameof(Index));
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

                if (usuario != null)
                {
                    var passwordHasher = new PasswordHasher<Usuario>();

                    var result = passwordHasher.VerifyHashedPassword(usuario, usuario.Clave, model.Clave);

                    if (result == PasswordVerificationResult.Success)
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

        [HttpGet]
        public IActionResult Index(int pagina = 1)
        {
            int tamPagina = 10;

            var usuarios = repoUsuario.ObtenerLista(pagNro: pagina, tamPagina: tamPagina);

            int totalRegistros = repoUsuario.ObtenerCantidadUsuarios();

            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / tamPagina);

            return View(usuarios);
        }
    }
}