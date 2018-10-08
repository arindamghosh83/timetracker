using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Timetracker.Core.Infrastructure.Data.Migrations
{
    public partial class SchemaUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effort_Person_PersonForeignKey",
                table: "Effort");

            migrationBuilder.DropForeignKey(
                name: "FK_Effort_Project_ProjectForeignKey",
                table: "Effort");

            migrationBuilder.DropIndex(
                name: "IX_Effort_PersonForeignKey",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "PersonForeignKey",
                table: "Effort");

            migrationBuilder.RenameColumn(
                name: "ProjectForeignKey",
                table: "Effort",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Effort",
                newName: "UpdatedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Effort_ProjectForeignKey",
                table: "Effort",
                newName: "IX_Effort_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Project",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Project",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Effort",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Effort",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Effort",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Effort",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Effort",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Effort",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Effort_Project_ProjectId",
                table: "Effort",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Effort_Project_ProjectId",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Effort");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Effort");

            migrationBuilder.RenameColumn(
                name: "UpdatedOn",
                table: "Effort",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Effort",
                newName: "ProjectForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Effort_ProjectId",
                table: "Effort",
                newName: "IX_Effort_ProjectForeignKey");

            migrationBuilder.AddColumn<int>(
                name: "PersonForeignKey",
                table: "Effort",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Effort_PersonForeignKey",
                table: "Effort",
                column: "PersonForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Effort_Person_PersonForeignKey",
                table: "Effort",
                column: "PersonForeignKey",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Effort_Project_ProjectForeignKey",
                table: "Effort",
                column: "ProjectForeignKey",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
