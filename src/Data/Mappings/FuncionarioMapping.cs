using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(p => p.Bairro)
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Complemento)
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.ToTable("Funcionario");

        }
    }
}
