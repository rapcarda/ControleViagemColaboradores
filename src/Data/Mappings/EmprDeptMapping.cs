using Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mappings
{
    public class EmprDeptMapping : IEntityTypeConfiguration<EmprDept>
    {
        public void Configure(EntityTypeBuilder<EmprDept> builder)
        {
            builder.HasKey(p => new { p.EmpresaId, p.DepartamentoId });

            builder.Property(p => p.EmpresaId)
                .IsRequired();

            builder.Property(p => p.DepartamentoId)
                .IsRequired();

            builder.HasOne(p => p.Empresa)
                .WithMany(e => e.EmpresaDepartamentos)
                .HasForeignKey(p => p.EmpresaId);

            builder.HasOne(p => p.Departamento)
                .WithMany(e => e.EmpresasDepartamento)
                .HasForeignKey(p => p.DepartamentoId);

            builder.ToTable("EmprDept");
        }
    }
}
