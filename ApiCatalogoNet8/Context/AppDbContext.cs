﻿using ApiCatalogoNet8.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoNet8.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }

    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Categoria>? Categorias { get; set; }
}
