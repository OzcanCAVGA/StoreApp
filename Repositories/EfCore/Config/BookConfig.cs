using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { ID = 1, Title = "Karagoz ve Hacivat", Price = 75 },
                new Book { ID = 2, Title = "Mesnevi", Price = 150 },
                new Book { ID = 3, Title = "Devlet", Price = 200 }
                );
        }
    }
}
