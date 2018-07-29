using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThesisProcessor.Data.Migrations
{
    public partial class AddAuthorproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_AspNetUsers_AuthorId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_AuthorId",
                table: "Theses");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Theses",
                newName: "UploaderId");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Theses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_UploaderId",
                table: "Theses",
                column: "UploaderId",
                unique: true,
                filter: "[UploaderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_AspNetUsers_UploaderId",
                table: "Theses",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_AspNetUsers_UploaderId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_UploaderId",
                table: "Theses");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Theses");

            migrationBuilder.RenameColumn(
                name: "UploaderId",
                table: "Theses",
                newName: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Theses_AuthorId",
                table: "Theses",
                column: "AuthorId",
                unique: true,
                filter: "[AuthorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_AspNetUsers_AuthorId",
                table: "Theses",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
