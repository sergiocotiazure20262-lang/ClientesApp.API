namespace ClientesApp.API.Dtos.Responses
{
    /// <summary>
    /// DTO para retornar dados de cliente na API
    /// </summary>
    public record ClienteResponseDto(
            Guid Id, // Identificador único do cliente
            string Nome, // Nome do cliente
            string Email, // Email do cliente
            string Telefone,// Telefone do cliente
            string Cpf, // CPF do cliente
            List<EnderecoResponseDto> enderecos // Lista de endereços do cliente
        );
}
