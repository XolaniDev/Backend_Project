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
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;

namespace af.assessment.api.Converters
{
    /// <summary>
    ///     The following converter contains the methods that will convert a model to DTO
    /// </summary>
    public class VaccineCardDtoConverter : IVaccineCardDtoConverter
    {
        /// <inheritdoc/>
        public VaccineCardDto ConvertToListOfFamilyMembers(Member member)
        {
            if (member == null)
            {
                return null;
            }

            var familyMemberList = new List<FamilyMemberDto>() { };

            foreach (FamilyMember familyMember in member.FamilyMember)
            {
                var vaccineList = new List<VaccineDto>() { };

                foreach (Vaccine vaccine in familyMember.Vaccines)
                {
                    string clinicName = null;
                    string professionalName = null;
                    if (vaccine.Clinic != null)
                    {
                        clinicName = vaccine.Clinic.ClinicName;
                    }
                    if (vaccine.AdministeredBy != null)
                    {
                        professionalName = vaccine.AdministeredBy.firstName + " " + vaccine.AdministeredBy.lastName;
                    }
                    vaccineList.Add(new VaccineDto { Name = vaccine.Name, Dose = vaccine.Dose, VaccineStatus = vaccine.VaccineStatus, BatchNumber = vaccine.BatchNumber, DateAdministered = vaccine.AdministeredDate, Site = vaccine.Site, ClinicName = clinicName, HealthcareProfessional = professionalName });
                }
                familyMemberList.Add(new FamilyMemberDto { FirstName = familyMember.FirstName, LastName = familyMember.LastName, IdentificationNumber = familyMember.IdentificationNumber, MemberType = familyMember.Relationship, Vaccines = vaccineList });
            };

            var vaccineCardDto = new VaccineCardDto
            {
                FamilyMember = familyMemberList
            };

            return vaccineCardDto;
        }
    }
}