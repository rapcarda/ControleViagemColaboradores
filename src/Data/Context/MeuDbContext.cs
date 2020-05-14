using Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
        }

        /*DBSets*/
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<EmprDept> EmprDept { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Para evitar que se esquecer de criar uma propriedade de alguma entidade no mapping, e a mesma for string */
            /* então o EF cria como nvarchar(max), para evitar isso, o código abaixo, varre as propriedades do tipo string, */
            /* e altera para varchar(100). Isso não sobre escreve o que foi definido no mapping */
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                   .Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            /* Em versões anteiores, era preciso dar o Aplly para cada entidade e classe de mapping. */
            /* Agora, ao dar o Aplly from assembly, o EF já busca o contexto, pega todos os dbsets do contexto */
            /* procura por arquivos que herdam de IEntityTypeConfiguration da entidade do dbset, e cria tudo de uma vez */
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            /*Desativar o cascade no delete*/
            foreach (var relations in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relations.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
