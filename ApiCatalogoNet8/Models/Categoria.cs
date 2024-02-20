﻿using System.Collections.ObjectModel;

namespace ApiCatalogoNet8.Models;

public class Categoria
{
    public Categoria() 
    {
        Produtos = new Collection<Produto>();
    }  

    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}