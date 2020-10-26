using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarathonWebApiCore.Models
{
    public class Events
    {
        public Events()
        {
            Registration = new HashSet<Registration>();
        }
        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(50)]
        public string EventNane { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? EventStart { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime? EventEnd { get; set; }

        [Required]
        public string EventDec { get; set; }

        public int? LocationId { get; set; }

        public bool? IsActive { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Events")]
        public virtual Location Location { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<Registration> Registration { get; set; }
    }
}
