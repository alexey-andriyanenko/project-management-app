using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Board.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardColumnFkToTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardColumnId",
                table: "Tasks",
                column: "BoardColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_BoardColumns_BoardColumnId",
                table: "Tasks",
                column: "BoardColumnId",
                principalTable: "BoardColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_BoardColumns_BoardColumnId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_BoardColumnId",
                table: "Tasks");
        }
    }
}
