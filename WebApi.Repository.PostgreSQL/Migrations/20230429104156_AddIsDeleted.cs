using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Repository.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quentity",
                table: "OrderItemsEntities",
                newName: "Quantity");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderItemsEntities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "OrderEntities",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderItemsEntities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "OrderEntities");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItemsEntities",
                newName: "Quentity");
        }
    }
}
