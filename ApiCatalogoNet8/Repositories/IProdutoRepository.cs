using ApiCatalogoNet8.Models;

namespace ApiCatalogoNet8.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto>> GetProdutosPorCategoria(int id);
}
