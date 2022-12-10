using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSharingApp.Migrations
{
    public partial class addUploadedDateTimeforUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedDateTime",
                table: "Uploads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedDateTime",
                table: "Uploads");
        }
    }
}
