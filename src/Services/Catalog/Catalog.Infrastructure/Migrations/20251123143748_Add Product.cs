using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ProductHierarchies_ProductHierarchyId",
                table: "Brands");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brands",
                table: "Brands");

            migrationBuilder.RenameTable(
                name: "Brands",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Brand",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "ProductHierarchyId",
                table: "Brand",
                newName: "producthierarchyid");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brand",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Brand",
                newName: "modifieddate");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Brand",
                newName: "modifiedby");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Brand",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Brand",
                newName: "createdby");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Brand",
                newName: "code");

            migrationBuilder.RenameIndex(
                name: "IX_Brands_ProductHierarchyId",
                table: "Brand",
                newName: "IX_Brand_producthierarchyid");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Brand",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "modifiedby",
                table: "Brand",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "createdby",
                table: "Brand",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Brand",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brand",
                table: "Brand",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UnitId = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    BarCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_code",
                table: "Brand",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_name",
                table: "Brand",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BarCode",
                table: "Product",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Code",
                table: "Product",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand",
                column: "producthierarchyid",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brand",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_code",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_name",
                table: "Brand");

            migrationBuilder.RenameTable(
                name: "Brand",
                newName: "Brands");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Brands",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "producthierarchyid",
                table: "Brands",
                newName: "ProductHierarchyId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Brands",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "modifieddate",
                table: "Brands",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modifiedby",
                table: "Brands",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "Brands",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "createdby",
                table: "Brands",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Brands",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_Brand_producthierarchyid",
                table: "Brands",
                newName: "IX_Brands_ProductHierarchyId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Brands",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Brands",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Brands",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brands",
                table: "Brands",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ProductHierarchies_ProductHierarchyId",
                table: "Brands",
                column: "ProductHierarchyId",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
