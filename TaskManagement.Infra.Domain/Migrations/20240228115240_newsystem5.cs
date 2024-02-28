using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infra.Domain.Migrations
{
    /// <inheritdoc />
    public partial class newsystem5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Tasks_TaskId1",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_TaskId1",
                table: "History");

            migrationBuilder.DropColumn(
                name: "TaskId1",
                table: "History");

            migrationBuilder.AddColumn<long>(
                name: "TaskId",
                table: "History",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_History_TaskId",
                table: "History",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tasks_TaskId",
                table: "History",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Tasks_TaskId",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_TaskId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "History");

            migrationBuilder.AddColumn<long>(
                name: "TaskId1",
                table: "History",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_TaskId1",
                table: "History",
                column: "TaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tasks_TaskId1",
                table: "History",
                column: "TaskId1",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }
    }
}
