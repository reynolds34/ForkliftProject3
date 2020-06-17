using System;
using System.Collections.Generic;

namespace ForkliftProject3.Models
{
    public partial class Vehicles
    {
        public int Id { get; set; }
        public string Vin { get; set; }
        public string MakeName { get; set; }
        public bool Active { get; set; }
        public int Year { get; set; }
    }
}
