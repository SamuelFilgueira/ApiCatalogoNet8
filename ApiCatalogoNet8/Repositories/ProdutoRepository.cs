using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produto>> GetProdutos(Produto produto)
        {
            var produtos = await _context.Produtos.ToListAsync();
            return produtos;
        }

        public async Task<Produto> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            return produto;
        }

        public async Task<Produto> Create(Produto produto)
        {
            if(produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
            return produto;
        }
        public Task<bool> Update(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
