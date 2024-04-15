using ApiCatalogoNet8.Context;
using ApiCatalogoNet8.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Repositories
{
    public class CategoriaRepository : Repository<Categoria> ,ICategoriaRepository
    {

        public CategoriaRepository(AppDbContext context) : base(context) { }


    }
}
