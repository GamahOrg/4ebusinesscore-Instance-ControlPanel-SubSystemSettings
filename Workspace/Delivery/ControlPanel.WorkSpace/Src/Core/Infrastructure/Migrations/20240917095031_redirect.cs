using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class redirect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "SubsytemSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                table: "SubsytemSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityProvider",
                table: "SubsytemSettings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                table: "SubsytemSettings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSinglSignOn",
                table: "SubsytemSettings",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "IdentityProvider",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "IsSinglSignOn",
                table: "SubsytemSettings");
        }
    }
}
