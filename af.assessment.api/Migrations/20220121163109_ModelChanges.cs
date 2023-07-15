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
    public partial class ModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_FamilyMembers_AppointmentsId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "VaccineStatus",
                table: "FamilyMembers");

            migrationBuilder.RenameColumn(
                name: "AppointmentsId",
                table: "Appointments",
                newName: "FamilyMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_AppointmentsId",
                table: "Appointments",
                newName: "IX_Appointments_FamilyMemberId");

            migrationBuilder.AddColumn<Guid>(
                name: "AdministeredBy",
                table: "Vaccines",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AdministeredDate",
                table: "Vaccines",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "BatchNumber",
                table: "Vaccines",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "FamilyMemberId",
                table: "Vaccines",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Site",
                table: "Vaccines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VaccineStatus",
                table: "Vaccines",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ClinicLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicName = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HealthcareProfessionals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: true),
                    lastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthcareProfessionals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_AdministeredBy",
                table: "Vaccines",
                column: "AdministeredBy");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_FamilyMemberId",
                table: "Vaccines",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicLocations_LocationId",
                table: "ClinicLocations",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_FamilyMembers_FamilyMemberId",
                table: "Appointments",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_FamilyMembers_FamilyMemberId",
                table: "Vaccines",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredBy",
                table: "Vaccines",
                column: "AdministeredBy",
                principalTable: "HealthcareProfessionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_FamilyMembers_FamilyMemberId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_FamilyMembers_FamilyMemberId",
                table: "Vaccines");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccines_HealthcareProfessionals_AdministeredBy",
                table: "Vaccines");

            migrationBuilder.DropTable(
                name: "ClinicLocations");

            migrationBuilder.DropTable(
                name: "HealthcareProfessionals");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_AdministeredBy",
                table: "Vaccines");

            migrationBuilder.DropIndex(
                name: "IX_Vaccines_FamilyMemberId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "AdministeredBy",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "AdministeredDate",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "BatchNumber",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "FamilyMemberId",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "Site",
                table: "Vaccines");

            migrationBuilder.DropColumn(
                name: "VaccineStatus",
                table: "Vaccines");

            migrationBuilder.RenameColumn(
                name: "FamilyMemberId",
                table: "Appointments",
                newName: "AppointmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_FamilyMemberId",
                table: "Appointments",
                newName: "IX_Appointments_AppointmentsId");

            migrationBuilder.AddColumn<bool>(
                name: "VaccineStatus",
                table: "FamilyMembers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_FamilyMembers_AppointmentsId",
                table: "Appointments",
                column: "AppointmentsId",
                principalTable: "FamilyMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
