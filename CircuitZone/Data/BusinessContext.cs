using CircuitZone.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircuitZone.Data
{
    public class BusinessContext : DbContext, IBusinessContext 
    {
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Movimento> Movimentos { get; set; }
        public DbSet<TipoMovimento> TipoMovimentos { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=SQL6033.site4now.net;" +
                "Initial Catalog=db_ab232d_circuitzone;" +
                "User Id=db_ab232d_circuitzone_admin;" +
                "Password=swuH.F!M9x.&JnG;" +
                "TrustServerCertificate=True;" +
                "Encrypt=False");
        }
    }
}
