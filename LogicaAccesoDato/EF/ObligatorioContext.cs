using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ObligatorioDW;
using ObligatorioDW.Entidades;

namespace LogicaAccesoDato.EF
{
    public class ObligatorioContext : DbContext

    {//Esta va a ser la clase que se conecta con la base de datos 
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Historia> Historias { get; set; }
        public DbSet<Capitulo> Capitulos { get; set; }
        public DbSet<Lectura> Lecturas { get; set; }
        
        public DbSet<Auditoria> Auditorias { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Le indicamos al metodo UseSqlServer la informacion la base de datos a utilizar
            //pasandole por parametro una cadena con el ConectioString.
            //El @ delante de las comillas es para que tome la contrabarra literalmente y no como una secuencia de escape.

            ;
            string connectionString = "Server=localhost,1433;Database=Lectura2026;User Id=sa;Password=Obligatorio2026!;TrustServerCertificate=True;";
               // "workstation id=ObliFloMedina.mssql.somee.com;packet size=4096;user id=florflor_SQLLogin_4;pwd=p5cilj76n8;data source=ObliFloMedina.mssql.somee.com;persist security info=False;initial catalog=ObliFloMedina;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(
                connectionString
            );
        }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .OwnsOne(u => u.Password);

            modelBuilder.Entity<Capitulo>()
                .HasDiscriminator<string>("TipoCapitulo")
                .HasValue<Capitulo>("Base")
                .HasValue<CapituloIntermedio>("Intermedio")
                .HasValue<CapituloFinal>("Final");
            
            
            modelBuilder.Entity<Opcion>()
                .HasOne(o => o.CapituloDestino)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Lectura>()
                .HasOne(l => l.Historia)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
    
}