using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Label.API.Migrations
{
    public partial class dto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: new Guid("5cd47252-058b-403d-aa72-24d3a181de69"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("e468ac2a-0710-467b-aae1-5cba36db7b54"));

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "ArtistName", "Birthdate", "Country", "Email", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StreetName" },
                values: new object[] { new Guid("ecf4fa1b-1595-4d47-a9e6-6ddf9d4bcdc3"), "Mave", "08/08/2001", "Belgium", "maxime6128@gmail.com", "Maxime", "175", "Vermeeren", "+32470053774", "9700", "Deinzestraat" });

            migrationBuilder.InsertData(
                table: "Recordlabels",
                columns: new[] { "RecordLabelId", "Country", "LabelName" },
                values: new object[,]
                {
                    { new Guid("94419947-98ed-4630-ac5d-17d47ab26547"), "Belgium", "Loud Memory Records" },
                    { new Guid("a95252fd-eb92-4008-b8c6-9632bf9a578c"), "Belgium", "Loud Memory" },
                    { new Guid("15196839-9d14-4eb1-a52c-679e7de6d9dd"), "Belgium", "Deep Memory" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: new Guid("ecf4fa1b-1595-4d47-a9e6-6ddf9d4bcdc3"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("15196839-9d14-4eb1-a52c-679e7de6d9dd"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("94419947-98ed-4630-ac5d-17d47ab26547"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("a95252fd-eb92-4008-b8c6-9632bf9a578c"));

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "ArtistName", "Birthdate", "Country", "Email", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StreetName" },
                values: new object[] { new Guid("5cd47252-058b-403d-aa72-24d3a181de69"), "Mave", "08/08/2001", "Belgium", "maxime6128@gmail.com", "Maxime", "175", "Vermeeren", "+32470053774", "9700", "Deinzestraat" });

            migrationBuilder.InsertData(
                table: "Recordlabels",
                columns: new[] { "RecordLabelId", "Country", "LabelName" },
                values: new object[] { new Guid("e468ac2a-0710-467b-aae1-5cba36db7b54"), "Belgium", "Loud Memory Records" });
        }
    }
}
