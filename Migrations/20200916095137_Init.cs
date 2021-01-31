using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace aspcore_watchshop.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    SeoImage = table.Column<string>(maxLength: 50, nullable: true),
                    SeoTitle = table.Column<string>(maxLength: 150, nullable: true),
                    SeoDescription = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Cost = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Infos",
                columns: table => new
                {
                    Logo = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Facebook = table.Column<string>(maxLength: 100, nullable: true),
                    Instargram = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 150, nullable: true),
                    WorkTime = table.Column<string>(maxLength: 50, nullable: true),
                    SeoImage = table.Column<string>(maxLength: 50, nullable: true),
                    SeoTitle = table.Column<string>(maxLength: 250, nullable: true),
                    SeoDescritption = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    CustomerName = table.Column<string>(maxLength: 40, nullable: true),
                    CustomerPhone = table.Column<string>(maxLength: 50, nullable: true),
                    CustomerProvince = table.Column<string>(maxLength: 25, nullable: true),
                    CustomerAddress = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerNote = table.Column<string>(maxLength: 250, nullable: true),
                    Promotion = table.Column<string>(maxLength: 50, nullable: true),
                    Fee = table.Column<string>(maxLength: 50, nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    PolicyContent = table.Column<string>(maxLength: 150, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostContent = table.Column<string>(nullable: true),
                    SeoImage = table.Column<string>(maxLength: 50, nullable: true),
                    SeoTitle = table.Column<string>(maxLength: 250, nullable: true),
                    SeoDescritption = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    FromDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Status = table.Column<bool>(nullable: true, defaultValue: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeWires",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeWires", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromBills",
                columns: table => new
                {
                    PromotionID = table.Column<int>(nullable: false),
                    Discount = table.Column<string>(maxLength: 50, nullable: true),
                    isFeeShip = table.Column<bool>(nullable: false, defaultValue: false),
                    ConditionItem = table.Column<byte>(nullable: false),
                    ConditionAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromBills", x => x.PromotionID);
                    table.ForeignKey(
                        name: "FK_PromBills_Promotions_PromotionID",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromProducts",
                columns: table => new
                {
                    PromotionID = table.Column<int>(nullable: false),
                    Discount = table.Column<string>(maxLength: 50, nullable: true),
                    ProductIDs = table.Column<string>(maxLength: 250, nullable: true),
                    CategoryID = table.Column<int>(nullable: true),
                    BandID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromProducts", x => x.PromotionID);
                    table.ForeignKey(
                        name: "FK_PromProducts_Promotions_PromotionID",
                        column: x => x.PromotionID,
                        principalTable: "Promotions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    isShow = table.Column<bool>(nullable: true, defaultValue: true),
                    isDel = table.Column<bool>(nullable: true, defaultValue: false),
                    Price = table.Column<int>(nullable: false),
                    SaleCount = table.Column<int>(nullable: false, defaultValue: 0),
                    Image = table.Column<string>(maxLength: 50, nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    TypeWireID = table.Column<int>(nullable: false),
                    BandID = table.Column<int>(nullable: false),
                    PostProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Bands_BandID",
                        column: x => x.BandID,
                        principalTable: "Bands",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Posts_PostProductID",
                        column: x => x.PostProductID,
                        principalTable: "Posts",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_TypeWires_TypeWireID",
                        column: x => x.TypeWireID,
                        principalTable: "TypeWires",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<byte>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Discount = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    Images = table.Column<string>(nullable: true),
                    TypeGlass = table.Column<string>(maxLength: 30, nullable: true),
                    TypeBorder = table.Column<string>(maxLength: 30, nullable: true),
                    TypeMachine = table.Column<string>(maxLength: 30, nullable: true),
                    Parameter = table.Column<string>(maxLength: 30, nullable: true),
                    ResistWater = table.Column<string>(maxLength: 30, nullable: true),
                    Warranty = table.Column<string>(maxLength: 30, nullable: true),
                    Origin = table.Column<string>(maxLength: 30, nullable: true),
                    Color = table.Column<string>(maxLength: 30, nullable: true),
                    Func = table.Column<string>(maxLength: 30, nullable: true),
                    DescriptionProduct = table.Column<string>(maxLength: 1500, nullable: true),
                    SeoImage = table.Column<string>(maxLength: 50, nullable: true),
                    SeoTitle = table.Column<string>(maxLength: 250, nullable: true),
                    SeoDescritption = table.Column<string>(maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BandID",
                table: "Products",
                column: "BandID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PostProductID",
                table: "Products",
                column: "PostProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_TypeWireID",
                table: "Products",
                column: "TypeWireID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Infos");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "PromBills");

            migrationBuilder.DropTable(
                name: "PromProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Bands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "TypeWires");
        }
    }
}
