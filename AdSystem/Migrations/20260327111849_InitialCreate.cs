using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AdSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_advertisers",
                columns: table => new
                {
                    advertiser_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    advertiser_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    subscription_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    organisation_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    invoice_address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    invoice_postal_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    invoice_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_advertisers", x => x.advertiser_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ads",
                columns: table => new
                {
                    ad_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    advertiser_id = table.Column<int>(type: "integer", nullable: false),
                    ad_title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ad_content = table.Column<string>(type: "text", nullable: false),
                    ad_itemprice = table.Column<decimal>(type: "numeric", nullable: false),
                    ad_adprice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ads", x => x.ad_id);
                    table.ForeignKey(
                        name: "FK_tbl_ads_tbl_advertisers_advertiser_id",
                        column: x => x.advertiser_id,
                        principalTable: "tbl_advertisers",
                        principalColumn: "advertiser_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ads_advertiser_id",
                table: "tbl_ads",
                column: "advertiser_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_advertisers_organisation_number",
                table: "tbl_advertisers",
                column: "organisation_number",
                unique: true,
                filter: "organisation_number IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_advertisers_subscription_number",
                table: "tbl_advertisers",
                column: "subscription_number",
                unique: true,
                filter: "subscription_number IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_ads");

            migrationBuilder.DropTable(
                name: "tbl_advertisers");
        }
    }
}
