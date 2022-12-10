using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSharingApp.Migrations
{
    public partial class addUploadUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Uploads",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Uploads");
        }
    }
}
