using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Softmad.Services.LeadGeneration.Data.migrations
{
    /// <inheritdoc />
    public partial class NewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "HospitalDetails",
                newName: "PurchaseHeadPhone");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Budget",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Leads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Leads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PinCode",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "BioMedicalName",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BioMedicalPhone",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerPhone",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PurchaseHeadName",
                table: "HospitalDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerType",
                table: "CustomerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "LastUpdated",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "BioMedicalName",
                table: "HospitalDetails");

            migrationBuilder.DropColumn(
                name: "BioMedicalPhone",
                table: "HospitalDetails");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "HospitalDetails");

            migrationBuilder.DropColumn(
                name: "OwnerPhone",
                table: "HospitalDetails");

            migrationBuilder.DropColumn(
                name: "PurchaseHeadName",
                table: "HospitalDetails");

            migrationBuilder.DropColumn(
                name: "CustomerType",
                table: "CustomerDetails");

            migrationBuilder.RenameColumn(
                name: "PurchaseHeadPhone",
                table: "HospitalDetails",
                newName: "Phone");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Budget",
                table: "Leads",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PinCode",
                table: "HospitalDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
