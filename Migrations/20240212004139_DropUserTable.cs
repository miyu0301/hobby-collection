using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HobbyCollection.Migrations
{
    /// <inheritdoc />
    public partial class DropUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTagMappings_TagId",
                table: "FavoriteTagMappings",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteTagMappings_Tag_TagId",
                table: "FavoriteTagMappings",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteTagMappings_Tag_TagId",
                table: "FavoriteTagMappings");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteTagMappings_TagId",
                table: "FavoriteTagMappings");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }
    }
}
