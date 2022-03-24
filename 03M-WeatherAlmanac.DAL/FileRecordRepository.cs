using _03M_WeatherAlmanac.Core.DTO;
using _03M_WeatherAlmanac.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03M_WeatherAlmanac.DAL
{
    public class FileRecordRepository : IRecordRepository
    {
        private List<DateRecord> _records;
        private string _path;

        public FileRecordRepository()
        {
            _records = new List<DateRecord>();
            _path = Directory.GetCurrentDirectory() + "/Data/DateRecords.csv";
            DateRecordCSVFormatter csv = new DateRecordCSVFormatter();

            using(StreamReader sr = new StreamReader(_path))
            {
                string currentLine = sr.ReadLine();

                if (currentLine != null)
                {
                    currentLine = sr.ReadLine();
                }
                while(currentLine != null)
                {
                    DateRecord record = csv.Deserialize(currentLine.Trim());
                    _records.Add(record);
                    currentLine = sr.ReadLine();
                }
            }
        }

        public Result<DateRecord> Add(DateRecord record)
        {
            _records.Add(record);

            WriteToFile();
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
                    result.Message = "Record " + record.Date + "was updated.";
                }
            }
            WriteToFile();
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
                if (_records[index].Date == date)
                {
                    result.Data = _records[index];
                    _records.Remove(_records[index]);
                    result.Success = true;
                    result.Message = "Record was removed.";
                }
            }
            WriteToFile();
            return result;
        }

        public void WriteToFile()
        {
            //string p = @"D:\Brayden\Dev10 Trainning\Result.csv"; //hardcoded pathway

            DateRecordCSVFormatter csv = new DateRecordCSVFormatter();
            File.WriteAllText(_path, "Date,HighTemp,LowTemp,Humidity,Description");

            bool appendMode = true;
            using (StreamWriter sw = new StreamWriter(_path, appendMode))
            {
                foreach (DateRecord record in _records)
                {
                    sw.Write($"\n{csv.Serialize(record)}");
                }
            }
        }
    }
}
