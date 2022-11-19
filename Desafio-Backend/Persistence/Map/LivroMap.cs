using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Backend.Domain.Models;

namespace Desafio_Backend.Infrastructure.Map
{
    public class LivroMap : BaseMap<Livro>
    {
        public LivroMap() : base("Livro")
        {

        }

        public override void Configure(EntityTypeBuilder<Livro> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.id);
            builder.Property(x => x.id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.nome).HasColumnName("nome").IsRequired();
            builder.Property(x => x.descricao).HasColumnName("descricao").IsRequired();
            builder.Property(x => x.edicao).HasColumnName("edicao").IsRequired();
            builder.Property(x => x.anoPublicacao).HasColumnName("anoPublicacao").IsRequired();
            builder.Property(x => x.dataCadastro).HasColumnName("dataCadastro").IsRequired();
            builder.Property(x => x.urlCapa).HasColumnName("urlCapa").IsRequired();
            builder.Property(x => x.valor).HasColumnName("valor").IsRequired();
            builder.Property(x => x.avaliacao).HasColumnName("avaliacao").IsRequired();

            builder.HasOne(x => x.Genero).WithMany(x => x.Livros).HasForeignKey(x => x.idGenero);
        }
    }
}
