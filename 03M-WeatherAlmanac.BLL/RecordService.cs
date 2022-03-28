using System;
using System.Collections.Generic;
using _03M_WeatherAlmanac.Core.Interface;
using _03M_WeatherAlmanac.Core.DTO;

namespace _03M_WeatherAlmanac.BLL
{
    public class RecordService : IRecordService
    {
        IRecordRepository _repo;
        public RecordService(IRecordRepository repo) //2:37PM CDT
        {
            _repo = repo;
        }
        public Result<DateRecord> Add(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            if(record.Date > DateTime.Now)
            {
                result.Message = "Date cannot be in the future.";
                result.Success = false;
                return result;
            }

            if(record.LowTemp < -50 || record.LowTemp>record.HighTemp || record.HighTemp > 140)
            {
                result.Message = "Invalid temperature range.";
                result.Success = false;
                return result;
            }

            if(record.Humidity < 0 || record.Humidity > 100)
            {
                result.Message = "Humidity must be between 0 and 100.";
                result.Success = false;
                return result;
            }
            return _repo.Add(record);
        }

        public Result<DateRecord> Edit(DateRecord record)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            if ((record.LowTemp < -50 || record.LowTemp > record.HighTemp || record.HighTemp > 140))
            {
                result.Message = "Invalid temperature range.";
                result.Success = false;
                return result;
            }
            
            if (record.Humidity < 0 || record.Humidity > 100)
            {
                result.Message = "Humidity must be between 0 and 100.";
                result.Success = false;
                return result;
            }

            return _repo.Edit(record);
        }

        public Result<DateRecord> Get(DateTime date)
        {
            Result<DateRecord> result = new Result<DateRecord>();
            if (date > DateTime.Now)
            {
                result.Message = "Date cannot be in the future.";
                result.Success = false;
                return result;
            }
            List<DateRecord> record = _repo.GetAll().Data;
            for (int index =0; index < record.Count; index++)
            {
                if(record[index].Date == date)
                {
                    result.Success = true;
                    result.Data = record[index];
                    return result;
                }
            }
            result.Success = false;
            result.Message = "No record with the requested date was found.";
            return result;
        }

        public Result<List<DateRecord>> LoadRange(DateTime start, DateTime end)
        {
            Result<List<DateRecord>> result = new Result<List<DateRecord>>();
            result.Data = new List<DateRecord>();
            
            if (start > end)
            {
                result.Message = ("End Date must be after Start Date.\n");
                return result;
            }

            Result<List<DateRecord>> records = _repo.GetAll();
            foreach (DateRecord record in records.Data)
            {
                if (record.Date >= start && record.Date <= end)
                {
                    result.Data.Add(record);
                    result.Success = true;
                }
            }
            if (result.Data == null || result.Data.Count == 0)  //if result was never assigned a value or count is at 0
            {
                result.Message = ("No records found within the given range.\n");
                result.Success = false;
            }
            return result;
        }

        public Result<DateRecord> Remove(DateTime date)
        {
            return _repo.Remove(date);
        }
    }
}
