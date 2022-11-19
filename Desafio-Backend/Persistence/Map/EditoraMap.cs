using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Backend.Domain.Models;

namespace Desafio_Backend.Infrastructure.Map
{
    public class EditoraMap : BaseMap<Editora>
    {
        public EditoraMap() : base("Editora")
        {

        }

        public override void Configure(EntityTypeBuilder<Editora> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.id);
            builder.Property(x => x.id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.nome).HasColumnName("nome").IsRequired();
            builder.Property(x => x.descricao).HasColumnName("descricao").IsRequired();

            builder.HasMany(x => x.Livros).WithOne(x => x.Editora).HasForeignKey(x => x.idEditora);
        }
    }
}
