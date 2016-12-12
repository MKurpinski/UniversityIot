using System.Collections.Generic;

namespace UniversityIot.VitocontrolApi.Services
{
    public interface IDataService
    {
        IEnumerable<User> Users { get; set; }
    }
}