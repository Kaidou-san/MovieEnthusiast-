using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace movieDemo.Migrations
{
    public partial class SixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatsOfMovies",
                table: "CatsOfMovies");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "CatsOfMovies");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CatsOfMovies");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CatsOfMovies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CatsOfMovies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "CatsOfMovies",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "CatsOfMovies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatsOfMovies",
                table: "CatsOfMovies",
                column: "GenreId");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatsOfMovies_CategoryId",
                table: "CatsOfMovies",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CatsOfMovies_MovieId",
                table: "CatsOfMovies",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_CatsOfMovies_Categories_CategoryId",
                table: "CatsOfMovies",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatsOfMovies_Movies_MovieId",
                table: "CatsOfMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatsOfMovies_Categories_CategoryId",
                table: "CatsOfMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_CatsOfMovies_Movies_MovieId",
                table: "CatsOfMovies");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatsOfMovies",
                table: "CatsOfMovies");

            migrationBuilder.DropIndex(
                name: "IX_CatsOfMovies_CategoryId",
                table: "CatsOfMovies");

            migrationBuilder.DropIndex(
                name: "IX_CatsOfMovies_MovieId",
                table: "CatsOfMovies");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "CatsOfMovies");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "CatsOfMovies");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CatsOfMovies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "CatsOfMovies",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CatsOfMovies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CatsOfMovies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatsOfMovies",
                table: "CatsOfMovies",
                column: "CategoryId");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                    table.ForeignKey(
                        name: "FK_Genres_CatsOfMovies_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CatsOfMovies",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Genres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_CategoryId",
                table: "Genres",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_MovieId",
                table: "Genres",
                column: "MovieId");
        }
    }
}
