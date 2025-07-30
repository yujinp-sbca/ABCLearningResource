using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABC.Learning.Resource.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColorFieldOnBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Books");
        }
    }
}
