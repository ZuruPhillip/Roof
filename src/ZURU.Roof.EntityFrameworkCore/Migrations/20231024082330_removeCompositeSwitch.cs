using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class removeCompositeSwitch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SwitchActionName",
                table: "tb_RobotPath");

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

            migrationBuilder.AlterColumn<string>(
                name: "ActionId",
                table: "tb_RobotPath",
                type: "longtext",
                nullable: true,
                comment: "动作Id",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldComment: "动作Id")
                .Annotation("MySql:CharSet", "utf8mb4")
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "tb_RobotPath",
                keyColumn: "ActionId",
                keyValue: null,
                column: "ActionId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ActionId",
                table: "tb_RobotPath",
                type: "longtext",
                nullable: false,
                comment: "动作Id",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true,
                oldComment: "动作Id")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.AddColumn<int>(
                name: "SwitchActionName",
                table: "tb_RobotPath",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "开关名称编号");
        }
    }
}
