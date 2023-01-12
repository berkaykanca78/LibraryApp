using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Data.Migrations
{
    public partial class AddIdIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavouritesAuthor_FavouritesAuthorID",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_FavouritesAuthor_FavouritesAuthorID",
                schema: "dbo",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Publishers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "FavouritesAuthor",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Books",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "BookAuthors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FavouritesAuthorID",
                schema: "dbo",
                table: "Authors",
                newName: "FavouritesAuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Authors",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_FavouritesAuthorID",
                schema: "dbo",
                table: "Authors",
                newName: "IX_Authors_FavouritesAuthorId");

            migrationBuilder.RenameColumn(
                name: "FavouritesAuthorID",
                schema: "dbo",
                table: "AspNetUsers",
                newName: "FavouritesAuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FavouritesAuthorID",
                schema: "dbo",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FavouritesAuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                schema: "dbo",
                table: "Address",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavouritesAuthor_FavouritesAuthorId",
                schema: "dbo",
                table: "AspNetUsers",
                column: "FavouritesAuthorId",
                principalSchema: "dbo",
                principalTable: "FavouritesAuthor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_FavouritesAuthor_FavouritesAuthorId",
                schema: "dbo",
                table: "Authors",
                column: "FavouritesAuthorId",
                principalSchema: "dbo",
                principalTable: "FavouritesAuthor",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavouritesAuthor_FavouritesAuthorId",
                schema: "dbo",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_FavouritesAuthor_FavouritesAuthorId",
                schema: "dbo",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Publishers",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "FavouritesAuthor",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Categories",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Books",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "BookAuthors",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "FavouritesAuthorId",
                schema: "dbo",
                table: "Authors",
                newName: "FavouritesAuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Authors",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_FavouritesAuthorId",
                schema: "dbo",
                table: "Authors",
                newName: "IX_Authors_FavouritesAuthorID");

            migrationBuilder.RenameColumn(
                name: "FavouritesAuthorId",
                schema: "dbo",
                table: "AspNetUsers",
                newName: "FavouritesAuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_FavouritesAuthorId",
                schema: "dbo",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_FavouritesAuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Address",
                newName: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavouritesAuthor_FavouritesAuthorID",
                schema: "dbo",
                table: "AspNetUsers",
                column: "FavouritesAuthorID",
                principalSchema: "dbo",
                principalTable: "FavouritesAuthor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_FavouritesAuthor_FavouritesAuthorID",
                schema: "dbo",
                table: "Authors",
                column: "FavouritesAuthorID",
                principalSchema: "dbo",
                principalTable: "FavouritesAuthor",
                principalColumn: "ID");
        }
    }
}
