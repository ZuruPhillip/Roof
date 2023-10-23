using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.EntityFrameworkCore.Modeling;
using ZURU.Roof.Roofs;

namespace ZURU.Roof.ModelBuilderExtension.Roofs
{
    public static class RoofPointModelBuilderExtension
    {
        public static void ConfigureRoofPoint(this ModelBuilder builder)
        {
            builder.Entity<RoofPoint>(entity =>
            {
                entity.ToTable(RoofServiceConsts.DbTablePrefix + "RoofPoint",
                    RoofServiceConsts.DbSchema, tb => tb.HasComment("屋顶顶点记录表"));
                entity.ConfigureByConvention(); //auto configure for the base class props

                entity.Property(x => x.Id).HasComment("屋顶编号").IsRequired().HasMaxLength(128);
                entity.Property(x => x.PointIndex).HasComment("点下标值");
                entity.Property(x => x.X).HasComment("X 坐标值");
                entity.Property(x => x.Y).HasComment("Y 坐标值");
                entity.Property(x => x.Z).HasComment("Z 坐标值");
                entity.Property(x => x.PointType).HasComment("1 : 轮廓点，2 ：中间点");

                entity.HasOne(d => d.RoofRecord).WithMany(p => p.RoofPoints).HasForeignKey(d => d.RecordId);
            });
        }
    }
}
