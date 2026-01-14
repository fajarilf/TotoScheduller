using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scheduller.Api.Migrations
{
    /// <inheritdoc />
    public partial class addopnumbercolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "operation_number",
                table: "schedule_detail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "operation_number",
                table: "schedule_detail");
        }
    }
}
