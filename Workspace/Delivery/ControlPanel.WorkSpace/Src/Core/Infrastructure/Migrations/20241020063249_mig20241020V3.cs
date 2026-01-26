using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class mig20241020V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettingsMainClassification_SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification",
                column: "SubsytemMainLookupId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubsytemSettingsMainClassification_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification",
                column: "SubsytemMainLookupId",
                principalTable: "SubsytemMainLookup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettingsMainClassification_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification");

            migrationBuilder.DropIndex(
                name: "IX_SubsytemSettingsMainClassification_SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification");

            migrationBuilder.DropColumn(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettingsMainClassification");
        }
    }
}
