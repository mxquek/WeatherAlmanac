﻿namespace _03M_WeatherAlmanac.Core.DTO
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data {get; set;}
    }
}
