using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Softmad.Services.LeadGeneration.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalDetailstovisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "NextSteps",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ClosurePlan",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Competitor",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CompetitorModel",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CompetitorName",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ExistingMachines",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Leads");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Visits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientFeedback",
                table: "Visits",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientFeedback",
                table: "Visits",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastUpdated",
                table: "Visits",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NextSteps",
                table: "Visits",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Budget",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClosurePlan",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Competitor",
                table: "Leads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CompetitorModel",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompetitorName",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExistingMachines",
                table: "Leads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Leads",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
