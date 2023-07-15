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

using System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using af.assessment.api.Data.Models;
using af.assessment.api.Enums;

namespace af.assessment.api.Data
{
    /// <summary>
    ///     Provides seed data for the database.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SeedData
    {
        /// <summary>
        ///     A <see cref="Guid"/> representing the location id to be seeded. 
        /// </summary>
        private static Guid _locationId = Guid.Parse("dca21998-eeab-4291-aae2-44a7b5e8ff03");
        
        /// <summary>
        ///     A <see cref="Guid"/> representing the member id to be seeded.
        /// </summary>
        private static Guid _memberId = Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");

        /// <summary>
        ///     A <see cref="Guid"/> representing the medical details id to be saved.
        /// </summary>        
        private static Guid _medicalDetatilsId = Guid.Parse("35c1a665-7bb5-43f2-9423-beb9afdda515");
        
        /// <summary>
        ///     Creates a seed member and then returns the member. 
        /// </summary>
        /// <returns>
        ///     A <see cref="Member"/> representing the seed member.
        /// </returns>
        public static Member SeedMember()
        {           
            return new Member()
            {
                Id = _memberId,
                IdentificationNumber = "0001010000006",
                MobileNumber = "(087)-999-8765",
                Name = "Jamie Dimon",
                Password = "$2a$11$Gf6Mgp210F8A1n827CrlX.4WRQSB9BnNfOdE6Yu0hWdp6PABDWEKG",
                Salt = "$2a$11$Gf6Mgp210F8A1n827CrlX.",
                Email = "jamie@eblocks.co.za",
                ProfilePictureUrl = "https://dcvcstorage.blob.core.windows.net/profilepics/ben.jpg",
            }; 
        }

        /// <summary>
        ///     Creates a list of seed family members and then returns the list of family members.
        /// </summary>
        /// <returns>
        ///     A <see cref="List"/> of <see cref="FamilyMember"/> representing the seed list of family members.
        /// </returns>
        public static List<FamilyMember> SeedFamilyMembers()
        {
            var familyMemberList = new List<FamilyMember>()
            {
                new FamilyMember()
                {
                    Id = Guid.Parse("8c907bc1-8c37-4c71-95e3-c35519280062"),
                    FirstName = "Maddy",
                    LastName = "Mason",
                    IdentificationNumber = "2109302345084",
                    Relationship = MemberType.Daughter,
                    MemberId = _memberId,
                },
                new FamilyMember()
                {
                    Id = Guid.Parse("77d4f8cc-9797-4f25-9b99-69fde7aeb495"),
                    FirstName = "Brad",
                    LastName = "Mason",
                    IdentificationNumber = "2109305234087",
                    Relationship = MemberType.Son,
                    MemberId = _memberId
                }
            };
            return familyMemberList;
        }

        /// <summary>
        ///     Creates a list of seed appointments and then returns the list of appoinments.
        /// </summary>
        /// <returns>
        ///     A <see cref="List"/> of <see cref="Appointment"/> representing the seed list of appointments.
        /// </returns>
        public static List<Appointment> SeedAppointments()
        {
            var appointmentList = new List<Appointment>()
            {
                new Appointment()
                {
                    Id = Guid.Parse("f6e3d125-609a-4db0-a909-2e104170f108"),
                    AvailableDate = new DateTime(2021, 11, 01),
                    DateSelected = new DateTime(2021, 11,02),
                    MemberId = _memberId,
                    Status = AppointmentStatus.UpComing,
                }
            };
            return appointmentList;
        }

        /// <summary>
        ///     Creates a seed location and then returns the location.
        /// </summary>
        /// <returns>
        ///     A <see cref="Location"/> representing the seed location.
        /// </returns>
        public static Location SeedLocation()
        {
            return
                new Location()
                {
                    Id = _locationId,
                    City = "Johannesburg",
                    StreetName = "21st Street",
                    PostalCode = 1220,
                    MemberId = _memberId
                };
        }

        /// <summary>
        ///     Creates a seed vaccine and then returns the vaccine.
        /// </summary>
        /// <returns>
        ///     A <see cref="Vaccine"/> representing the seed vaccine.
        /// </returns>
       public static Vaccine SeedVaccine()
        {
            return new Vaccine()
            {
                Id = Guid.Parse("3608a3c3-8e5c-4af5-a38c-fb843344080b"),
                Dose = 0.12,
                Name = "Hepatitis A",               
            };
        }

        /// <summary>
        ///     Creates a list of seed vaccines and then returns the list of vaccines.
        /// </summary>
        /// <returns>
        ///     A <see cref="List"/> of <see cref="Vaccine"/> representing the list of seed vaccines.
        /// </returns>
        public static List<Vaccine> SeedVaccines()
        {
            var vaccineList = new List<Vaccine>()
            {
                new Vaccine()
                {
                    Id = Guid.Parse("fabda97c-eaff-41ca-ae32-0c0a099d2955"),
                    Dose = 0.07,
                    Name = "Haemophilus Influenzae type B",
                    VaccineStatus = true,
                    FamilyMemberId = Guid.Parse("8c907bc1-8c37-4c71-95e3-c35519280062"),
                },
                new Vaccine()
                {
                    Id = Guid.Parse("8ce0c789-f7bd-4a9c-a491-45df5b386657"),
                    Dose = 0.12,
                    Name = " Rotavirus Gastroenteritis",
                    VaccineStatus = false,
                    FamilyMemberId = Guid.Parse("8c907bc1-8c37-4c71-95e3-c35519280062"),
                },
                new Vaccine()
                {
                    Id = Guid.Parse("e36265ea-0e0d-4a9c-8275-9cac364e75a6"),
                    Dose = 0.8,
                    Name = " Tetanus ",
                    VaccineStatus = true,
                    FamilyMemberId = Guid.Parse("8c907bc1-8c37-4c71-95e3-c35519280062"),
                }
            };
            return vaccineList;
        }

        /// <summary>
        ///      Creates a seed medical aid details and then returns the medical details.
        /// </summary>
        /// <returns>A <see cref="MedicalDetails"/> represeting the medical details </returns>
        public static MedicalDetails SeedMedicalDetails()
        {
            return new MedicalDetails()
            {
                Id = _medicalDetatilsId,
                MainMemberName = "Jamie Dimon",
                MainMemberNumber = "(087)-999-8765",
                MedicalAidName = "Discovery Life",
                MedicalAidNumber = "001334761",
                MemberId = _memberId
            };
        }
    }   
}