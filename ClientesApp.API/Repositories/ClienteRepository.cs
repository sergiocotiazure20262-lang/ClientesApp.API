using ClientesApp.API.Contexts;
using ClientesApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientesApp.API.Repositories
{
    /// <summary>
    /// Classe de repositório de dados para cliente.
    /// </summary>
    public class ClienteRepository : BaseRepository<Cliente>
    {
        /// <summary>
        /// Método para consultar 1 cliente baseado no id
        /// E retornar os endereços associados ao cliente
        /// </summary>
        public override Cliente? GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                        .Set<Cliente>() //Consulta de Cliente
                        .Include(c => c.Enderecos) //Incluir os endereços associados
                        .FirstOrDefault(c => c.Id == id); //Filtrando pelo id
            }
        }

        /// <summary>
        /// Método para consultar clientes que contenham um determinado nome
        /// E retornar os clientes cada um com os seus endereços associados.
        /// </summary>
        public List<Cliente> GetAllByNome(string nome)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                        .Set<Cliente>() //Consulta de Cliente
                        .Include(c => c.Enderecos) //Incluir os endereços associados
                        .Where(c => c.Nome.Contains(nome)) //Filtrar pelo nome
                        .OrderBy(c => c.Nome) //Ordenar por nome
                        .ToList(); //Retornar como lista
            }
        }
    }
}
