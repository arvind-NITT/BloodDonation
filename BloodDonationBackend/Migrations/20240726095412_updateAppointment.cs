using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonationBackend.Migrations
{
    public partial class updateAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "Appointments");
        }
    }
}
