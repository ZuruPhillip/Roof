using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class update_plcData : Migration
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

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "tb_PlcDatas",
                type: "bigint",
                nullable: false,
                comment: "Id",
                oldClrType: typeof(string),
                oldType: "char(128)",
                oldMaxLength: 128,
                oldComment: "Id")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
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
                table: "tb_PlcDatas",
                type: "char(128)",
                maxLength: 128,
                nullable: false,
                comment: "Id",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "Id")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
