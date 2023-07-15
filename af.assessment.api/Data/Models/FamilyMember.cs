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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using af.assessment.api.Enums;

namespace af.assessment.api.Data.Models
{
    /// <summary>
    ///     Represents the family member details of a member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FamilyMember
    {

        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing a unique identifier for the family member.
        /// </summary>
        [Required]
        [DataMember(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the first name of a family member.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the last name of a family member.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the identification number of a family member.
        /// </summary>
        [Required]
        [DataMember(Name = "identificationNumber")]
        public string IdentificationNumber { get; set; }
        
        /// <summary>
        ///     Gets or sets a <see cref="MemberType"/> representing the relationship of a family member with regards to the member.
        /// </summary>
        [DataMember(Name = "relationship")]
        public MemberType Relationship { get; set; }

        /// <summary>
        ///     Gets or sets a list of <see cref="Appointment"/> representing the appointments of a family member.
        /// </summary>
        [DataMember(Name = "appointment")]
        public List<Appointment> Appointments { get; set; }

        /// <summary>
        ///     Gets or sets a list of <see cref="Vaccine"/> representing the vaccines of a family member.
        /// </summary>
        [DataMember(Name = "vaccine")]
        public List<Vaccine> Vaccines { get; set; }

        /// <summary>
        ///     A <see cref="Guid"/> representing the foreign key of the member.
        /// </summary>
        public Guid MemberId { get; set; }
        /// <summary>
        ///     A <see cref="Member"/> representing the inverse navigation property to member.
        /// </summary>
        public Member Member { get; set; }
    }
}