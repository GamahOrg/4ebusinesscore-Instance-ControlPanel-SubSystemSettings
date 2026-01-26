using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class sq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Squence",
                table: "SubsytemSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SubsystemIsVisiable",
                table: "SubsytemSettings",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Squence",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "SubsystemIsVisiable",
                table: "SubsytemSettings");
        }
    }
}
