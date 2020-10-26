using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarathonWebApiCore.Models
{
    public class Location
    {
        public Location()
        {
            Events = new HashSet<Events>();
        }
        [Key]
        public int LocationId { get; set; }
        [StringLength(50)]
        public string LocationName { get; set; }
        public string LocationAddress { get; set; }
        [StringLength(50)]
        public string LocationCity { get; set; }
        [StringLength(50)]
        public string LocationState { get; set; }
        [StringLength(6)]
        public string LocationPostal { get; set; }
        public double? LocationLong { get; set; }
        public double? LocationLati { get; set; }

        [InverseProperty("Location")]
        public virtual ICollection<Events> Events { get; set; }
    }
}
