using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyULibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    IdGenre = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.IdGenre);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    IdRol = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RolName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.IdRol);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Idbook = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Author = table.Column<string>(maxLength: 100, nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    PublishedYear = table.Column<DateTime>(nullable: false),
                    IdGenre = table.Column<int>(nullable: false),
                    GenreIdGenre = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Idbook);
                    table.ForeignKey(
                        name: "FK_Book_Genre_GenreIdGenre",
                        column: x => x.GenreIdGenre,
                        principalTable: "Genre",
                        principalColumn: "IdGenre",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    IdPerson = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    IdRol = table.Column<int>(nullable: false),
                    RolIdRol = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.IdPerson);
                    table.ForeignKey(
                        name: "FK_Person_Rol_RolIdRol",
                        column: x => x.RolIdRol,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookRegistry",
                columns: table => new
                {
                    IdBookRegistry = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCheckout = table.Column<DateTime>(nullable: false),
                    DateReturn = table.Column<DateTime>(nullable: false),
                    Returned = table.Column<bool>(nullable: false),
                    IdBook = table.Column<int>(nullable: false),
                    BookIdbook = table.Column<int>(nullable: true),
                    IdStudent = table.Column<int>(nullable: false),
                    StudentIdPerson = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRegistry", x => x.IdBookRegistry);
                    table.ForeignKey(
                        name: "FK_BookRegistry_Book_BookIdbook",
                        column: x => x.BookIdbook,
                        principalTable: "Book",
                        principalColumn: "Idbook",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookRegistry_Person_StudentIdPerson",
                        column: x => x.StudentIdPerson,
                        principalTable: "Person",
                        principalColumn: "IdPerson",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreIdGenre",
                table: "Book",
                column: "GenreIdGenre");

            migrationBuilder.CreateIndex(
                name: "IX_BookRegistry_BookIdbook",
                table: "BookRegistry",
                column: "BookIdbook");

            migrationBuilder.CreateIndex(
                name: "IX_BookRegistry_StudentIdPerson",
                table: "BookRegistry",
                column: "StudentIdPerson");

            migrationBuilder.CreateIndex(
                name: "IX_Person_RolIdRol",
                table: "Person",
                column: "RolIdRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRegistry");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
