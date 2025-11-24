using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand",
                column: "producthierarchyid",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_ProductHierarchies_producthierarchyid",
                table: "Brand",
                column: "producthierarchyid",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
