using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore.Modeling;
using ZURU.Roof.Books;

namespace ZURU.Roof.ModelBuilderExtension.Books
{
    public static class BooksModelBuilderExtension
    {
        public static void ConfigureBooks(this ModelBuilder builder)
        {
            builder.Entity<Book>(b =>
            {
                b.ToTable(RoofServiceConsts.DbTablePrefix + "Books",
                    RoofServiceConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
        }
    }
}
