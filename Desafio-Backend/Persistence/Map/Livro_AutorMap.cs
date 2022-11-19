using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Backend.Domain.Models;

namespace Desafio_Backend.Infrastructure.Map
{
    public class Livro_AutorMap : BaseMap<Livro_Autor>
    {
        public Livro_AutorMap() : base("Livro_Autor")
        {

        }

        public override void Configure(EntityTypeBuilder<Livro_Autor> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.idAutor).HasColumnName("idAutor").IsRequired();
            builder.Property(x => x.idLivro).HasColumnName("idLivro").IsRequired();

            builder.HasOne(x => x.Livro).WithMany(x => x.Livro_Autores).HasForeignKey(x => x.idLivro);
            builder.HasOne(x => x.Autor).WithMany(x => x.Livro_Autores).HasForeignKey(x => x.idAutor);
        }
    }
}
