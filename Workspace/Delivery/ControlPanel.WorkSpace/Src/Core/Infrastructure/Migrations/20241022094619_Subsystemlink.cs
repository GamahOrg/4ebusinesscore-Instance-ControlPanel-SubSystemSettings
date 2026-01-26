using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class Subsystemlink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SubsytemSettingsMainClassification_SubsytemMainId",
                table: "SubsytemSettings");

            migrationBuilder.DropIndex(
                name: "IX_SubsytemSettings_SubsytemMainId",
                table: "SubsytemSettings");

            migrationBuilder.DropColumn(
                name: "SubsytemMainId",
                table: "SubsytemSettings");

            migrationBuilder.CreateTable(
                name: "SubsystemSettingsMainClassificationLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubsystemSettingsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubsytemMainId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsystemSettingsMainClassificationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsystemSettingsMainClassificationLinks_SubsytemSettingsMainClassification_SubsytemMainId",
                        column: x => x.SubsytemMainId,
                        principalTable: "SubsytemSettingsMainClassification",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubsystemSettingsMainClassificationLinks_SubsytemSettings_SubsystemSettingsId",
                        column: x => x.SubsystemSettingsId,
                        principalTable: "SubsytemSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubsystemSettingsMainClassificationLinks_SubsystemSettingsId",
                table: "SubsystemSettingsMainClassificationLinks",
                column: "SubsystemSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsystemSettingsMainClassificationLinks_SubsytemMainId",
                table: "SubsystemSettingsMainClassificationLinks",
                column: "SubsytemMainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubsystemSettingsMainClassificationLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "SubsytemMainId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
