using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Backend.Domain.Models;

namespace Desafio_Backend.Infrastructure.Map
{
    public class AutorMap : BaseMap<Autor>
    {
        public AutorMap() : base("Autor")
        {

        }

        public override void Configure(EntityTypeBuilder<Autor> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.id);
            builder.Property(x => x.id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.nome).HasColumnName("nome").IsRequired();
            builder.Property(x => x.descricao).HasColumnName("descricao").IsRequired();
        }
    }
}
