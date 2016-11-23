using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityIot.VitocontrolApi.Enums;

namespace UniversityIot.VitocontrolApi.Models
{
    public class Gateway
    {
        public string SerialNumber { get; set; }
        public GatewayStatus Status { get; set; }
        public User User { get; set; }
    }
}