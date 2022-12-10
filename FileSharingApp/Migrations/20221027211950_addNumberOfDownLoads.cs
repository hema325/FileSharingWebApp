using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSharingApp.Migrations
{
    public partial class addNumberOfDownLoads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "NumberOfDownLoads",
                table: "Uploads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDownLoads",
                table: "Uploads");
        }
    }
}
