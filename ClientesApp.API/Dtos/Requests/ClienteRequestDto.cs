namespace ClientesApp.API.Dtos.Requests
{
    /// <summary>
    /// DTO para requisição de clientes na API
    /// </summary>
    public record ClienteRequestDto(
            string Nome, //Nome do cliente
            string Email, //Email do cliente
            string Telefone, //Telefone do cliente
            string Cpf, //CPF do cliente
            List<EnderecoRequestDto> Enderecos //Lista de endereços do cliente
        );
}
