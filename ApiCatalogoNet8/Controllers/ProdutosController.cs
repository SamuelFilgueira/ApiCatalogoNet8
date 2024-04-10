using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Models;
using ApiCatalogoNet8.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _repository.GetProdutos();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados!");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _repository.GetProduto(id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(Produto novoProduto)
        {

            if (novoProduto is null)
            {
                return BadRequest();
            }

            await _repository.Create(novoProduto);
            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            bool atualiza = await _repository.Update(produto);      
            if(atualiza == true)
            {
                return Ok(produto);
            }else
            {
                return StatusCode(500, $"Falha ao atualizar o produto de id {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = await _repository.Delete(id);

            if(produto == true)
            {
                return Ok($"O produto de id {id} foi excluído");
            }else
            {
                return StatusCode(500, $"Falha ao excluir o produto de id {id}");
            }
        }
    }
}
