using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class migSubsytemMainLookups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes");

            migrationBuilder.AddColumn<Guid>(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettings",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubsytemMainLookup",
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
                    table.PrimaryKey("PK_SubsytemMainLookup", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_SubsytemMainLookupId",
                table: "SubsytemSettings",
                column: "SubsytemMainLookupId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubsytemSettings_SubsytemMainLookup_SubsytemMainLookupId",
                table: "SubsytemSettings");

            migrationBuilder.DropTable(
                name: "SubsytemMainLookup");

            migrationBuilder.DropIndex(
                name: "IX_SubsytemSettings_SubsytemMainLookupId",
                table: "SubsytemSettings");

            migrationBuilder.DropIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes");

            migrationBuilder.DropColumn(
                name: "SubsytemMainLookupId",
                table: "SubsytemSettings");

            migrationBuilder.CreateIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes",
                columns: new[] { "NameAr", "NameEn" },
                unique: true);
        }
    }
}
