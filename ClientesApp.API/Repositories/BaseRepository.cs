using ClientesApp.API.Contexts;

namespace ClientesApp.API.Repositories
{
    /// <summary>
    /// Classe abstrata para definir o repositório genérico do sistema.
    /// </summary>
    public class BaseRepository<TEntity>(DataContext dataContext) where TEntity : class
    {
        public virtual void Add(TEntity entity)
        {
            dataContext.Add(entity);
            dataContext.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            dataContext.Update(entity);
            dataContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            dataContext.Remove(entity);
            dataContext.SaveChanges();
        }

        public virtual List<TEntity> GetAll()
        {
            return dataContext.Set<TEntity>().ToList();
        }

        public virtual TEntity? GetById(Guid id)
        {
            return dataContext.Set<TEntity>().Find(id);
        }
    }
}
