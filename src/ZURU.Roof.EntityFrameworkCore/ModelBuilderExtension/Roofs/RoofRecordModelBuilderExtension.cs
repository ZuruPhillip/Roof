using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.ModelBuilderExtension.Roofs
{
    public static class RoofRecordModelBuilderExtension
    {
        public static void ConfigureRoofRecord(this ModelBuilder builder)
        {
            builder.Entity<RoofRecord>(entity =>
            {
                entity.ToTable(RoofServiceConsts.DbTablePrefix + "RoofRecord",
                    RoofServiceConsts.DbSchema, tb => tb.HasComment("屋顶记录表"));
                entity.ConfigureByConvention(); //auto configure for the base class props

                entity.Property(x => x.Id).HasComment("屋顶编号").IsRequired().HasMaxLength(128);
                entity.Property(x => x.Status).HasComment("0 ： 停用， 1 ：启用");
            });
        }
    }
}
