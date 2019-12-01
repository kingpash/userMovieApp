using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace usermovieApp.Migrations
{
    public partial class AzMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Movie",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Movie",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
