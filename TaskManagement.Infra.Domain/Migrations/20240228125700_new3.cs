using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infra.Domain.Migrations
{
    /// <inheritdoc />
    public partial class new3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "OTP",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OTP_UserId",
                table: "OTP",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTP_Users_UserId",
                table: "OTP");

            migrationBuilder.DropIndex(
                name: "IX_OTP_UserId",
                table: "OTP");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OTP");
        }
    }
}
