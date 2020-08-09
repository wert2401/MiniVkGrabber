using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MiniGraber.Objects;
using Newtonsoft.Json;

namespace MiniGraber.Utils
{
    static class JSONProcessor
    {
        public static List<Person> ParsePeople(string response)
        {
            response = Regex.Replace(response, "\"response\":{\"count\":([0-9]+),\"items\":", "");
            if (TryParseError(response))
            {
                return null;
            }
            response = response.Substring(1, response.Length - 4);
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(response);
            return people;
        }

        public static bool TryParseError(string response)
        {
            Error e;
            try
            {
                e = JsonConvert.DeserializeObject<Error>(response);
                if (e.error != null)
                {
                    return true;
                }
            }
            catch 
            {
                return false;
            }
            
            return false;
        }

        public static Person ParsePerson(string response)
        {
            response = response.Replace("{\"response\":[", "");
            response = response.Substring(0, response.Length - 3);
            if (TryParseError(response))
            {
                return new Person();
            }
            return JsonConvert.DeserializeObject<Person>(response);
        }

    }
}
