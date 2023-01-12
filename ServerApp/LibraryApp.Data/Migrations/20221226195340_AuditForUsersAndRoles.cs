using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Data.Migrations
{
    public partial class AuditForUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedRoleId",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedRoleId",
                schema: "dbo",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedBy",
                schema: "dbo",
                table: "AspNetRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedRoleId",
                schema: "dbo",
                table: "AspNetRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "dbo",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedBy",
                schema: "dbo",
                table: "AspNetRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "dbo",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedRoleId",
                schema: "dbo",
                table: "AspNetRoles",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedRoleId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedRoleId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreatedRoleId",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedRoleId",
                schema: "dbo",
                table: "AspNetRoles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
