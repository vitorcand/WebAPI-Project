using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAPIProject.Models.Contexto;
using WebAPIProject.Models.Entidades;

namespace WebAPIProject.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly Contexto _contexto;
        public UsuariosController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            var lista = _contexto.Usuario.ToList();
            CarregaTipoUser();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var usuario = new Usuario();
            CarregaTipoUser();
            return View(usuario);
        }
        [HttpPost]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuario.Add(usuario);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            CarregaTipoUser();
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);
            CarregaTipoUser();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Edit(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _contexto.Usuario.Update(usuario);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                CarregaTipoUser();
                return View(usuario);
            }
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);
            CarregaTipoUser();
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delete(Usuario _usuario)
        {
            var usuario = _contexto.Usuario.Find(_usuario.Id);
            if (usuario != null)
            {
                _contexto.Usuario.Remove(usuario);
                _contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(usuario);
        }
        [HttpGet]
        public IActionResult Details(int Id)
        {
            var usuario = _contexto.Usuario.Find(Id);
            CarregaTipoUser();
            return View(usuario);
        }

        public void CarregaTipoUser()
        {
            var ItensTipoUser = new List<SelectListItem>
            {
                new SelectListItem{ Value = "1", Text = "Administrador"},
                new SelectListItem{ Value = "2", Text = "Técnico"},
                new SelectListItem{ Value = "3", Text = "Usuário Normal"}


            };
            ViewBag.TipoUser = ItensTipoUser;
        }

    }
}