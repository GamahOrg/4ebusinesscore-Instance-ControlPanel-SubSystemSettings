using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _4EPlatForm.Infrastrucure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_NoteTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_NoteTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubsytemSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteNameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteNameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkSpaceKey = table.Column<long>(type: "bigint", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoteTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OrganizationHierarchyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsytemSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubsytemSettings_LK_NoteTypes_NoteTypeId",
                        column: x => x.NoteTypeId,
                        principalTable: "LK_NoteTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubsytemSettings_SubsytemSettings_ParentId",
                        column: x => x.ParentId,
                        principalTable: "SubsytemSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LK_NoteTypes_NameAr_NameEn",
                table: "LK_NoteTypes",
                columns: new[] { "NameAr", "NameEn" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_NoteTypeId",
                table: "SubsytemSettings",
                column: "NoteTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_ParentId",
                table: "SubsytemSettings",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubsytemSettings_WorkSpaceKey",
                table: "SubsytemSettings",
                column: "WorkSpaceKey",
                unique: true,
                filter: "[WorkSpaceKey] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubsytemSettings");

            migrationBuilder.DropTable(
                name: "LK_NoteTypes");
        }
    }
}
