
using ApiCatalogoNet8.Context;

namespace ApiCatalogoNet8.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _context;
        private IProdutoRepository? _produtoRepo;
        private ICategoriaRepository? _categoriaRepo;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
