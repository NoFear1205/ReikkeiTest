using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_New_Table_Product_And_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product_category", x => new { x.product_id, x.category_id });
                    table.ForeignKey(
                        name: "fk_product_category_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_category_products_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_category_category_id",
                table: "product_category",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product_category");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "product");
        }
    }
}
