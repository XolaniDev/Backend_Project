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
    ///     Represents the appointment details of the member in the vaccine system.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Appointment
    {
        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the appointment id that will be retrieved.
        /// </summary>
        [Required]
        [DataMember(Name = "AppointmentsId")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the member id that will be retrieved.
        /// </summary>
        [Required]
        [DataMember(Name = "memberId")]
        public Guid MemberId { get; set; }


        /// <summary>
        ///     Gets or sets a <see cref="DateTime"/> representing the appointment date selected that will be retrieved.
        /// </summary>
        [DataMember(Name = "dateSelected")]
        public DateTime DateSelected { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="DateTime"/> representing the appointment available date that will be retrieved.
        /// </summary>
        [DataMember(Name = "availableDate")]
        public DateTime AvailableDate { get; set; }

        /// <summary>
        ///     Gets or sets an <see cref="AppointmentStatus"/> representing the status of the appointment that will be retrieved.
        /// </summary>
        [DataMember(Name = "status")]
        public AppointmentStatus Status { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="Guid"/> representing the vaccine id of the vaccine for the appointment that will be retrieved.
        /// </summary>
        [DataMember(Name = "vaccineId")]
        [ForeignKey("vaccineId")]
        public Guid vaccineId { get; set; }

        /// <summary>
        ///     Gets or sets a list of <see cref="Vaccine"/> representing the vaccines booked for the appointment that will be retrieved.
        /// </summary>
        [DataMember(Name = "vaccines")]
        public List<Vaccine> Vaccines { get; set; }
    }
}