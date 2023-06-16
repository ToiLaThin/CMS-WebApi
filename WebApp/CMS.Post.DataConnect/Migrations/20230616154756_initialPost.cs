using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CMS.Post.DataConnect.Migrations
{
    /// <inheritdoc />
    public partial class initialPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CategorySchema");

            migrationBuilder.EnsureSchema(
                name: "PostSchema");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "CategorySchema",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "0, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CategoryCreateBy = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "PostSchema",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "0, 1"),
                    PostTitle = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PostImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostStatus = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    PostCreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    PostCreateBy = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)0),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Post_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "CategorySchema",
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "CategorySchema",
                table: "Category",
                columns: new[] { "CategoryId", "CategoryDescription", "ModifiedBy", "ModifiedDate", "CategoryName" },
                values: new object[,]
                {
                    { -2, "Physical Activitis and all kind of outdoor activities", null, null, "Sport" },
                    { -1, "Unknown", null, null, "Default" }
                });

            migrationBuilder.InsertData(
                schema: "PostSchema",
                table: "Post",
                columns: new[] { "PostId", "CategoryId", "PostContent", "PostImageUrl", "ModifiedBy", "ModifiedDate", "PostStatus", "PostTitle" },
                values: new object[,]
                {
                    { -3, -1, "Content 3", "None", null, null, 0, "Title 3" },
                    { -2, -2, "Content 2", "None", null, null, 0, "Title 2" },
                    { -1, -1, "Content 1", "None", null, null, 0, "Title 1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Post_CategoryId",
                schema: "PostSchema",
                table: "Post",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post",
                schema: "PostSchema");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "CategorySchema");
        }
    }
}
