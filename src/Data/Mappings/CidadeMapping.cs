﻿using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(p => p.Empresas)
                .WithOne(p => p.Cidade)
                .HasForeignKey(p => p.CidadeId);

            builder.ToTable("Cidades");
        }
    }
}
