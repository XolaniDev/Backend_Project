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
    ///     Represents a location of a clinic
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HealthcareProfessional
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing a healthcare professionals unique identifier.
        /// </summary>
        [Required]
        [DataMember(Name = "healthcare professional")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the first name of a healthcare professional.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string firstName { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the last name of a healthcare professional.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string lastName { get; set; }
    }
}