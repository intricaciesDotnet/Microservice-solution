using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace user.api.Migrations
{
    /// <inheritdoc />
    public partial class Update_fk_key_for_payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Users_Id",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "PaymentMethods",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_UserId",
                table: "PaymentMethods",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_UserId",
                table: "PaymentMethods",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethods_Users_UserId",
                table: "PaymentMethods");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_UserId",
                table: "PaymentMethods");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PaymentMethods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethods_Users_Id",
                table: "PaymentMethods",
                column: "Id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
