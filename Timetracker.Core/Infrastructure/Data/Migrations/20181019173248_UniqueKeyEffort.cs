using Microsoft.EntityFrameworkCore.Migrations;

namespace Timetracker.Core.Infrastructure.Data.Migrations
{
    public partial class UniqueKeyEffort : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Effort_ProjectId",
                table: "Effort");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Effort",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Effort_ProjectId_UserId_StartDate_EndDate",
                table: "Effort",
                columns: new[] { "ProjectId", "UserId", "StartDate", "EndDate" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Effort_ProjectId_UserId_StartDate_EndDate",
                table: "Effort");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Effort",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Effort_ProjectId",
                table: "Effort",
                column: "ProjectId");
        }
    }
}
