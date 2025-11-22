using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductHierarchyIdinBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductHierarchyId",
                table: "Brands",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_ProductHierarchyId",
                table: "Brands",
                column: "ProductHierarchyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_ProductHierarchies_ProductHierarchyId",
                table: "Brands",
                column: "ProductHierarchyId",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brands_ProductHierarchies_ProductHierarchyId",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_ProductHierarchyId",
                table: "Brands");

            migrationBuilder.DropColumn(
                name: "ProductHierarchyId",
                table: "Brands");
        }
    }
}
