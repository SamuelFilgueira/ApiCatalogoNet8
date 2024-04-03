using ApiCatalogoNet8.Models;

namespace ApiCatalogoNet8.Repositories;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategorias();
    Task<Categoria> GetCategoria(int id);
    Task<Categoria> Create(Categoria categoria);
    Task<Categoria> Update(Categoria categoria);
    Task<Categoria> Delete(int id);
}
