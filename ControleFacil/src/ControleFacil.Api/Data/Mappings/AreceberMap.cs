using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleFacil.Api.Damain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleFacil.Api.Data.Mappings
{
    public class AreceberMap : IEntityTypeConfiguration<Areceber>
    {
        public void Configure(EntityTypeBuilder<Areceber> builder)
        {
            builder.ToTable("areceber")
            .HasKey(p => p.id);

            builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(fk => fk.idUsuario);

            builder.HasOne(p => p.NaturezaDeLancamento)
            .WithMany()
            .HasForeignKey(fk => fk.idNaturezaDeLancamento);

            builder.Property(p => p.descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.observacao)
            .HasColumnType("VARCHAR");
        

            builder.Property(p => p.valorOriginal)
            .HasColumnType("double precision")
            .IsRequired();

            builder.Property(p => p.valorRecebido)
            .HasColumnType("double precision");

            builder.Property(p => p.dataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.dataReferencia)
            .HasColumnType("timestamp");

            builder.Property(p => p.dataVencimento)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.dataRecebimento)
            .HasColumnType("timestamp");

            
            builder.Property(p => p.dataInativacao)
            .HasColumnType("timestamp");



        }

    }
}