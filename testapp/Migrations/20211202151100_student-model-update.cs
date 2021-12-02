using Microsoft.EntityFrameworkCore.Migrations;

namespace testapp.Migrations
{
    public partial class studentmodelupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgPath",
                table: "Students",
                newName: "ImgFile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgFile",
                table: "Students",
                newName: "ImgPath");
        }
    }
}
