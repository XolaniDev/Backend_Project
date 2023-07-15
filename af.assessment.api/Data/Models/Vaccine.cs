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

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace af.assessment.api.Data.Models
{
    /// <summary>
    ///     Represents the vaccine details.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Vaccine
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing a unique identifier for a vaccine.
        /// </summary>
        [Required]
        [DataMember(Name = "vaccineId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the name of a vaccine.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="double"/> representing the dose of a vaccine.
        /// </summary>
        [DataMember(Name = "dosage")]
        public double Dose { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="ClinicLocation"/> representing the clinic where the vaccine was taken.
        /// </summary>
        [DataMember(Name = "clinic")]
        public ClinicLocation Clinic { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="bool"/> representing whether the vaccine has been administered or not.
        /// </summary>
        [DataMember(Name = "vaccine status")]
        public bool VaccineStatus { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="double"/> representing the batch number of a family member.
        /// </summary>
        [DataMember(Name = "batch number")]
        public double BatchNumber { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="DateTime"/> representing the date at which a vaccine had been administered.
        /// </summary>
        [DataMember(Name = "date administered")]
        public DateTime AdministeredDate { get; set; }

    /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the site of the family member that the vaccine was administered to.
        /// </summary>
        [DataMember(Name = "site")]
        public string Site { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="HealthcareProfessional"/> representing the healthcare professional that administered the vaccine.
        /// </summary>
        [DataMember(Name = "healthcare professional")]
        public HealthcareProfessional AdministeredBy { get; set; }

        /// <summary>
        ///     A <see cref="Guid"/> representing the foreign key of the family member.
        /// </summary>
        public Guid? FamilyMemberId { get; set; }
        
        /// <summary>
        ///     A <see cref="FamilyMember"/> representing the inverse navigation property to family member.
        /// </summary>
        public FamilyMember FamilyMember { get; set; }
    }
}