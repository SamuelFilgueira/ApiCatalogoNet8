using ApiCatalogoNet8.Models;

namespace ApiCatalogoNet8.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetProdutos(Produto produto);
    Task<Produto> GetProduto(int id);
    Task<Produto> Create(Produto produto);
    Task<bool> Update(Produto produto);
    Task<bool> Delete(int id);

}
