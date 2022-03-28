using _03M_WeatherAlmanac.Core.Interface;
using _03M_WeatherAlmanac.Core.DTO;
using System;
using System.Collections.Generic;

namespace _03M_WeatherAlmanac.UI
{
    class MenuController
    {
        private ConsoleIO _ui;
        public IRecordService Service;
        private ApplicationMode _mode;

        public MenuController(ConsoleIO ui)
        {
            _ui = ui;
        }

        public ApplicationMode SetUp()
        {
            _ui.Display("Enter Application Mode: ");
            _ui.Display("1. Test");
            _ui.Display("2. Live");

            int mode = _ui.GetInt("");
            if (mode == 1)
            {
                return ApplicationMode.TEST;
            }
            else if (mode == 2)
            {
                return ApplicationMode.LIVE;
            }
            else
            {
                _ui.Error("Invalid mode. Exiting.");
                Environment.Exit(0);
                return ApplicationMode.TEST;    //the program exited here, bogus return stmt
            }
            
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                switch (GetMenuChoice())
                {
                    case 1:
                        LoadRecord();
                        break;
                    case 2:
                        ViewRecordsByDateRange();
                        break;
                    case 3:
                        AddRecord();
                        break;
                    case 4:
                        EditRecord();
                        break;
                    case 5:
                        DeleteRecord();
                        break;
                    case 6:
                        running = false;
                        break;
                    default:
                        _ui.Display("Invelid Menu Option");
                        break;
                }
            }
        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            return _ui.GetInt("Enter Choice");
        }

        public void DisplayMenu()
        {
            _ui.Display("");
            _ui.Display("Main Menu");
            _ui.Display("==========================");
            _ui.Display("1. Load a Record");
            _ui.Display("2. View Records by Date Range");
            _ui.Display("3. Add Record");
            _ui.Display("4. Edit Record");
            _ui.Display("5. Delete Record");
            _ui.Display("6. Quit");
        }

        public void LoadRecord()
        {
            _ui.Display("\nLoad Record\n==========================");
            DateRecord input = new DateRecord();
            input.Date = _ui.GetDateTime("Date");
            Result<DateRecord> result = Service.Get(input.Date);
            if (!result.Success)
            {
                _ui.Error(result.Message);
                return;
            }

            _ui.Display(result.Data.ToString());
        }

        public void ViewRecordsByDateRange()
        {
            _ui.Display("View Records By Date Range");
            Result<List<DateRecord>> result = Service.LoadRange(_ui.GetDateTime("Enter Start Date"), _ui.GetDateTime("Enter End Date"));
            if (!result.Success)
            {
                _ui.Error(result.Message);
                return;
            }
            foreach (DateRecord record in result.Data)
            {
                _ui.Display("\n" + record.ToString());
            }
        }
        public void AddRecord()
        {
            _ui.Display("\nAdd Record\n==========================");
            DateRecord input = new DateRecord();
            input.Date = _ui.GetDateTime("Date");
            input.HighTemp = _ui.GetDecRequired("High");
            input.LowTemp = _ui.GetDecRequired("Low");
            input.Humidity = _ui.GetDecRequired("Humidity");
            input.Description = _ui.GetString("Description");
            Result<DateRecord> result = Service.Add(input);

            if (!result.Success)
            {
                _ui.Display(result.Message);
            }
        }
        public void EditRecord()
        {
            _ui.Display("\nEdit Record\n==========================");
            DateRecord input = new DateRecord();

            input.Date = _ui.GetDateTime("Date");
            Result<DateRecord> result = Service.Get(input.Date);    //checks if it exists
            if (!result.Success)
            {
                _ui.Error(result.Message);
                return;
            }

            input.HighTemp = _ui.GetDecOptional("High (" + result.Data.HighTemp + ")");
            input.LowTemp = _ui.GetDecOptional("Low (" + result.Data.LowTemp + ")");
            input.Humidity = _ui.GetDecOptional("Humidity (" + result.Data.Humidity + "%)");
            
            _ui.Display("Old Description: " + result.Data.Description);
            input.Description = _ui.GetString("New Description");

            result = Service.Edit(input);

            if (!result.Success)
            {
                _ui.Error(result.Message);
            }

        }
        public void DeleteRecord()
        {
            _ui.Display("\nDelete Record\n==========================");
            DateTime input = _ui.GetDateTime("Date");

            Result<DateRecord> result = Service.Get(input);     //checks if it exists
            if (!result.Success)
            {
                _ui.Error(result.Message);
                return;
            }

            _ui.Display(result.Data.ToString());
            string deleteConfirm =_ui.GetString("Are you sure you want to delete this record (y/n)").ToLower();
            if (deleteConfirm == "y")
            {
                result = Service.Remove(input);
            }
            else if (deleteConfirm == "n")
            {
                result.Message = "Record was not deleted. Returning to Main Menu...";
            }
            else
            {
                result.Message = "No Confirmation. Returning to Main Menu...";
            }
            _ui.Display(result.Message);
        }
    }
}
