using System;
using System.Collections.Generic;
using _03M_WeatherAlmanac.Core.Interface;
using _03M_WeatherAlmanac.Core.DTO;

namespace _03M_WeatherAlmanac.DAL
{
    public class MockRecordRepository : IRecordRepository
    {
        private List<DateRecord> _records;

        public MockRecordRepository()
        {
            _records = new List<DateRecord>();
            //DateRecord bogus = new DateRecord();
            //bogus.Date = new DateTime();
            //bogus.HighTemp = 82;
            //bogus.LowTemp = 40;
            //bogus.Humidity = .30m;
            //bogus.Description = "Really inconsistent weather today";
            //_records.Add(bogus);
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            _records.Add(record);
            Result<DateRecord> result = new Result<DateRecord>();
            result.Success = true;
            result.Message = "New DateRecord Added";
            //result.Data = record;
            return result;
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            for (int index = 0; index < _records.Count; index++)
            {
                if (_records[index].Date == record.Date)
                {
                    if (record.HighTemp != -1000)
                    {
                        _records[index].HighTemp = record.HighTemp;
                    }
                    if (record.LowTemp != -1000)
                    {
                        _records[index].LowTemp = record.LowTemp;
                    }
                    if (record.Humidity != -1000)
                    {
                        _records[index].Humidity = record.Humidity;
                    }
                    if (!record.Description.Equals(""))
                    {
                        _records[index].Description = record.Description;
                    }
                    result.Success = true;
                    result.Message = "Record "+ record.Date +"was updated.";
                }
            }

            return result;
        }

        public Result<List<DateRecord>> GetAll()
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            result.Success = true;
            result.Message = "";
            result.Data = new List<DateRecord>(_records);
            return result;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            for (int index = 0; index < _records.Count; index++)
            {
                if(_records[index].Date == date)
                {
                    result.Data = _records[index];
                    _records.Remove(_records[index]);
                    result.Success = true;
                    result.Message = "Record was removed.";
                }
            }
            return result;
        }
    }
}
