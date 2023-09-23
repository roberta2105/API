using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Damain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFacil.Api.Data.Mappings
{
    public class NaturezaDeLancamentoMap : IEntityTypeConfiguration<NaturezaDeLancamento>
    {
        public void Configure(EntityTypeBuilder<NaturezaDeLancamento> builder)
        {
            builder.ToTable("naturezaDeLancamento")
            .HasKey(p => p.id);

            builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(fk => fk.idUsuario);

            builder.Property(p => p.descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.observacao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.dataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p=> p.dataInativacao)
            .HasColumnType("timestamp");


        }

    }
}