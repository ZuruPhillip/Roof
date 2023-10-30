using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore.Modeling;
using ZURU.Roof.Books;
using ZURU.Roof.PlcDatas;

namespace ZURU.Roof.ModelBuilderExtension.Books
{
    public static class PlcDatasModelBuilderExtension
    {
        public static void ConfigurePlcDatas(this ModelBuilder builder)
        {
            builder.Entity<PlcData>(entity =>
            {
                entity.ToTable(RoofServiceConsts.DbTablePrefix + "PlcDatas",
                    RoofServiceConsts.DbSchema);
                entity.ConfigureByConvention(); //auto configure for the base class props
                entity.Property(x => x.Id).IsRequired().HasComment("Id").ValueGeneratedOnAdd();
                entity.Property(x => x.RoofId).HasComment("屋顶Id");
                entity.Property(x => x.NodeId).HasComment("NodeId");
                entity.Property(x => x.Value).HasComment("NodeId 值");
                entity.Property(x => x.CreateTime).HasComment("创建时间");
            });
        }
    }
}
