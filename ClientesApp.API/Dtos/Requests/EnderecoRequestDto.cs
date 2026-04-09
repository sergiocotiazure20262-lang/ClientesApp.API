namespace ClientesApp.API.Dtos.Requests
{
    /// <summary>
    /// DTO para requisição de endereços na API
    /// </summary>
    public record EnderecoRequestDto(
            string Logradouro, //Logradouro do endereço
            string Numero, //Número do endereço
            string Complemento, //Complemento do endereço
            string Bairro, //Bairro do endereço
            string Cidade, //Cidade do endereço
            string UF, //UF do endereço
            string Cep //CEP do endereço
        );
}
