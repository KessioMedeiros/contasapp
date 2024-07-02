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
    public class ContaConfiguration : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            //nome da tabela no banco de dados
            builder.ToTable("CONTA");

            //chave primária
            builder.HasKey(c => c.Id);

            //mapeamento do campo 'Id'
            builder.Property(c => c.Id)
                .HasColumnName("ID");

            //mapeamento do campo 'Nome'
            builder.Property(c => c.Nome)
            .HasColumnName("NOME")
            .HasMaxLength(150)
            .IsRequired();

            //mapeamento do campo 'Data'
            builder.Property(c => c.Data)
            .HasColumnName("DATA")
            .HasColumnType("date")
            .IsRequired();

            //mapeamento do campo 'Valor'
            builder.Property(c => c.Valor)
            .HasColumnName("VALOR")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

            //mapeamento do campo 'Observacoes'
            builder.Property(c => c.Observacoes)
            .HasColumnName("OBSERVACOES")
            .HasMaxLength(250)
            .IsRequired();

            //mapeamento do campo 'CategoriaId'
            builder.Property(c => c.CategoriaId)
            .HasColumnName("CATEGORIA_ID")
            .IsRequired();

            //mapeamento do campo 'UsuarioId'
            builder.Property(c => c.UsuarioId)
            .HasColumnName("USUARIO_ID")
            .IsRequired();

            //mapeamento do relacionamento com Categoria (OneToMany)
            builder.HasOne(c => c.Categoria) //Conta TEM 1 Categoria
            .WithMany(ca => ca.Contas) //Categoria TEM MUITAS Contas
            .HasForeignKey(c => c.CategoriaId) //chave estrangeira
            .OnDelete(DeleteBehavior.NoAction);

            //mapeamento do relacionamento com Usuario (OneToMany)
            builder.HasOne(c => c.Usuario) //Conta TEM 1 Usuário
            .WithMany(u => u.Contas) //Usuario TEM MUITAS Contas
            .HasForeignKey(c => c.UsuarioId) //chave estrangeira
            .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
