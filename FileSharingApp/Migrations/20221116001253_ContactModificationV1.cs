using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSharingApp.Migrations
{
    public partial class ContactModificationV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "Contacts");
        }
    }
}
