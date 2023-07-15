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
    public partial class seedDatabaseVaccines : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("8ce0c789-f7bd-4a9c-a491-45df5b386657"),
                column: "FamilyMemberId",
                value: new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"));

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("e36265ea-0e0d-4a9c-8275-9cac364e75a6"),
                columns: new[] { "FamilyMemberId", "VaccineStatus" },
                values: new object[] { new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"), true });

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("fabda97c-eaff-41ca-ae32-0c0a099d2955"),
                columns: new[] { "FamilyMemberId", "VaccineStatus" },
                values: new object[] { new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"), true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("8ce0c789-f7bd-4a9c-a491-45df5b386657"),
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("e36265ea-0e0d-4a9c-8275-9cac364e75a6"),
                columns: new[] { "FamilyMemberId", "VaccineStatus" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "Vaccines",
                keyColumn: "Id",
                keyValue: new Guid("fabda97c-eaff-41ca-ae32-0c0a099d2955"),
                columns: new[] { "FamilyMemberId", "VaccineStatus" },
                values: new object[] { null, false });
        }
    }
}
