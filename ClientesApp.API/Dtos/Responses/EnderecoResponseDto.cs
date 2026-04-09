namespace ClientesApp.API.Dtos.Responses
{
    /// <summary>
    /// DTO para retornar dados de endereço na API
    /// </summary>
    public record EnderecoResponseDto(
            Guid id, // Identificador único do endereço
            string Logradouro, // Logradouro do endereço
            string Numero, // Número do endereço
            string Complemento, // Complemento do endereço
            string Bairro, // Bairro do endereço
            string Cidade, // Cidade do endereço
            string Uf, // Estado do endereço
            string Cep // CEP do endereço
        );
}
