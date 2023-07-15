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
    public partial class editSeedDataToCorrectFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("77d4f8cc-9797-4f25-9b99-69fde7aeb495"),
                column: "IdentificationNumber",
                value: "2109305234087");

            migrationBuilder.UpdateData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"),
                column: "IdentificationNumber",
                value: "2109302345084");

            migrationBuilder.UpdateData(
                table: "MedicalDetails",
                keyColumn: "Id",
                keyValue: new Guid("35c1a665-7bb5-43f2-9423-beb9afdda515"),
                column: "MainMemberNumber",
                value: "(087)-999-8765");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"),
                column: "MobileNumber",
                value: "(087)-999-8765");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("77d4f8cc-9797-4f25-9b99-69fde7aeb495"),
                column: "IdentificationNumber",
                value: "98334854081");

            migrationBuilder.UpdateData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: new Guid("8c907bc1-8c37-4c71-95e3-c35519280062"),
                column: "IdentificationNumber",
                value: "98098854081");

            migrationBuilder.UpdateData(
                table: "MedicalDetails",
                keyColumn: "Id",
                keyValue: new Guid("35c1a665-7bb5-43f2-9423-beb9afdda515"),
                column: "MainMemberNumber",
                value: "0879998765");

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9"),
                column: "MobileNumber",
                value: "0879998765");
        }
    }
}
