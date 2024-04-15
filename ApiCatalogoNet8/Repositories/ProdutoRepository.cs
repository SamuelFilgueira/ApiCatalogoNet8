using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Repositories
{
    public class ProdutoRepository : Repository<Produto> ,IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context):base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var allProdutos = await GetAll();
            var produtosPorCategoria = allProdutos.Where(c => c.CategoriaId == id);
            return produtosPorCategoria;

        }
    }
}
