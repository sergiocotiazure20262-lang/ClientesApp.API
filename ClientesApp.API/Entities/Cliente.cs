namespace ClientesApp.API.Entities
{
    /// <summary>
    /// Modelo de entidade para a classe Cliente
    /// </summary>
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        #region Relacionamentos

        public List<Endereco>? Enderecos { get; set; }

        #endregion
    }
}
