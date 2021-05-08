using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet_and_Copmete_Capstone.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public string Extras { get; set; }
        public int ZipCode { get; set; }

        [ForeignKey("EventPlaner")]
        public int EventPlannerId { get; set; }
        public EventPlaner EventPlaner { get; set; }
    }
}
