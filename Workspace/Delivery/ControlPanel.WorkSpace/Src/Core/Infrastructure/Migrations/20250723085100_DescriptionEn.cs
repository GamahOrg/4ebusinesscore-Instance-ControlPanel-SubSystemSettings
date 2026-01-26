using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionEn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "SubsytemSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "SubsytemSettings");
        }
    }
}
