using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class add_roofId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "tb_RoofRecord",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "屋顶编号",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "屋顶编号")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecordId",
                table: "tb_RoofPoint",
                type: "char(128)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(128)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "tb_RoofPoint",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "屋顶编号",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "屋顶编号")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "tb_RobotPath",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "路径Id",
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "路径Id")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "tb_RobotPath",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "创建时间");

            migrationBuilder.AddColumn<string>(
                name: "RoofId",
                table: "tb_RobotPath",
                type: "longtext",
                nullable: false,
                comment: "屋顶Id")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "tb_RobotPath");

            migrationBuilder.DropColumn(
                name: "RoofId",
                table: "tb_RobotPath");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "tb_RoofRecord",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "屋顶编号",
                oldClrType: typeof(Guid),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "屋顶编号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "RecordId",
                table: "tb_RoofPoint",
                type: "char(128)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(128)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "tb_RoofPoint",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "屋顶编号",
                oldClrType: typeof(Guid),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "屋顶编号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "tb_RobotPath",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "路径Id",
                oldClrType: typeof(Guid),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "路径Id")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
