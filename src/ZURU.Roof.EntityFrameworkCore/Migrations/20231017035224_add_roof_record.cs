using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZURU.Roof.Migrations
{
    /// <inheritdoc />
    public partial class add_roof_record : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_RoofRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(128)", maxLength: 128, nullable: false, comment: "屋顶编号", collation: "ascii_general_ci"),
                    RoofId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false, comment: "0 ： 停用， 1 ：启用"),
                    ExtraProperties = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LastModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RoofRecord", x => x.Id);
                },
                comment: "屋顶记录表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tb_RoofPoint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(128)", maxLength: 128, nullable: false, comment: "屋顶编号", collation: "ascii_general_ci"),
                    PointIndex = table.Column<int>(type: "int", nullable: false, comment: "点下标值"),
                    PointType = table.Column<int>(type: "int", nullable: false, comment: "1 : 轮廓点，2 ：中间点"),
                    X = table.Column<float>(type: "float", nullable: false, comment: "X 坐标值"),
                    Y = table.Column<float>(type: "float", nullable: false, comment: "Y 坐标值"),
                    Z = table.Column<float>(type: "float", nullable: false, comment: "Z 坐标值"),
                    RoofRecordId = table.Column<Guid>(type: "char(128)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_RoofPoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_RoofPoint_tb_RoofRecord_RoofRecordId",
                        column: x => x.RoofRecordId,
                        principalTable: "tb_RoofRecord",
                        principalColumn: "Id");
                },
                comment: "屋顶顶点记录表")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tb_RoofPoint_RoofRecordId",
                table: "tb_RoofPoint",
                column: "RoofRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_RoofPoint");

            migrationBuilder.DropTable(
                name: "tb_RoofRecord");
        }
    }
}
