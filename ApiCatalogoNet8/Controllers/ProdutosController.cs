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
        private readonly IUnitOfWork _uof;

        public ProdutosController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosPorCategoria(int id)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosPorCategoria(id);
            if(produtos is null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _uof.ProdutoRepository.GetAll();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados!");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

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

            await _uof.ProdutoRepository.Create(novoProduto);
            _uof.Commit();
            return new CreatedAtRouteResult("ObterProduto", new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = await _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
            if(produto is null)
            {
                return BadRequest();
            }
            await _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();
            return Ok(produto);
        }
    }
}
