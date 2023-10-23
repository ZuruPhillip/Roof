using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class update_RoofRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_RoofPoint_tb_RoofRecord_RoofRecordId",
                table: "tb_RoofPoint");

            migrationBuilder.DropIndex(
                name: "IX_tb_RoofPoint_RoofRecordId",
                table: "tb_RoofPoint");

            migrationBuilder.DropColumn(
                name: "RoofRecordId",
                table: "tb_RoofPoint");

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

            migrationBuilder.AddColumn<Guid>(
                name: "RecordId",
                table: "tb_RoofPoint",
                type: "char(128)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_tb_RoofPoint_RecordId",
                table: "tb_RoofPoint",
                column: "RecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_RoofPoint_tb_RoofRecord_RecordId",
                table: "tb_RoofPoint",
                column: "RecordId",
                principalTable: "tb_RoofRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_RoofPoint_tb_RoofRecord_RecordId",
                table: "tb_RoofPoint");

            migrationBuilder.DropIndex(
                name: "IX_tb_RoofPoint_RecordId",
                table: "tb_RoofPoint");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "tb_RoofPoint");

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

            migrationBuilder.AddColumn<string>(
                name: "RoofRecordId",
                table: "tb_RoofPoint",
                type: "char(128)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_RoofPoint_RoofRecordId",
                table: "tb_RoofPoint",
                column: "RoofRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_RoofPoint_tb_RoofRecord_RoofRecordId",
                table: "tb_RoofPoint",
                column: "RoofRecordId",
                principalTable: "tb_RoofRecord",
                principalColumn: "Id");
        }
    }
}
