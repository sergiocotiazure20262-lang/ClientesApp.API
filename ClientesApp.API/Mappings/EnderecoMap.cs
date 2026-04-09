using ClientesApp.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientesApp.API.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Endereço.
    /// </summary>
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("ENDERECOS");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Logradouro).HasColumnName("LOGRADOURO").IsRequired();
            builder.Property(e => e.Numero).HasColumnName("NUMERO").IsRequired();
            builder.Property(e => e.Complemento).HasColumnName("COMPLEMENTO");
            builder.Property(e => e.Bairro).HasColumnName("BAIRRO").IsRequired();
            builder.Property(e => e.Cidade).HasColumnName("CIDADE").IsRequired();
            builder.Property(e => e.Uf).HasColumnName("UF").IsRequired();
            builder.Property(e => e.Cep).HasColumnName("CEP").IsRequired();
            builder.Property(e => e.ClienteId).HasColumnName("CLIENTE_ID").IsRequired();

            #region Mapeamento do relacionamento

            builder.HasOne(e => e.Cliente) //Endereço TEM 1 Cliente
                .WithMany(c => c.Enderecos) //Cliente POSSUI MUITOS Endereços
                .HasForeignKey(e => e.ClienteId); //Chave estrangeira em Endereço

            #endregion
        }
    }
}
