using Microsoft.EntityFrameworkCore.Migrations;

namespace testapp.Migrations
{
    public partial class RelationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Students",
                newName: "GroupForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupId",
                table: "Students",
                newName: "IX_Students_GroupForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupForeignKey",
                table: "Students",
                column: "GroupForeignKey",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Groups_GroupForeignKey",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "GroupForeignKey",
                table: "Students",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_GroupForeignKey",
                table: "Students",
                newName: "IX_Students_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Groups_GroupId",
                table: "Students",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
