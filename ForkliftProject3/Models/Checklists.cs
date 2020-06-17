using System;
using System.Collections.Generic;

namespace ForkliftProject3.Models
{
    public partial class Checklists
    {
        public int ChecklistId { get; set; }
        public string CompletedBy { get; set; }
        public DateTime TodaysDate { get; set; }
        public string VinNumber { get; set; }
        public string Battery { get; set; }
        public string Oil { get; set; }
        public string Lights { get; set; }
        public string Horn { get; set; }
        public string Levers { get; set; }
        public string Tires { get; set; }
        public string Forks { get; set; }
        public string Comments { get; set; }
        
    }
}
