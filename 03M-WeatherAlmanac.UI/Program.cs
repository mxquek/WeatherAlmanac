using System;
using _03M_WeatherAlmanac.Core.DTO;
using _03M_WeatherAlmanac.Core.Interface;
using _03M_WeatherAlmanac.BLL;

namespace _03M_WeatherAlmanac.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //String foo = "This is the resulting data";
            //Result<string> stringResult= new Result<string>();
            //stringResult.Data = foo;
            //stringResult.Success = true;

            //stringResult.Success = false;
            //stringResult.Message = "Did not parse";

            ConsoleIO ui = new ConsoleIO();
            MenuController menu = new MenuController(ui);
            ApplicationMode mode = menu.SetUp();   //Get test vs live mode
            IRecordService service = RecordServiceFactory.GetRecordService(mode);
            menu.Service = service;
            menu.Run();     //Do the thing!
        }
    }
}
