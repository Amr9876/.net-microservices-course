using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressLine", "CVV", "CardName", "CardNumber", "Country", "CreatedBy", "CreatedDate", "EmailAddress", "Expiration", "FirstName", "LastModifiedBy", "LastModifiedDate", "LastName", "PaymentMethod", "State", "TotalPrice", "UserName", "ZipCode" },
                values: new object[] { 1, "Bahcelievler", null, null, null, "Turkey", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "amr.aig.2007@gmail.com", null, "John", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Doe", 0, null, 350m, "John Doe", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
