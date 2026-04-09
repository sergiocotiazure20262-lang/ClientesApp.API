using ClientesApp.API.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ClientesApp.API.Contexts
{
    /// <summary>
    /// Classe de contexto de dados para a aplicação ClientesApp
    /// E definição da conexão com o banco de dados.
    /// </summary>
    public class DataContext (IConfiguration configuration) : DbContext
    {
        /// <summary>
        /// Método para mapear a conexão com o banco de dados SQL Server.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ClientesApp"));
        }

        /// <summary>
        /// Método para adicionar as classes de mapeamento
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());
        }
    }
}
