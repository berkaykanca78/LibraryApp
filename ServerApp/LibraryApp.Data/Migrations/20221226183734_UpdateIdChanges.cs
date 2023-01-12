using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Data.Migrations
{
    public partial class UpdateIdChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookAuthorId",
                schema: "dbo",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                schema: "dbo",
                table: "BookAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                schema: "dbo",
                table: "BookAuthors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookAuthorId",
                schema: "dbo",
                table: "Authors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Authors",
                column: "BookAuthorId",
                principalSchema: "dbo",
                principalTable: "BookAuthors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Books",
                column: "BookAuthorId",
                principalSchema: "dbo",
                principalTable: "BookAuthors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "BookAuthorId",
                schema: "dbo",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                schema: "dbo",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                schema: "dbo",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookAuthorId",
                schema: "dbo",
                table: "Authors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Authors",
                column: "BookAuthorId",
                principalSchema: "dbo",
                principalTable: "BookAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookAuthors_BookAuthorId",
                schema: "dbo",
                table: "Books",
                column: "BookAuthorId",
                principalSchema: "dbo",
                principalTable: "BookAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
