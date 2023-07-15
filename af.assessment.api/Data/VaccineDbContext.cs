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

using af.assessment.api.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Data
{
    /// <summary>
    ///     Provides method to seed data and sets up the database sets.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VaccineDbContext : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VaccineDbContext"/> class with the database context options that will be used. 
        /// </summary>
        /// <param name="options">
        ///     A <see cref="DbContextOptions"/> of context <see cref="VaccineDbContext"/> representing the context options.
        /// </param>
        public VaccineDbContext(DbContextOptions<VaccineDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="Appointment"/> representing the appointment database set.
        /// </summary>
     public virtual DbSet<Appointment> Appointments { get; set; }
        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="FamilyMember"/> representing the family member's table.
        /// </summary>
       public virtual DbSet<FamilyMember> FamilyMembers { get; set; }
        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="Location"/> representing the location table.
        /// </summary>
        public virtual DbSet<Location> Locations { get; set; }
        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="Member"/> representing the member table.
        /// </summary>
        public virtual DbSet<Member> Members { get; set; }
        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="Vaccine"/> representing the vaccine table.
        /// </summary>
        public virtual DbSet<Vaccine> Vaccines { get; set; }

        /// <summary>
        ///      A <see cref="DbSet"/> of <see cref="MedicalDetails"/> representing the medical details table.
        /// </summary>
        public virtual DbSet<MedicalDetails> MedicalDetails { get; set; }

        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="ClinicLocation"/> representing the clinic location table.
        /// </summary>
        public virtual DbSet<ClinicLocation> ClinicLocations { get; set; }

        /// <summary>
        ///     A <see cref="DbSet"/> of <see cref="HealthcareProfessional"/> representing the healthcare professional table.
        /// </summary>
        public virtual DbSet<HealthcareProfessional> HealthcareProfessionals { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vaccine>().HasData(SeedData.SeedVaccines());
            modelBuilder.Entity<FamilyMember>().HasData(SeedData.SeedFamilyMembers());
            modelBuilder.Entity<Member>().HasData(SeedData.SeedMember());
            modelBuilder.Entity<Location>().HasData(SeedData.SeedLocation());
            modelBuilder.Entity<MedicalDetails>().HasData(SeedData.SeedMedicalDetails());
        }
    }
}