using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infra.Domain.Migrations
{
    /// <inheritdoc />
    public partial class new4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OTP",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_OTP_UserId",
                table: "OTP",
                newName: "IX_OTP_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTP_Users_userId",
                table: "OTP",
                column: "userId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTP_Users_userId",
                table: "OTP");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "OTP",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OTP_userId",
                table: "OTP",
                newName: "IX_OTP_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
