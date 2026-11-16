using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ObligatorioDW.Entidades;

namespace LogicaAccesoDatos.EF
{
    public class ObligatorioContext : DbContext

    {
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Le indicamos al metodo UseSqlServer la informacion la base de datos a utilizar
            //pasandole por parametro una cadena con el ConectioString.
            //El @ delante de las comillas es para que tome la contrabarra literalmente y no como una secuencia de escape.

            optionsBuilder.UseSqlServer(
                @"SERVER=(localdb)\MsSqlLocalDb;DATEBASE=Lectura2026;Integrated Security=true;");
        }
    }
}
