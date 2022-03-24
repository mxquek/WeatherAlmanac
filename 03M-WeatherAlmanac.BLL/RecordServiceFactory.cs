using System;
using System.Collections.Generic;
using _03M_WeatherAlmanac.Core.Interface;
using _03M_WeatherAlmanac.Core.DTO;
using _03M_WeatherAlmanac.DAL;

namespace _03M_WeatherAlmanac.BLL
{
    public class RecordServiceFactory
    {
        public static IRecordService GetRecordService(ApplicationMode mode)
        {
            if (mode == ApplicationMode.TEST)
            {
                return new RecordService(new MockRecordRepository());
            }
            else if (mode == ApplicationMode.LIVE)
            {
                return new RecordService(new FileRecordRepository());
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
