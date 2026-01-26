using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewModelSubsytemSettingsMainClassification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubsytemMainId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubsytemSettingsMainClassification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsytemSettingsMainClassification", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_SubsytemMainId",
                table: "SubsytemSettings",
                column: "SubsytemMainId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsytemSettings_SubsytemSettingsMainClassification_SubsytemMainId",
                table: "SubsytemSettings",
                column: "SubsytemMainId",
                principalTable: "SubsytemSettingsMainClassification",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SubsytemSettingsMainClassification_SubsytemMainId",
                table: "SubsytemSettings");

            migrationBuilder.DropTable(
                name: "SubsytemSettingsMainClassification");

            migrationBuilder.DropIndex(
                name: "IX_SubsytemSettings_SubsytemMainId",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "SubsytemMainId",
                table: "SubsytemSettings");
        }
    }
}
