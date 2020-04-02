using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.UF)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.HasMany(p => p.Cidades)
                .WithOne(p => p.Estado)
                .HasForeignKey(p => p.EstadoId);

            builder.ToTable("Estados");
        }
    }
}
