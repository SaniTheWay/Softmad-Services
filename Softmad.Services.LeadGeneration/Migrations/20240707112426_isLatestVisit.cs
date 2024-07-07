using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Softmad.Services.LeadGeneration.Migrations
{
    /// <inheritdoc />
    public partial class isLatestVisit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLatestVisit",
                table: "Visits",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLatestVisit",
                table: "Visits");
        }
    }
}
