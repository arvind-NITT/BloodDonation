using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationBackend.Migrations
{
    public partial class DCUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DonationCenters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DonationCenters");
        }
    }
}
