using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ControleFacil.Api.Damain.Models;
using ControleFacil.Api.Data.Mappings;

namespace ControleFacil.Api.Data
{  //Todo context herda de DbContext
    public class ApplicationContext : DbContext
    {
        //Armagena a entidade no DbSet
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<NaturezaDeLancamento> NaturezaDeLancamento { get; set; }
        public DbSet<Apagar> Apagar { get; set; }
        public DbSet<Areceber> Areceber { get; set; }


        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Se basear no UsuarioMap para criar um usuario
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            modelBuilder.ApplyConfiguration(new NaturezaDeLancamentoMap());

            modelBuilder.ApplyConfiguration(new ApagarMap());

             modelBuilder.ApplyConfiguration(new AreceberMap());
        }
    }
}