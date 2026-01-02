using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                column: "ParentId",
                principalTable: "ProductHierarchies",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductHierarchies_ProductHierarchies_ParentId",
                table: "ProductHierarchies",
                column: "ParentId",
                principalTable: "ProductHierarchies",
                principalColumn: "id");
        }
    }
}
