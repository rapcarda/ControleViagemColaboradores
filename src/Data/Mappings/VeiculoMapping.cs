using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mappings
{
    class VeiculoMapping: IEntityTypeConfiguration<Veiculo>
    {
        public void Configure(EntityTypeBuilder<Veiculo> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Codigo)
                .IsRequired()
                .HasColumnType("varchar(15)");

            builder.Property(v => v.Modelo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(v => v.Placa)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.ToTable("Veiculo");
        }
    }
}
