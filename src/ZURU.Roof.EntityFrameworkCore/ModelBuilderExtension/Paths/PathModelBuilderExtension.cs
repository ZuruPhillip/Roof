using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using ZURU.Roof.Paths;

namespace ZURU.Roof.ModelBuilderExtension.Paths
{
    public static class PathModelBuilderExtension
    {
        public static void ConfigureRobotPath(this ModelBuilder builder)
        {
            builder.Entity<RobotPath>(entity =>
            {
                entity.ToTable(RoofServiceConsts.DbTablePrefix + "RobotPath",
                    RoofServiceConsts.DbSchema, tb => tb.HasComment("机械臂路径记录表"));
                entity.ConfigureByConvention(); //auto configure for the base class props

                entity.Property(x => x.Id).HasComment("路径Id").IsRequired().ValueGeneratedOnAdd();
                entity.Property(x => x.RoofId).HasComment("屋顶Id");
                entity.Property(x => x.Index).HasComment("动作下标");
                entity.Property(x => x.ActionId).HasComment("动作Id");
                entity.Property(x => x.RobotId).HasComment("机械臂Id");
                entity.Property(x => x.ActionType).HasComment("动作类型： 1：机器人，2：延时，3：开关");
                entity.Property(x => x.NextActionType).HasComment("下一个类型： 1：立即，2：当上一个动作完成时，3：当前动作完成前");
                entity.Property(x => x.X).HasComment("X 坐标值");
                entity.Property(x => x.Y).HasComment("Y 坐标值");
                entity.Property(x => x.Z).HasComment("Z 坐标值");
                entity.Property(x => x.A).HasComment("轴A的值");
                entity.Property(x => x.B).HasComment("轴B的值");
                entity.Property(x => x.C).HasComment("轴C的值");
                entity.Property(x => x.S).HasComment("S的值");
                entity.Property(x => x.T).HasComment("T的值");
                entity.Property(x => x.KukaMotionType).HasComment("机器人动作类型： 1：PTP，2：LIN，3：CIRC");
                entity.Property(x => x.Velocity).HasComment("速度");
                entity.Property(x => x.OverWrite).HasComment("倍率");
                entity.Property(x => x.CreateTime).HasComment("创建时间");
            });
        }
    }
}
