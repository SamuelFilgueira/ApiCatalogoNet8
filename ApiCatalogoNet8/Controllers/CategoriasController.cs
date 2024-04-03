using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Filters;
using ApiCatalogoNet8.Models;
using ApiCatalogoNet8.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        //[HttpGet("produtos")]
        //public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriasProdutos()
        //{
        //    return await _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToListAsync();
        //}

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var categorias = await _repository.GetCategorias();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return Ok(categorias);
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var categoria = await _repository.GetCategoria(id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Categoria novaCategoria)
        {

            //if (novaCategoria is null)
            //{
            //    return NotFound("Informe uma categoria!");
            //}

            var categoriaCriada = await _repository.Create(novaCategoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Categoria>> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }

            var categoriaAtualizada = await _repository.Update(categoria);

            return Ok(categoriaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = await _repository.Delete(id);
            return Ok(categoria);
        }
    }
}
