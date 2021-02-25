using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class FixAmenitieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupancy",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "SqFt",
                table: "Amenities",
                newName: "Timming");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Amenities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "Timming",
                table: "Amenities",
                newName: "SqFt");

            migrationBuilder.AddColumn<int>(
                name: "Occupancy",
                table: "Amenities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Amenities",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
