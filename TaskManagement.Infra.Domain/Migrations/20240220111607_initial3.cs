using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infra.Domain.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Tasks_TaskId",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "History",
                newName: "TaskId1");

            migrationBuilder.RenameIndex(
                name: "IX_History_TaskId",
                table: "History",
                newName: "IX_History_TaskId1");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tasks_TaskId1",
                table: "History",
                column: "TaskId1",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Tasks_TaskId1",
                table: "History");

            migrationBuilder.RenameColumn(
                name: "TaskId1",
                table: "History",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_History_TaskId1",
                table: "History",
                newName: "IX_History_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Tasks_TaskId",
                table: "History",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");
        }
    }
}
