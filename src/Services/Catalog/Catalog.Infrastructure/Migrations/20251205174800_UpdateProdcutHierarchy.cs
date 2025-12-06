using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProdcutHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                column: "ParentId",
                principalTable: "ProductHierarchies",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies");

            migrationBuilder.DropIndex(
                name: "IX_ProductHierarchies_ParentId",
                table: "ProductHierarchies");
        }
    }
}
