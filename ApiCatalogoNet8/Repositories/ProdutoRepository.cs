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

        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();
            return produtos;
        }

        public async Task<Produto> GetProduto(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
            if(produto == null)
            {
                throw new InvalidOperationException("O produto é nulo");
            }
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
        public async Task<bool> Update(Produto produto)
        {
            if(produto == null)
                throw new InvalidOperationException("O produto é nulo");
            if (await _context.Produtos.AnyAsync(p => p.ProdutoId == produto.ProdutoId))
            {
                return true;
            }   
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if(produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
