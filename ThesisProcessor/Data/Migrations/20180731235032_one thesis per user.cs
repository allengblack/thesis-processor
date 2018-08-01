using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ThesisProcessor.Data.Migrations
{
    public partial class onethesisperuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theses_AspNetUsers_UploaderId",
                table: "Theses");

            migrationBuilder.DropIndex(
                name: "IX_Theses_UploaderId",
                table: "Theses");

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Theses",
                nullable: true,
                oldClrType: typeof(string));

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

            migrationBuilder.AlterColumn<string>(
                name: "UploaderId",
                table: "Theses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theses_UploaderId",
                table: "Theses",
                column: "UploaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Theses_AspNetUsers_UploaderId",
                table: "Theses",
                column: "UploaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
