using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class add_plcDatas : Migration
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

            migrationBuilder.CreateTable(
                name: "tb_PlcDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(128)", maxLength: 128, nullable: false, comment: "Id", collation: "ascii_general_ci"),
                    RoofId = table.Column<string>(type: "longtext", nullable: false, comment: "屋顶Id")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NodeId = table.Column<string>(type: "longtext", nullable: false, comment: "NodeId")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false, comment: "NodeId 值")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PlcDatas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_PlcDatas");

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
