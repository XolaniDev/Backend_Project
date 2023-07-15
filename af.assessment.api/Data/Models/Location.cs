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
    ///     Represents a location of the member or clinic
    /// </summary>
    public class Location
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the the location details.
        /// </summary>
        [Required]
        [DataMember(Name = "id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Gets or sets <see cref="string"/> representing the street name.
        /// </summary>
        [DataMember(Name = "streetName")]
        public string StreetName { get; set; }
        
        /// <summary>
        ///     Gets or sets <see cref="int"/> representing the postal code.
        /// </summary>
        [DataMember(Name = "postalCode")]
        public int PostalCode { get; set; }
        
        /// <summary>
        ///     Gets or sets <see cref="string"/> representing the city.
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets <see cref="Guid"/> representing the member id.
        /// </summary>
        [DataMember(Name = "member")]
        public Guid MemberId { get; set; }
    }
}