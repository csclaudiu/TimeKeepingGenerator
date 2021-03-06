﻿using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeKeepingGenerator.ExternalDto;

namespace TimeKeepingGenerator
{
    public class HolidaysExplorer
    {
        private DateTime _startDate;
        private DateTime _endDate;
        // private readonly string _key = "3ebd0937-7811-47c9-955a-5083fa67e221"; // test
        //private readonly string _key = "3113fe1d-11b4-4c0a-ab75-da25e2fe31a7"; // prod
        private readonly string _country = "RO";
        private readonly string _api = "http://publicholiday.azurewebsites.net/api/v1/";
        public HolidaysExplorer(DateTime startDate, DateTime endDate)
        {
            _startDate = startDate;
            _endDate = endDate;
        }

        public List<DateTime> Get()
        {
            var result = new List<DateTime>();

            for (var year=_startDate.Year; year <= _endDate.Year; year++)
            {
                result.AddRange(GetHolidaysForYear(year));
            }

            return result;
        }

        private List<DateTime> GetHolidaysForYear(int year)
        {
            if (!File.Exists($"{year}.txt"))
                GetHolidaysFromExternal(year).Wait();
            
            return ReadHolidaysFromFile(year);
        }

        private List<DateTime> ReadHolidaysFromFile(int year)
        {
            try
            {
                List<Holiday> response = null;
                using (StreamReader file = File.OpenText($"{year}.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    response = (List<Holiday>)serializer.Deserialize(file, typeof(List<Holiday>));
                }
                return response.Select(h => h.date).ToList();
            }
            catch(Exception ex)
            {
                Trace.TraceError($"An error has occured while trying to read json file for year {year}. {ex}");
                throw;
            }
        }

        private async Task GetHolidaysFromExternal(int year)
        {
            Stream jsonHolidays;

            try
            {
                jsonHolidays = await $"{_api}/get/{_country}/{year}".GetStreamAsync();
            }
            catch(Exception ex)
            {
                Trace.TraceError($"An error has occured durring request of holidays for year {year}. {ex}");
                throw;
            }
            try
            {
                using (var fileStream = File.Create($"{year}.txt"))
                {
                    jsonHolidays.CopyTo(fileStream);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceError($"An error has occured while saving the json file of holidays for year {year}. {ex}");
                throw;
            }
            
        }
    }
}
