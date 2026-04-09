using ClientesApp.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientesApp.API.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Cliente.
    /// </summary>
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTES");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("ID");
            builder.Property(c => c.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(c => c.Cpf).HasColumnName("CPF").IsRequired();
            builder.Property(c => c.Telefone).HasColumnName("TELEFONE").IsRequired();
            builder.Property(c => c.Email).HasColumnName("EMAIL").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("DATACRIACAO").IsRequired();
        }
    }
}
