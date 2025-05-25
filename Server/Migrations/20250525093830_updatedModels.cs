using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class updatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doctor_Specialization",
                table: "AppointmentDTOs");

            migrationBuilder.AddColumn<string>(
                name: "FacebookAccountLink",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramAccountLink",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinAccountLink",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XAccountLink",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "AppointmentDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookAccountLink",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "InstagramAccountLink",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "LinkedinAccountLink",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "XAccountLink",
                table: "Doctors");

            migrationBuilder.AlterColumn<string>(
                name: "PatientId",
                table: "AppointmentDTOs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Doctor_Specialization",
                table: "AppointmentDTOs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
