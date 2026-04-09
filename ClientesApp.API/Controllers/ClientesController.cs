using Azure.Core;
using ClientesApp.API.Dtos.Requests;
using ClientesApp.API.Dtos.Responses;
using ClientesApp.API.Entities;
using ClientesApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //Atributos
        private readonly ClienteRepository _clienteRepository;
        private readonly EnderecoRepository _enderecoRepository;

        //Método construtor -> ctor + tab
        public ClientesController()
        {
            _clienteRepository = new ClienteRepository(); //Instanciando
            _enderecoRepository = new EnderecoRepository(); //Instanciando
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteResponseDto), 201)]
        public IActionResult Post([FromBody] ClienteRequestDto request)
        {
            try
            {
                #region Validações

                //Verificar se o cliente possui pelo menos 1 endereço
                if (request.Enderecos == null || !request.Enderecos.Any())
                    throw new ApplicationException("O cliente deve ter pelo menos 1 endereço.");

                #endregion

                #region Cadastro do cliente

                //Capturar os dados do DTO para a entidade
                var cliente = new Cliente()
                {
                    Nome = request.Nome, //capturando o nome do cliente
                    Email = request.Email, //capturando o email do cliente
                    Cpf = request.Cpf, //capturando o CPF do cliente
                    Telefone = request.Telefone, //capturando o telefone do cliente
                    Enderecos = new() //Inicializando a lista de endereços
                };

                //Salvar o cliente no banco de dados
                _clienteRepository.Add(cliente);

                #endregion

                #region Cadastro dos endereços

                //Percorrer a lista de endereços do cliente
                foreach(var enderecos in request.Enderecos)
                {
                    //Capturar os dados do DTO para a entidade
                    var endereco = new Endereco()
                    {
                        Logradouro = enderecos.Logradouro,
                        Numero = enderecos.Numero,
                        Complemento = enderecos.Complemento,
                        Bairro = enderecos.Bairro,
                        Cidade = enderecos.Cidade,
                        Uf = enderecos.UF,
                        Cep = enderecos.Cep,
                        ClienteId = cliente.Id //Definindo o relacionamento
                    };

                    //Salvar o endereço no banco de dados
                    _enderecoRepository.Add(endereco);

                    //Adicionar o endereço ao cliente
                    cliente.Enderecos.Add(endereco);
                }

                #endregion

                //HTTP 201 - Created (Recurso criado com sucesso)
                return StatusCode(201, MapToDto(cliente));
            }
            catch (ApplicationException e)
            {
                //HTTP 400 - Bad Request (Requisição inválida do cliente)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - Internal Server Error (Erro interno no servidor)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ClienteRequestDto request)
        {
            try
            {
                //buscar o cliente no banco de dados através do ID
                var cliente = _clienteRepository.GetById(id);

                //Verificar se o cliente não foi encontrado
                if (cliente == null)
                    throw new ApplicationException("Cliente não encontrado para edição.");

                //Modificar os dados do cliente
                cliente.Nome = request.Nome;
                cliente.Cpf = request.Cpf;
                cliente.Email = request.Email;
                cliente.Telefone = request.Telefone;

                //Atualizando o cliente no banco de dados
                _clienteRepository.Update(cliente);

                //Percorrendo os endereços do cliente enviado na edição
                for (int i = 0; i < request.Enderecos.Count; i++)
                {
                    //alterar o endereço do cliente com os dados enviados
                    var endereco = cliente?.Enderecos?[i];
                    endereco?.Logradouro = request.Enderecos[i].Logradouro;
                    endereco?.Numero = request.Enderecos[i].Numero;
                    endereco?.Complemento = request.Enderecos[i].Complemento;
                    endereco?.Bairro = request.Enderecos[i].Bairro;
                    endereco?.Cidade = request.Enderecos[i].Cidade;
                    endereco?.Uf = request.Enderecos[i].UF;
                    endereco?.Cep = request.Enderecos[i].Cep;

                    //atualizando os dados do endereço no banco
                    _enderecoRepository.Update(endereco);
                }

                //Retornar sucesso HTTP 200 (OK)
                return StatusCode(200, MapToDto(cliente));
            }
            catch (ApplicationException e)
            {
                //HTTP 400 - Bad Request (Requisição inválida do cliente)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - Internal Server Error (Erro interno no servidor)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //buscar o cliente no banco de dados através do ID
                var cliente = _clienteRepository.GetById(id);

                //Verificar se o cliente não foi encontrado
                if (cliente == null)
                    throw new ApplicationException("Cliente não encontrado para exclusão.");

                //Excluir os endereços do cliente
                foreach (var item in cliente.Enderecos.ToList())
                    _enderecoRepository.Delete(item);

                //Excluir o cliente
                _clienteRepository.Delete(cliente);

                //Retornar sucesso HTTP 200 (OK)
                return StatusCode(200, MapToDto(cliente));
            }
            catch (ApplicationException e)
            {
                //HTTP 400 - Bad Request (Requisição inválida do cliente)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - Internal Server Error (Erro interno no servidor)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //buscar o cliente no banco de dados através do ID
                var cliente = _clienteRepository.GetById(id);

                //Verificar se o cliente não foi encontrado
                if (cliente == null)
                    throw new ApplicationException("Cliente não encontrado.");

                //Retornar sucesso HTTP 200 (OK)
                return StatusCode(200, MapToDto(cliente));
            }
            catch (ApplicationException e)
            {
                //HTTP 400 - Bad Request (Requisição inválida do cliente)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 - Internal Server Error (Erro interno no servidor)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string nome)
        {
            try
            {
                //Verificando se o nome esta null ou se tem menos de 3 caracteres
                if(nome == null || nome.Trim().Length < 3)
                    throw new ApplicationException("O nome deve conter pelo menos 3 caracteres.");

                //Consultar os clientes no repositório
                var clientes = _clienteRepository.GetAllByNome(nome.Trim());

                //Verificar se nenhum cliente foi encontrado
                if (clientes == null || !clientes.Any())
                    return StatusCode(204); //HTTP 204 - No Content (Nenhum conteúdo)                    

                //Mapear os clientes para DTOs de resposta
                var response = clientes.Select(cliente => MapToDto(cliente)).ToList();

                //HTTP 200 - OK (Requisição bem-sucedida)
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                //HTTP 400 - Bad Request (Requisição inválida do cliente)
                return StatusCode(400, new { e.Message });
            }
            catch(Exception e)
            {
                //HTTP 500 - Internal Server Error (Erro interno no servidor)
                return StatusCode(500, new { e.Message });
            }
        }

        /*
         * Método para copiar os dados da entidade Cliente
         * para um objeto do tipo ClienteResponseDto
         */ 
        private ClienteResponseDto MapToDto(Cliente cliente)
        {
            var response = new ClienteResponseDto(
                cliente.Id, //Copiando o ID do cliente
                cliente.Nome, //Copiando o nome do cliente
                cliente.Email, //Copiando o email do cliente
                cliente.Telefone, //Copiando o telefone do cliente
                cliente.Cpf, //Copiando o CPF do cliente
                cliente.Enderecos != null ?
                    cliente.Enderecos.Select(endereco => new EnderecoResponseDto( //Copiando os endereços do cliente
                        endereco.Id, //Id do endereço
                        endereco.Logradouro, //Logradouro do endereço
                        endereco.Numero, //Número do endereço
                        endereco.Complemento, //Complemento do endereço
                        endereco.Bairro, //Bairro do endereço
                        endereco.Cidade, //Cidade do endereço
                        endereco.Uf, //Estado do endereço
                        endereco.Cep //
                    )).ToList() : new List<EnderecoResponseDto>() //Caso não tenha endereços, retorna uma lista vazia
            );

            //Retornar o objeto do tipo ClienteResponseDto
            return response;
        }
    }
}
