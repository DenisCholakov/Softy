using Microsoft.EntityFrameworkCore.Migrations;

namespace Quiz.Data.Migrations
{
    public partial class UserAnswerAddFKforQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "UserAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId1",
                table: "UserAnswers",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId1",
                table: "UserAnswers",
                column: "QuestionId1",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Questions_QuestionId1",
                table: "UserAnswers");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswers_QuestionId1",
                table: "UserAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "UserAnswers");
        }
    }
}
