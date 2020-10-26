using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarathonWebApiCore.Models
{
    public class Registration
    {
        [Key]
        public int RegistarionId { get; set; }
        public int? EventId { get; set; }
        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey(nameof(EventId))]
        [InverseProperty("Registration")]
        public virtual Events Event { get; set; }
    }
}
