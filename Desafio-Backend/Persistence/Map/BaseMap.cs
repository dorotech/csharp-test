using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Backend.Domain.Models;

namespace Desafio_Backend.Infrastructure.Map
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Base
    {
        string tablename;
        public BaseMap(string tableName)
        {
            tablename = tablename;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(tablename))
            {
                builder.ToTable(tablename);
            }
        }
    }
}
