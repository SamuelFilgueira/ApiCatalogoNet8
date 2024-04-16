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
        private readonly IProdutoRepository _produtoRepository;
        private readonly IRepository<Produto> _repository;
        public ProdutosController(IRepository<Produto> repository, IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
            _repository = repository;
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutosPorCategoria(int id)
        {
            var produtos = await _produtoRepository.GetProdutosPorCategoria(id);
            if(produtos is null)
            {
                return NotFound();
            }
            return Ok(produtos);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _repository.GetAll();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados!");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            var produto = await _repository.Get(p => p.ProdutoId == id);

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

            var produtoAtualizado = await _repository.Update(produto);
            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> Delete(int id)
        {
            var produto = await _repository.Get(p => p.ProdutoId == id);
            if(produto is null)
            {
                return BadRequest();
            }
            await _repository.Delete(produto);
            return Ok(produto);
        }
    }
}
