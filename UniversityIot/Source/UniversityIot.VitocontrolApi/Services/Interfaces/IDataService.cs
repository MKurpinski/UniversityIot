using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public interface IDataService
    {
         IList<User> Users { get; set; }
         IList<Gateway> Gateways { get; set; }
    }
}