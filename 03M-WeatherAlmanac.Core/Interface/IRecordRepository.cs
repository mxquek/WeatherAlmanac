using System;
using System.Collections.Generic;
using _03M_WeatherAlmanac.Core.DTO;

namespace _03M_WeatherAlmanac.Core.Interface
{
    public interface IRecordRepository
    {
        Result<List<DateRecord>> GetAll();          // Retrieves all records from storage
        Result<DateRecord> Add(DateRecord record);  // Adds a record to storage
        Result<DateRecord> Remove(DateTime date);   // Removes record for date
        Result<DateRecord> Edit(DateRecord record); // Replaces a record with the same date

    }
}
