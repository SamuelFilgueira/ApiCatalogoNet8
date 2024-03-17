using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Filters;
using ApiCatalogoNet8.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        {
            return await _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToListAsync();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var categorias = await _context.Categorias.AsNoTracking().ToListAsync();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return Ok(categorias);
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Produto> Get(int id)
        {
            var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(x => x.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Produto> Post(Categoria novaCategoria)
        {

            if (novaCategoria is null)
            {
                return NotFound("Informe uma categoria!");
            }

            _context.Categorias.Add(novaCategoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoria", new { id = novaCategoria.CategoriaId }, novaCategoria);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            return Ok(categoria);
        }
    }
}
