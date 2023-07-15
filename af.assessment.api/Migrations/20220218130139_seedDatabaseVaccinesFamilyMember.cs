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
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class seedDatabaseVaccinesFamilyMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Members_MemberId",
                table: "FamilyMembers");

            migrationBuilder.RenameColumn(
                name: "Member",
                table: "FamilyMembers",
                newName: "Relationship");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "FamilyMembers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "FirstName", "IdentificationNumber", "LastName", "MemberId", "Relationship" },
                values: new object[,]
                {
                    { new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"), "Maddy", "98098854081", "Mason", new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"), 2 },
                    { new Guid("77d4f8cc-9797-4f25-9b99-69fde7aeb495"), "Brad", "98334854081", "Mason", new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"), 3 }
                });

            migrationBuilder.InsertData(
                table: "Vaccines",
                columns: new[] { "Id", "AdministeredById", "AdministeredDate", "AppointmentId", "BatchNumber", "ClinicId", "Dose", "FamilyMemberId", "Name", "Site", "VaccineStatus" },
                values: new object[,]
                {
                    { new Guid("fabda97c-eaff-41ca-ae32-0c0a099d2955"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, null, 0.070000000000000007, null, "Haemophilus Influenzae type B", null, false },
                    { new Guid("8ce0c789-f7bd-4a9c-a491-45df5b386657"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, null, 0.12, null, " Rotavirus Gastroenteritis", null, false },
                    { new Guid("e36265ea-0e0d-4a9c-8275-9cac364e75a6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0.0, null, 0.80000000000000004, null, " Tetanus ", null, false }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Members_MemberId",
                table: "FamilyMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Members_MemberId",
                table: "FamilyMembers");

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("77d4f8cc-9797-4f25-9b99-69fde7aeb495"));

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("8ce0c789-f7bd-4a9c-a491-45df5b386657"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("e36265ea-0e0d-4a9c-8275-9cac364e75a6"));

            migrationBuilder.DeleteData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("fabda97c-eaff-41ca-ae32-0c0a099d2955"));

            migrationBuilder.RenameColumn(
                name: "Relationship",
                table: "FamilyMembers",
                newName: "Member");

            migrationBuilder.AlterColumn<Guid>(
                name: "MemberId",
                table: "FamilyMembers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Members_MemberId",
                table: "FamilyMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
