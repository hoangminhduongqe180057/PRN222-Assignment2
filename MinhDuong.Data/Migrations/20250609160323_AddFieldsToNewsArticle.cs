using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinhDuong.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToNewsArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Accounts_AccountId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags");

            migrationBuilder.AddColumn<string>(
                name: "Headline",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "NewsArticles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewsSource",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "NewsArticles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_UpdatedById",
                table: "NewsArticles",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Accounts_AccountId",
                table: "NewsArticles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Accounts_UpdatedById",
                table: "NewsArticles",
                column: "UpdatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Accounts_AccountId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Accounts_UpdatedById",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles");

            migrationBuilder.DropForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags");

            migrationBuilder.DropIndex(
                name: "IX_NewsArticles_UpdatedById",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "Headline",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "NewsSource",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "NewsArticles");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Accounts_AccountId",
                table: "NewsArticles",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsArticles_Categories_CategoryId",
                table: "NewsArticles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsTags_Tags_TagId",
                table: "NewsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
