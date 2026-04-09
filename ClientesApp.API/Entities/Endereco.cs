namespace ClientesApp.API.Entities
{
    /// <summary>
    ///  Modelo de entidade para a classe Endereço
    /// </summary>
    public class Endereco
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public Guid ClienteId { get; set; }

        #region Relacionamentos

        public Cliente? Cliente { get; set; }

        #endregion
    }
}
