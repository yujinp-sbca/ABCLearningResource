using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Learning.Resource.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "UserAccounts",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "UserAccounts",
                newName: "UserName");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }
    }
}
