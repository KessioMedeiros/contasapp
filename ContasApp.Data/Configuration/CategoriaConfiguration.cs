using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //nome da tabela no banco de dados
            builder.ToTable("CATEGORIA");

            //chave primaria
            builder.HasKey(c => c.Id);

            //mapeando o campo 'Id'
            builder.Property(c => c.Id)
                .HasColumnName("ID");

            //mapeando o capo 'Nome'
            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            //mapeando o campo 'tipo'
            builder.Property(c => c.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            //mapeando o relacinamento OnToMany
            builder.HasOne(c => c.Usuario) // Categoria tem 1 Usuario
                .WithMany(u => u.Categorias) // Usuario tem MUITAS Categorias
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
