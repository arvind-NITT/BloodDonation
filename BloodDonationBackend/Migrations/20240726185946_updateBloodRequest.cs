using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationBackend.Migrations
{
    public partial class updateBloodRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "BloodRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BloodRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "BloodRequests");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BloodRequests");
        }
    }
}
