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
        private readonly IRepository<Categoria> _repository;
        public CategoriasController(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            var categorias = await _repository.GetAll();
            if (categorias is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return Ok(categorias);
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var categoria = await _repository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Categoria novaCategoria)
        {
            if (novaCategoria is null)
            {
                return NotFound("Categoria não pode ser nula");
            }
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
            var categoria = await _repository.Get(c => c.CategoriaId == id);
            if(categoria is null)
            {
                return NotFound($"Categoria com o id {id} não encontrada...");
            }
            _repository.Delete(categoria);
            return Ok(categoria);
        }
    }
}
