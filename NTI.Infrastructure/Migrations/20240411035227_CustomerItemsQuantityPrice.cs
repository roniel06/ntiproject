﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerItemsQuantityPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "CustomerItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CustomerItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CustomerItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CustomerItems");
        }
    }
}
