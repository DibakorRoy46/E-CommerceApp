using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "ProductHierarchies",
                newName: "Parentid");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                newName: "IX_ProductHierarchies_Parentid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_Parentid",
                table: "ProductHierarchies",
                column: "Parentid",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_Parentid",
                table: "ProductHierarchies");

            migrationBuilder.RenameColumn(
                name: "Parentid",
                table: "ProductHierarchies",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductHierarchies_Parentid",
                table: "ProductHierarchies",
                newName: "IX_ProductHierarchies_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                column: "ParentId",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
