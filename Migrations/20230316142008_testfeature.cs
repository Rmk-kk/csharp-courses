using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCoreCourse.Migrations
{
    /// <inheritdoc />
    public partial class testfeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "test",
                table: "students",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "test",
                table: "students");
        }
    }
}
