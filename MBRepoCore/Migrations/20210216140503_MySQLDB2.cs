using Microsoft.EntityFrameworkCore.Migrations;

namespace MBRepoCore.Migrations
{
    public partial class MySQLDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Branches_BrancheID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branches",
                table: "Branches");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Student");

            migrationBuilder.RenameTable(
                name: "Branches",
                newName: "Branche");

            migrationBuilder.RenameIndex(
                name: "IX_Students_BrancheID",
                table: "Student",
                newName: "IX_Student_BrancheID");

            migrationBuilder.AddColumn<string>(
                name: "BrancheID1",
                table: "Student",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Student",
                table: "Student",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branche",
                table: "Branche",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_BrancheID1",
                table: "Student",
                column: "BrancheID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Branche",
                table: "Student",
                column: "BrancheID",
                principalTable: "Branche",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Branche_BrancheID1",
                table: "Student",
                column: "BrancheID1",
                principalTable: "Branche",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Branche",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Branche_BrancheID1",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Student",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_BrancheID1",
                table: "Student");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Branche",
                table: "Branche");

            migrationBuilder.DropColumn(
                name: "BrancheID1",
                table: "Student");

            migrationBuilder.RenameTable(
                name: "Student",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Branche",
                newName: "Branches");

            migrationBuilder.RenameIndex(
                name: "IX_Student_BrancheID",
                table: "Students",
                newName: "IX_Students_BrancheID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Branches",
                table: "Branches",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Branches_BrancheID",
                table: "Students",
                column: "BrancheID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
