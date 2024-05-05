using Labs.Feedback.API.Abstraction.Repositorios;
using Labs.Feedback.API.Context;
using Labs.Feedback.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Labs.Feedback.API.Repositorios;

internal class RepositorioMensagem : IRepositorioMensagem
{
    private readonly DbContext _dbContext;
    private readonly DbSet<Mensagem> _dbSet;

    public RepositorioMensagem(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._dbSet = dbContext?.Set<Mensagem>();
    }

    public Boolean AdicionarMensagem(Mensagem mensagem)
    {
        this._dbSet.Add(mensagem);
        this._dbContext.SaveChanges();

        return true;
    }

    public Mensagem PesquisaPorIdent(Guid ident)
    {
        var mensagem = _dbSet.Find(ident);

        return mensagem;
    }

    public IEnumerable<Mensagem> PesquisaPor(Expression<Func<Mensagem, bool>> condicao)
    {
        IQueryable<Mensagem> query = _dbSet;

        return query.Where(condicao);
    }
}
