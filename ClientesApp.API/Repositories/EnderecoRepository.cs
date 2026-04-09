using ClientesApp.API.Contexts;
using ClientesApp.API.Entities;

namespace ClientesApp.API.Repositories
{
    public class EnderecoRepository (DataContext dataContext) : BaseRepository<Endereco>(dataContext)
    {

    }
}
