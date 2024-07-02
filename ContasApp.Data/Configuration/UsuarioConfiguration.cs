using ContasApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //nome da tabela no banco de dados
            builder.ToTable("USUARIO");

            //chave primária
            builder.HasKey(u => u.Id);

            //mapeamento do campo 'Id'
            builder.Property(u => u.Id)

            .HasColumnName("ID");
            //mapeamento do campo 'Nome'
            builder.Property(u => u.Nome)

            .HasColumnName("NOME")
            .HasMaxLength(150)
            .IsRequired();

            //mapeamento do campo 'Email'
            builder.Property(u => u.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(100)
            .IsRequired();

            //criando um índice no campo Email tornando-o único
            builder.HasIndex(u => u.Email).IsUnique();

            //mapeamento do campo 'Senha'
            builder.Property(u => u.Senha)
            .HasColumnName("SENHA")
            .HasMaxLength(40)
            .IsRequired();
        }
    }
}
