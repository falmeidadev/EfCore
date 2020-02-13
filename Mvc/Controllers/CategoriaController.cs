using System.Linq;
using System.Threading.Tasks;
using Dados;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers
{

    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public CategoriaController(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            var categorias = _contexto.Categorias.ToList();        

            return View(categorias);
        }

        [HttpGet]   
        public IActionResult Salvar()
        {
            return View();
        }

        public IActionResult Editar(int id) 
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            return View("Salvar", categoria);
        }   

        public async Task<IActionResult> Deletar(int id) 
        {
            var categoria = _contexto.Categorias.First(c => c.Id == id);
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }           

        [HttpPost]
        public async Task<IActionResult> Salvar(Categoria categoria)
        {
            if(categoria.Id == 0)
                _contexto.Categorias.Add(categoria);
            else{
                var categoriaAlterar = _contexto.Categorias.First(c => c.Id == categoria.Id);
                categoriaAlterar.Nome = categoria.Nome;
            }
            await _contexto.SaveChangesAsync();
            return RedirectToAction("Index");
        }        
    }
}