using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet_and_Copmete_Capstone.Models
{
    public class Invite
    {
        [Key]
        public int Id { get; set; }
        public bool Accepted { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }


        [ForeignKey("Eventee")]
        public int EventeeId { get; set; }
        public Eventee Eventee { get; set; }
    }
}
