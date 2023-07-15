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

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace af.assessment.api.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class VaccineHealthcareNavigation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredBy",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "AdministeredBy",
                table: "Vaccines",
                newName: "AdministeredById");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccines_AdministeredBy",
                table: "Vaccines",
                newName: "IX_Vaccines_AdministeredById");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredById",
                table: "Vaccines",
                column: "AdministeredById",
                principalTable: "HealthcareProfessionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredById",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "AdministeredById",
                table: "Vaccines",
                newName: "AdministeredBy");

            migrationBuilder.RenameIndex(
                name: "IX_Vaccines_AdministeredById",
                table: "Vaccines",
                newName: "IX_Vaccines_AdministeredBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredBy",
                table: "Vaccines",
                column: "AdministeredBy",
                principalTable: "HealthcareProfessionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
