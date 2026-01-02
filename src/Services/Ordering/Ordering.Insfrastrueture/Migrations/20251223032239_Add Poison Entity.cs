using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Insfrastrueture.Migrations
{
    /// <inheritdoc />
    public partial class AddPoisonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PoisonedOn",
                table: "OutboxMessages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "OutboxMessages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoisonedOn",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "OutboxMessages");
        }
    }
}
