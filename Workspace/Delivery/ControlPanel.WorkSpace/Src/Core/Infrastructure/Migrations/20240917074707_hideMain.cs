using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class hideMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHide",
                table: "SubsytemSettingsMainClassification",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHide",
                table: "SubsytemSettingsMainClassification");
        }
    }
}
