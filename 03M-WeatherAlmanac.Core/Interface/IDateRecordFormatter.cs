using _03M_WeatherAlmanac.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_WeatherAlmanac.Core.Interface
{
    public interface IDateRecordFormatter
    {
        DateRecord Deserialize(string data);
        string Serialize(DateRecord record);
    }
}
