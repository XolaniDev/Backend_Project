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
using System.Runtime.Serialization;

namespace af.assessment.api.Data.Models
{
    /// <summary>
    ///     Represents the medical details of the member in the vaccine system.
    /// </summary>
    public class MedicalDetails
    {
        /// <summary>
        ///      Gets or sets a <see cref="Guid"/> representing the medical details Id that will be retrieved.
        /// </summary>
        [Required]
        [DataMember(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the medical aid name that will be retrieved.
        /// </summary>
        [DataMember(Name = "medicalAidName")]
        public string MedicalAidName { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the medical aid number that will be retrieved.
        /// </summary>
        [DataMember(Name = "medicalAidNumber")]
        public string MedicalAidNumber { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the main member name that will be retrieved.
        /// </summary>
        [DataMember(Name = "mainMemberName")]
        public string MainMemberName { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the main member cell number that will be retrieved.
        /// </summary>
        [DataMember(Name = "mainMemberNumber")]
        public string MainMemberNumber { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the member id.
        /// </summary>
        [DataMember(Name = "memberId")]
        public Guid MemberId { get; set; }
    }
}