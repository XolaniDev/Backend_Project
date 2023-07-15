/*
 * [2019] - [2021] Eblocks Software (Pty) Ltd, All Rights Reserved.
 * NOTICE: All information contained herein is, and remains the property of Eblocks
 * Software (Pty) Ltd.
 * and its suppliers (if any). The intellectual and technical concepts contained herein
 * are proprietary
 * to Eblocks Software (Pty) Ltd. and its suppliers (if any) and may be covered by South 
 * African, U.S.
 * and Foreign patents, patents in process, and are protected by trade secret and / or 
 * copyright law.
 * Dissemination of this information or reproduction of this material is forbidden unless
 * prior written
 * permission is obtained from Eblocks Software (Pty) Ltd.
*/

#pragma warning disable CS1591

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace af.assessment.api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddDbNavigations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Members_MemberId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDetails_Members_MemberId",
                table: "MedicalDetails");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("acf3f164-3640-4db2-bc8b-a3b4531e0b86"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("2c6a1350-7fae-4b80-8c9f-c18c7f206028"));            

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "MedicalDetails",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Locations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "IdentificationNumber", "MobileNumber", "Name", "OtpPreference", "Password", "ProfilePictureUrl", "Salt" },
                values: new object[] { new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"), "jamie@eblocks.co.za", "0001010000006", "0879998765", "Jamie Dimon", 0, "$2a$11$Gf6Mgp210F8A1n827CrlX.4WRQSB9BnNfOdE6Yu0hWdp6PABDWEKG", "https://dcvcstorage.blob.core.windows.net/profilepics/ben.jpg", "$2a$11$Gf6Mgp210F8A1n827CrlX." });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "MemberId", "PostalCode", "StreetName" },
                values: new object[] { new Guid("dca21998-eeab-4291-aae2-44a7b5e8ff03"), "Johannesburg", new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"), 1220, "21st Street" });

            migrationBuilder.InsertData(
                table: "MedicalDetails",
                columns: new[] { "Id", "MainMemberName", "MainMemberNumber", "MedicalAidName", "MedicalAidNumber", "MemberId" },
                values: new object[] { new Guid("35c1a665-7bb5-43f2-9423-beb9afdda515"), "Jamie Dimon", "0879998765", "Discovery Life", "001334761", new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9") });

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Members_MemberId",
                table: "Locations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDetails_Members_MemberId",
                table: "MedicalDetails",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Members_MemberId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalDetails_Members_MemberId",
                table: "MedicalDetails");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("dca21998-eeab-4291-aae2-44a7b5e8ff03"));

            migrationBuilder.DeleteData(
                table: "MedicalDetails",
                keyColumn: "Id",
                keyValue: new Guid("35c1a665-7bb5-43f2-9423-beb9afdda515"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"));            

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "MedicalDetails",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "Locations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "MemberId", "PostalCode", "StreetName" },
                values: new object[] { new Guid("acf3f164-3640-4db2-bc8b-a3b4531e0b86"), "Johannesburg", null, 1220, "21st Street" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "IdentificationNumber", "MobileNumber", "Name", "OtpPreference", "Password", "ProfilePictureUrl", "Salt" },
                values: new object[] { new Guid("2c6a1350-7fae-4b80-8c9f-c18c7f206028"), "email@address.com", "0001010000006", "0879998765", "Sarah", 0, "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi", null, "$2a$11$66mWNajWltzj.iDOJIjFye" });

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Members_MemberId",
                table: "Locations",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalDetails_Members_MemberId",
                table: "MedicalDetails",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
