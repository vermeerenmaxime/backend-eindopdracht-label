using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Label.API.Migrations
{
    public partial class albumsongs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AlbumSongs",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumSongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumSongs", x => new { x.AlbumId, x.SongId });
                    table.ForeignKey(
                        name: "FK_AlbumSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "ArtistId", "ArtistName", "Birthdate", "Country", "Email", "FirstName", "HouseNumber", "LastName", "PhoneNumber", "PostalCode", "StreetName" },
                values: new object[] { new Guid("ef52121a-dffb-4bc9-a983-9ff59b90962a"), "Mave", "08/08/2001", "Belgium", "maxime6128@gmail.com", "Maxime", "175", "Vermeeren", "+32470053774", "9700", "Deinzestraat" });

            migrationBuilder.InsertData(
                table: "Recordlabels",
                columns: new[] { "RecordLabelId", "Country", "LabelName" },
                values: new object[] { new Guid("39b0cb5f-ec33-4a0a-ad3c-08b16d81a869"), "Belgium", "Loud Memory Records" });

            migrationBuilder.InsertData(
                table: "Recordlabels",
                columns: new[] { "RecordLabelId", "Country", "LabelName" },
                values: new object[] { new Guid("6c5db577-8683-4039-b964-c34b19c4a4d6"), "Belgium", "Deep Memory" });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumSongs_SongId",
                table: "AlbumSongs",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums");

            migrationBuilder.DropTable(
                name: "AlbumSongs");

            migrationBuilder.DeleteData(
                table: "Artists",
                keyColumn: "ArtistId",
                keyValue: new Guid("ef52121a-dffb-4bc9-a983-9ff59b90962a"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("39b0cb5f-ec33-4a0a-ad3c-08b16d81a869"));

            migrationBuilder.DeleteData(
                table: "Recordlabels",
                keyColumn: "RecordLabelId",
                keyValue: new Guid("6c5db577-8683-4039-b964-c34b19c4a4d6"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ArtistId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Artists_ArtistId",
                table: "Albums",
                column: "ArtistId",
                principalTable: "Artists",
                principalColumn: "ArtistId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
