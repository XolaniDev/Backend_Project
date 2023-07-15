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

namespace af.assessment.api.Data.Models
{
    /// <summary>
    ///     Represents a member in the vaccine system.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Member
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the member's unique identifier.
        /// </summary>
        [Required]
        [DataMember(Name = "id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the member's name.
        /// </summary>
        [DataMember(Name = "memberName")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the member's email.
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the member's identification number.
        /// </summary>
        [DataMember(Name = "identificationNumber")]
        public string IdentificationNumber { get; set; }

        /// <summary>
        ///    Gets or sets a <see cref="string"/> representing the member's mobile number. 
        /// </summary>
        [DataMember(Name = "mobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        ///    Gets or sets a <see cref="string"/> representing the member's hashed password. 
        /// </summary>
        [DataMember(Name = "hashPassword")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the member's password salt.
        /// </summary>
        [DataMember(Name = "salt")]
        public string Salt { get; set; }

        /// <summary>
        ///    Gets or sets a list of <see cref="FamilyMember"/> representing the member's family members. 
        /// </summary>
        [DataMember(Name = "familyMember")]
        public List<FamilyMember> FamilyMember { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="int"/> representing the member's otp preference.
        /// </summary>
        public int OtpPreference { get; set; }

        /// <summary>
        ///      Gets or sets a <see cref="Location"/> representing the member's location.
        /// </summary>                
        public Location Locations { get; set; }

        /// <summary>
        ///      Gets or sets a <see cref="MedicalDetails"/> representing the member's medical details.
        /// </summary>
        [DataMember(Name = "medicalDetails")]
        public MedicalDetails MedicalDetails { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the member's profile picture url.
        /// </summary>
        [DataMember(Name = "profilePictureUrl")]
        public string ProfilePictureUrl { get; set; }
    }
}