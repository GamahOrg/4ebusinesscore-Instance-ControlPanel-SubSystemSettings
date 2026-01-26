using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class securitysetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SSOSecuritySettingId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SSOSecuritySetting",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSOSecuritySetting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_SSOSecuritySettingId",
                table: "SubsytemSettings",
                column: "SSOSecuritySettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsytemSettings_SSOSecuritySetting_SSOSecuritySettingId",
                table: "SubsytemSettings",
                column: "SSOSecuritySettingId",
                principalTable: "SSOSecuritySetting",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SSOSecuritySetting_SSOSecuritySettingId",
                table: "SubsytemSettings");

            migrationBuilder.DropTable(
                name: "SSOSecuritySetting");

            migrationBuilder.DropIndex(
                name: "IX_SubsytemSettings_SSOSecuritySettingId",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "SSOSecuritySettingId",
                table: "SubsytemSettings");
        }
    }
}
