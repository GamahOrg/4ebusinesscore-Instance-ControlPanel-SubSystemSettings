using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class mig454ss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettings");

            migrationBuilder.DropIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubsytemSettingsMainClassification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "SubsytemSettingsMainClassification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes",
                columns: new[] { "NameAr", "NameEn" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubsytemSettings_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettings",
                column: "SubsytemMainLookupId",
                principalTable: "SubsytemMainLookup",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettings");

            migrationBuilder.DropIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubsytemSettingsMainClassification");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "SubsytemSettingsMainClassification");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes",
                columns: new[] { "NameAr", "NameEn" });

            migrationBuilder.AddForeignKey(
                name: "FK_SubsytemSettings_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettings",
                column: "SubsytemMainLookupId",
                principalTable: "SubsytemMainLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
