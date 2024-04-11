using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerItemIsActiveProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CustomerItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CustomerItems");
        }
    }
}
