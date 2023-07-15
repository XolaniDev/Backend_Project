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

using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class EditMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalDetails_MemberId",
                table: "MedicalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Locations_MemberId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDetails_MemberId",
                table: "MedicalDetails",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MemberId",
                table: "Locations",
                column: "MemberId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalDetails_MemberId",
                table: "MedicalDetails");

            migrationBuilder.DropIndex(
                name: "IX_Locations_MemberId",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalDetails_MemberId",
                table: "MedicalDetails",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MemberId",
                table: "Locations",
                column: "MemberId");
        }
    }
}
