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
    ///     Represents the clinic details of a clinic.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ClinicLocation
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing a unique identifier for the clinic.
        /// </summary>
        [Required]
        [DataMember(Name = "id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="string"/> representing the name of the clinic that will be retrieved.
        /// </summary>
        [DataMember(Name = "clinic name")]
        public string ClinicName { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="Location"/> representing the location details of the clinic that will be retrieved.
        /// </summary>
        [DataMember(Name = "location")]
        public Location Location { get; set; }
    }
}